using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AppTempoAgora.Models
{
    public class Tempo
    {
        [JsonPropertyName("lon")]
        public double? Lon { get; set; }//Longitude

        [JsonPropertyName("lat")]
        public double? Lat { get; set; }//Latitude

        [JsonPropertyName("temp_min")]
        public double? TempMin { get; set; }//Temperatura mínima

        [JsonPropertyName("temp_max")]
        public double? TempMax { get; set; }//Temperatura máxima

        [JsonPropertyName("visibility")]
        public int? Visibility { get; set; }//Visibilidade

        [JsonPropertyName("speed")]
        public double? Speed { get; set; }//Velocidade do vento

        [JsonPropertyName("main")]
        public string? Main { get; set; }//Condição do clima

        [JsonPropertyName("description")]
        public string? Description { get; set; }//Descrição do clima

        [JsonPropertyName("sunrise")]
        public string? Sunrise { get; set; }//Nascer do Sol

        [JsonPropertyName("sunset")]
        public string? Sunset { get; set; }//Pôr do Sol
    }
}
