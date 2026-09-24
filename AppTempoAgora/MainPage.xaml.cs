using AppTempoAgora.Models;
using AppTempoAgora.Services;
using System.Diagnostics;

namespace AppTempoAgora
{
    public partial class MainPage : ContentPage
    {
   
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked_Previsao(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if(t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"Latitude: {t.Lat} \n" +
                                         $"Longitude: {t.Lon} \n" +
                                         $"Nascer do Sol: {t.Sunrise} \n" +
                                         $"Pôr do Sol: {t.Sunset} \n" +
                                         $"Temperatura Máxima: {t.TempMax}°C \n" +
                                         $"Temperatura Mínima: {t.TempMin}°C \n\n" +
                                         $"Velocidade do Vento: {t.Speed} m/s \n" +
                                         $"Visibilidade: {t.Visibility} metros \n" +
                                         $"Descrição do clima: {t.Description} \n";

                        lbl_res.Text = dados_previsao;

                        string mapa = $"https://embed.windy.com/embed.html?" +
                                      $"type=map&location=coordinates&metricRain=mm&metricTemp=°C" +
                                      $"&metricWind=km/h&zoom=5&overlay=wind&product=ecmwf&level=surface" +
                                      $"&lat={t.Lat.ToString().Replace(",", ".")}&lon={t.Lon.ToString().Replace(",", ".")}";
                        
                        wv_mapa.Source = mapa;

                        Debug.WriteLine(mapa);
                    }
                    else
                    {
                        lbl_res.Text = "Não foi possível obter a previsão do tempo.";
                    }

                }
                else 
                {
                    lbl_res.Text = "Preencha a cidade";
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void Button_Clicked_Localizacao(object sender, EventArgs e)
        {
            try 
            {
                GeolocationRequest request = 
                    new GeolocationRequest
                    (
                        GeolocationAccuracy.Medium,
                        TimeSpan.FromSeconds(10)
                    );

                Location? local = await Geolocation.Default.GetLocationAsync(request);
                
                if(local != null)
                {
                    string local_disp = $"Latitude: {local.Latitude} \n" +
                                        $"Longitude: {local.Longitude} \n";

                    lbl_coords.Text = local_disp;

                    //Pega o nome da cidade que está na coordenada
                    GetCidade(local.Latitude, local.Longitude);

                } else
                {
                    lbl_coords.Text = "Nenhuma localização";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Erro: Recurso não suportado no dispositivo.", fnsEx.Message, "OK");
            }
            catch (FeatureNotEnabledException fneEx) 
            {
                await DisplayAlert("Erro: Recurso não habilitado no dispositivo.", fneEx.Message, "OK");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Erro:Recurso permitido no dispositivo.", pEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro!", ex.Message, "OK");
            }
        }

        private async void GetCidade(double Lat, double Lon)
        {
            try
            {
                IEnumerable<Placemark> places = await Geocoding.Default.GetPlacemarksAsync(Lat, Lon);

                Placemark? place = places.FirstOrDefault();

                if (place != null)
                {
                    txt_cidade.Text = place.Locality;
                }
            } 
            catch (Exception ex)
            {
                await DisplayAlert("Erro: Obtenção do nome da cidade", ex.Message, "OK");
            }
        }   

    }
}
