using Newtonsoft.Json.Linq;
using TempoAgoraWilker.Models;

namespace TempoAgoraWilker.Services
{
    public class DataService
    {
       public static async Task<SemCidade?> GetCidade(string cidade)
        {
            
            SemCidade? se = null;

            string chave = "221654780700d3c13ee8c7816a859202";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&units=metric&appid=221654780700d3c13ee8c7816a859202";



            using (HttpClient client = new HttpClient())

            {
                HttpResponseMessage resp = await client.GetAsync(url);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(json);
                        
                    se = new()
                    {
                        message = (string)rascunho["massage"],
                    };
                    // Objeto do semcidade fechado
                } // If fechado se o status do server deu certo
            } // Fecha o using


            return se;
        }
        
        public static async Task<Tempo?> Getprevisao(string cidade)
        {
            Tempo? t = null;


            string chave = "221654780700d3c13ee8c7816a859202";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&units=metric&appid=221654780700d3c13ee8c7816a859202";

           

            using (HttpClient client = new HttpClient())

            {
                HttpResponseMessage resp = await client.GetAsync(url);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(json);

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                    t = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],
                        speed = (double)rascunho["wind"]["speed"],
                        sunrise = sunrise.ToString(),
                        sunset = sunset.ToString(),
                        temp_max = (double)rascunho["main"]["temp_max"],
                        temp_min = (double)rascunho["main"]["temp_min"],
                        visibility = (int)rascunho["visibility"],
                        

                    };
                    
                    // Objeto do tempo fechado
                } // If fechado se o status do server deu certo
            } // Fecha o using


            return t;
        }
    }
}
