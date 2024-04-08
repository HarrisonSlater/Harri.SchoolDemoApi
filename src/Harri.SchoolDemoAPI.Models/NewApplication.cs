/*
 * Swagger School ADMIN - OpenAPI 3.0
 *
 * Demo API about students, schools, and students applications to schools
 *
 * The version of the OpenAPI document: 1.0.0
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Harri.SchoolDemoAPI.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class NewApplication 
    {
        /// <summary>
        /// Gets or Sets SId
        /// </summary>
        /// <example>1234</example>
        [DataMember(Name="sId", EmitDefaultValue=true)]
        [JsonPropertyName("sId")]
        public int? SId { get; set; }

        /// <summary>
        /// Gets or Sets SchoolId
        /// </summary>
        /// <example>1001</example>
        [DataMember(Name="schoolId", EmitDefaultValue=true)]
        [JsonPropertyName("schoolId")]

        public int? SchoolId { get; set; }

        /// <summary>
        /// Gets or Sets Major
        /// </summary>
        /// <example>Computer Science</example>
        [DataMember(Name="major", EmitDefaultValue=false)]
        [JsonPropertyName("major")]
        public string? Major { get; set; }

        /// <summary>
        /// Gets or Sets Decision
        /// </summary>
        [DataMember(Name="decision", EmitDefaultValue=true)]
        [JsonPropertyName("decision")]
        public Decision? Decision { get; set; }

    }
}