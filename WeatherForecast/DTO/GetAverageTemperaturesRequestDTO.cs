using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WeatherForecastAPI.Converters;

namespace WeatherForecastAPI.DTO
{
    [BindProperties]
    public class GetAverageTemperaturesRequestDTO
    {
        /// <summary>
        /// List of city IDs.
        /// </summary>
        [Description("List of city IDs.")]
        public List<int>? CityIds { get; set; }

        /// <summary>
        /// Start time for temperature data. Please follow format MM/dd/yyyy HH:mm.
        /// </summary>
        [Description("Start time for temperature data.")]
        [Required]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// End time for temperature data. Please follow format MM/dd/yyyy HH:mm.
        /// </summary>
        [Description("End time for temperature data.")]
        [Required]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Whether to sort results in ascending order.
        /// </summary>
        [Description("Whether to sort results in ascending order.")]
        public bool Ascending { get; set; }
    }
}
