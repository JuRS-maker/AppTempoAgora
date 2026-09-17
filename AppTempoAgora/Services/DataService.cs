using AppTempoAgora.Models;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net;

namespace AppTempoAgora.Services
{
    internal class DataService
    {
        public static async Task<Tempo?>GetPrevisao(string cidade) 
        {
            Tempo? t = null;

            string chave = "381f5ab8778b4901fc3e0dd07858c3ed";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&units=metric&appid={chave}";

            using (HttpClient client = new HttpClient())
            {
                try { 
                    HttpResponseMessage resp = await client.GetAsync(url);

                    if (resp.IsSuccessStatusCode)
                    {
                        string json = await resp.Content.ReadAsStringAsync();

                        var rascunho = JObject.Parse(json);

                        DateTime time = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                        DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                     t = new()
                        {
                            Lat = (double)rascunho["coord"]["lat"],
                            Lon = (double)rascunho["coord"]["lon"],
                            Description = (string)rascunho["weather"][0]["description"],
                            Main = (string)rascunho["weather"][0]["main"],
                            TempMin = (double)rascunho["main"]["temp_min"],
                            TempMax = (double)rascunho["main"]["temp_max"],
                            Speed = (double)rascunho["wind"]["speed"],
                            Visibility = (int)rascunho["visibility"],
                            Sunrise = sunrise.ToString(),
                            Sunset = sunset.ToString(),
                        }; //Fecha o objeto do tempo
                    }// Fecha if se o status do servidor foi de sucesso
                    else if(resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        //Throw - usado para lançar na tela do usuário quando o sistema detecta um erro.
                        throw new Exception("Cidade não encontrada. Verifique o nome digitado.");
                    }
                    else
                    {
                        //Throw - usado para lançar na tela do usuário quando há um erro na API.
                        throw new Exception("Erro na API: {resp.StatusCode}");
                    }
                }
                catch (HttpRequestException) //Ausência de conxão com a rede
                {
                    throw new Exception("Sem conexão com a internet. Verifique sua rede e tente novamento.");
                }
            }// Fecha laço using
            
            return t;
        }
    }
}
