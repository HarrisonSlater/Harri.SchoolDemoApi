﻿using Harri.SchoolDemoAPI.Models;
using System.Net.Http.Json;

namespace Harri.SchoolDemoApi.Client
{
    public class StudentApiClientHttpClient
    {
        private readonly HttpClient _client;

        public StudentApiClientHttpClient()
        {
            //var options = new RestClientOptions("https://localhost:44301/");
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:44301/");
        }

        // Add
        public async Task<int?> AddStudent(NewStudent student)
        {
            var restResponse = await AddStudentRestResponse(student);
            return await restResponse.Content.ReadFromJsonAsync<int?>();
        }

        //RestResponse<int?>
        public async Task<HttpResponseMessage> AddStudentRestResponse(NewStudent student)
        {
            //var request = new RestRequest("student/").AddBody(student);
            //var restResponse = await _restClient.ExecutePostAsync<int?>(request);
            var response = await _client.PostAsJsonAsync("student/", student);
            return response;
        }

        
        // Get
        public async Task<Student?> GetStudent(int sId)
        {
            var restResponse = await GetStudentRestResponse(sId);
            return await restResponse.Content.ReadFromJsonAsync<Student?>();
        }

        public async Task<HttpResponseMessage> GetStudentRestResponse(int sId)
        {
            var restResponse = await _client.GetAsync($"student/{sId}");
            return restResponse;
        }
        /*
        // Update
        public async Task<bool> UpdateStudent(Student student)
        {
            var restResponse = await UpdateStudentRestResponse(student);
            return restResponse.IsSuccessful;
        }

        public async Task<RestResponse> UpdateStudentRestResponse(Student student)
        {
            var request = new RestRequest("student/").AddBody(student);
            var restResponse = await _restClient.ExecutePutAsync(request);
            return restResponse;
        }
        /*
        // Delete

        public async Task<bool> DeleteStudent(int sId)
        {
            var restResponse = await DeleteStudentRestResponse(sId);
            return restResponse.IsSuccessful;
        }

        public async Task<RestResponse> DeleteStudentRestResponse(int sId)
        {
            var request = new RestRequest("student/{sId}").AddUrlSegment("sId", sId);
            request.Method = Method.Delete;

            var restResponse = await _restClient.ExecuteAsync(request);
            return restResponse;
        }
        */
    }
}