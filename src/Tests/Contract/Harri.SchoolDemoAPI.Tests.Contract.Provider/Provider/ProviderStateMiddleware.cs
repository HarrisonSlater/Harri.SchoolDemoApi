using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Harri.SchoolDemoAPI.Models;
using Harri.SchoolDemoAPI.Repository;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Harri.SchoolDemoAPI.Tests.Contract.Provider
{
    /// <summary>
    /// Middleware for handling provider state requests
    /// base file from: https://github.com/pact-foundation/pact-net/blob/master/samples/OrdersApi/Provider.Tests/ProviderStateMiddleware.cs
    /// </summary>
    public class ProviderStateMiddleware
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNameCaseInsensitive = true
        };

        private readonly IDictionary<string, Func<IDictionary<string, object>, Task>> providerStates;
        private readonly RequestDelegate _next;
        //private readonly IStudentRepository _studentRepository;

        /// <summary>
        /// Initialises a new instance of the <see cref="ProviderStateMiddleware"/> class.
        /// </summary>
        /// <param name="next">Next request delegate</param>
        /// <param name="orders">Orders repository for actioning provider state requests</param>
        public ProviderStateMiddleware(RequestDelegate next)
        {
            _next = next;
            //_studentRepository = studentRepository;

            this.providerStates = new Dictionary<string, Func<IDictionary<string, object>, Task>>
            {
                ["a student with sId {sId} exists"] = this.EnsureStudentExists
            };
        }

        /// <summary>
        /// Ensure an event exists
        /// </summary>
        /// <param name="parameters">Event parameters</param>
        /// <returns>Awaitable</returns>
        private async Task EnsureStudentExists(IDictionary<string, object> parameters)
        {
            var sId = (JsonElement?)parameters["sId"];
            var name = (JsonElement?)parameters["name"];
            var gpa = (JsonElement?)parameters["GPA"];

            var studentToMock = new Student()
            {
                SId = sId?.GetInt32(),
                Name = name?.GetString(),
                GPA = gpa?.GetDecimal()
            };
            TestStartup.MockStudentRepo.Setup(s => s.GetStudent(It.IsAny<int>())).Returns(studentToMock);
        }

        /// <summary>
        /// Handle the request
        /// </summary>
        /// <param name="context">Request context</param>
        /// <returns>Awaitable</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            if (!(context.Request.Path.Value?.StartsWith("/provider-states") ?? false))
            {
                await _next.Invoke(context);
                return;
            }

            context.Response.StatusCode = StatusCodes.Status200OK;

            if (context.Request.Method == HttpMethod.Post.ToString())
            {
                string jsonRequestBody;

                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                {
                    jsonRequestBody = await reader.ReadToEndAsync();
                }

                try
                {
                    ProviderState providerState = JsonSerializer.Deserialize<ProviderState>(jsonRequestBody, Options);
                    
                    if (!string.IsNullOrEmpty(providerState?.State))
                    {
                        await this.providerStates[providerState.State].Invoke(providerState.Params);
                    }
                }
                catch (Exception e)
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsync("Failed to deserialise JSON provider state body:");
                    await context.Response.WriteAsync(jsonRequestBody);
                    await context.Response.WriteAsync(string.Empty);
                    await context.Response.WriteAsync(e.ToString());
                }
            }
        }
    }
}