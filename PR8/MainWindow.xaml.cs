using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        private void UpdateWeather_Click(object sender, RoutedEventArgs e)
        {
            string city = CityTextBox.Text.Trim();

            if (string.IsNullOrEmpty(city))
            {
                MessageBox.Show("Пожалуйста, введите название города.");
                return;
            }

            await UpdateWeather(city);
            UpdateRequestCount();
        }
    }
}