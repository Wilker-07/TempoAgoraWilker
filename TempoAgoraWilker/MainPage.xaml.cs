using System.Threading.Tasks;
using TempoAgoraWilker.Models;
using TempoAgoraWilker.Services;
using Microsoft.Maui.Networking;

namespace TempoAgoraWilker
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
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    SemCidade? se = await DataService.GetCidade(txt_cidade.Text);
                    if (se != null)
                    {
                        string sem_previsao = "";
                        sem_previsao = $"Mensagem:  {se.message} \n";
                        lbl_res.Text = sem_previsao;



                    }
                    else
                    {
                        await DisplayAlert("Ops", "Cidade não encontrada!", "OK");
                    }
                }
                else
                {
                    lbl_res.Text = "Cidade não encontrada!";
                }

                if (! string.IsNullOrEmpty(txt_cidade.Text) )
                {
                    
                    Tempo? t = await DataService.Getprevisao(txt_cidade.Text);
                    if( t != null )
                    {
                        string dados_previsão = "";

                        dados_previsão = $"Latitude: {t.lat} \n" +
                                         $"Longitude: {t.lon} \n" +
                                         $"Nascer do sol: {t.sunrise} \n" +
                                         $"Por do sol: {t.sunset} \n" +
                                         $"Temp Máx: {t.temp_max} \n" +
                                         $"Temp Min: {t.temp_min} \n" +
                                         $"Velocidade do Vento: {t.speed} \n" +
                                         $"Descrição do clima: {t.description} \n" +
                                         $"Visibilidade: {t.visibility} \n";
                                         




                        lbl_res.Text = dados_previsão;
                        
                    }
                    else
                    {
                        lbl_res.Text = "Sem Dados de Previsão";
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha o campo Buscar Cidade!";
                }
                    
            }catch (Exception ex)
            {
                await DisplayAlert("Sem conexão!", "Você está sem acesso à internet", "OK");
            }
        }
    }

}
