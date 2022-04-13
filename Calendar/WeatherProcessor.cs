using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Calendar
{
    class WeatherProcessor
    {




        public static async Task<WeatherInfo.root> LoadWeather(string City)
        {
            string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", City, ApiKeys.ApiKey);

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    WeatherInfo.root weather = await response.Content.ReadAsAsync<WeatherInfo.root>();

                    return weather;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }



        }

    }
}
