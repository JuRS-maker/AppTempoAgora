using AppTempoAgora.Models;
using AppTempoAgora.Services;

namespace AppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
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
                                         $"Temperatura Mínima: {t.TempMin}°C ";
                                           

                        lbl_res.Text = dados_previsao;
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
    }
}
