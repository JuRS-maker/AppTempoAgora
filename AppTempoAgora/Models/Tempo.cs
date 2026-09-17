using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppTempoAgora.Models
{
    public class Tempo
    {
        [JsonPropertyName("lon")]
        public double? Lon { get; set; }

        [JsonPropertyName("lat")]
        public double? Lat { get; set; }

        [JsonPropertyName("temp_min")]
        public double? TempMin { get; set; }

        [JsonPropertyName("temp_max")]
        public double? TempMax { get; set; }

        [JsonPropertyName("visibility")]
        public int? Visibility { get; set; }

        [JsonPropertyName("speed")]
        public double? Speed { get; set; }

        [JsonPropertyName("main")]
        public string? Main { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("sunrise")]
        public string? Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public string? Sunset { get; set; }
    }
}
