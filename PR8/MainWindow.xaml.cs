using Newtonsoft.Json;
using PR8.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PR8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ApiKey = "992a69bc60315f46a2587400486e1f4f";
        private const string ApiUrl = "https://api.openweathermap.org/data/2.5/forecast?q={0}&appid={1}&units=metric&lang=ru";

        public MainWindow()
        {
            InitializeComponent();
            WeatherCache.InitializeDatabase();
        }

        private async void UpdateWeather_Click(object sender, RoutedEventArgs e)
        {
            string city = CityTextBox.Text.Trim();

            if (string.IsNullOrEmpty(city))
            {
                MessageBox.Show("Введите название города.");
                return;
            }

            
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string defaultCity = "Пермь";

        }

        private async Task<List<WeatherData>> FetchWeatherData(string city)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = string.Format(ApiUrl, city, ApiKey);
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Ошибка {response.StatusCode}: {response.ReasonPhrase}");
                }
                string responseBody = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<dynamic>(responseBody);

                var weatherList = new List<WeatherData>();

                foreach (var item in json.list)
                {
                    weatherList.Add(new WeatherData
                    {
                        DateTime = Convert.ToDateTime(item.dt_txt).ToString("dd.MM.yyyy HH:mm"),
                        Temperature = $"{item.main.temp} °C",
                        Pressure = $"{item.main.pressure} мм рт. ст.",
                        Humidity = $"{item.main.humidity}%",
                        WindSpeed = $"{item.wind.speed} м/с",
                        FeelsLike = $"{item.main.feels_like} °C",
                        WeatherDescription = item.weather[0].description.ToString(),
                    });
                }
                return weatherList;
            }
        }
    }
}