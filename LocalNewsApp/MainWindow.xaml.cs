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
using System.Threading;
using LocalNews;

namespace LocalNewsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Weather weather;
        Display disp=new Display();
        object TempRadioSender;
        object DistRadioSender;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                //this.Activated += new EventHandler(SearchNewsGotFocus);
                //this.Deactivated += new EventHandler(SearchNewsGotFocus);
                

                Location current = LocalNews.Program.getCurrentLocation();
                String city = current.getCity();
                String region = current.getRegion();
                weather = LocalNews.Program.getCurrentWeather(city, region);
                disp.location = city + ", " + region;
                disp.humidity = weather.getHumidity();
                disp.update = weather.getUpdateTime();
                disp.icon = weather.getIconURL();
                UpdateFahrenheitData();
                UpdateMilesData();
                this.DataContext = disp;
            }
            catch (Exception)
            {
                MessageBox.Show("Check your internet connection");
            }
        }

        private void TempRadioChecked(object sender, RoutedEventArgs e)
        {
            TempRadioSender = sender;
            this.DataContext = null;
            var btn = sender as RadioButton;
            String radiobtnName = btn.Content.ToString();

            switch (radiobtnName)
            {
                case "Fahrenheit": if (disp != null && weather!=null)
                    {
                        UpdateFahrenheitData();
                        this.DataContext = disp;
                    }
                    break;

                case "Centigrade": UpdateCentigradeData();
                    this.DataContext = disp;
                                   break;
            }
        }

        private void UpdateFahrenheitData()
        {
            disp.temp = (int)Double.Parse(weather.getTempF()) + "F";
            disp.feelslike = (int)Double.Parse(weather.getFeelslikeF()) + " F";
        }

        private void UpdateCentigradeData()
        {
            disp.temp = (int)Double.Parse(weather.getTempC()) + "C";
            disp.feelslike = (int)Double.Parse(weather.getFeelslikeC()) + " C";
        }

        private void DistRadioChecked(object sender, RoutedEventArgs e)
        {
            DistRadioSender = sender;
            this.DataContext = null;
            var btn = sender as RadioButton;
            String radiobtnName = btn.Content.ToString();

            switch (radiobtnName)
            {
                case "Miles": if (disp != null && weather != null)
                    {
                        UpdateMilesData();
                        this.DataContext = disp;
                    }
                    break;
                case "Kilometers": UpdateKMData();
                    this.DataContext = disp;
                    break;

            }
        }

        private void UpdateMilesData()
        {
            disp.windspeed = weather.getWindMPH() + " mph";
            disp.visibility = weather.getVisibilityinMiles() + " miles";
        }

        private void UpdateKMData()
        {
            disp.windspeed = weather.getWindKPH() + " kph";
            disp.visibility = weather.getVisibilityinKM() + " km";
        }

        private void RefreshWeather(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DataContext = null;
                Location current = LocalNews.Program.getCurrentLocation();
                String city = current.getCity();
                String region = current.getRegion();
                weather = LocalNews.Program.getCurrentWeather(city, region);
                disp.location = city + ", " + region;
                disp.humidity = weather.getHumidity();
                disp.update = weather.getUpdateTime();
                disp.icon = weather.getIconURL();
                TempRadioChecked(TempRadioSender, null);
                DistRadioChecked(DistRadioSender, null);
                UpdateMilesData();
                this.DataContext = disp;
            }
            catch (Exception)
            {
                MessageBox.Show("Check your internet connection");
            }
        }

        private void SearchNewsGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.Foreground = Brushes.Black;
            //tb.GotFocus -= SearchNewsGotFocus;
        }

        private void GetNews(object sender, RoutedEventArgs e)
        {
            String search_str = SearchText.Text;
            List<News> NewsList = LocalNews.Program.getNews(search_str);
            this.DataContext = null;
            //NewsItems news = new NewsItems();

            if (NewsList.Count() >= 1)
            {
                disp.headline1 = NewsList[1].headline;
                disp.url1 = NewsList[1].url;
                disp.bullet_color1 = "Blue";
            }
            else
            {
                disp.headline1 = "";
                disp.url1 = "";
            }

            if (NewsList.Count() >= 2)
            {
                disp.headline2 = NewsList[2].headline;
                disp.url2 = NewsList[2].url;
                disp.bullet_color2 = "Blue";
            }
            else
            {
                disp.headline2 = "";
                disp.url2 = "";
            }

            if (NewsList.Count() >= 3)
            {
                disp.headline3 = NewsList[3].headline;
                disp.url3 = NewsList[3].url;
                disp.bullet_color3 = "Blue";
            }
            else
            {
                disp.headline3 = "";
                disp.url3 = "";
            }

            if (NewsList.Count() >= 4)
            {
                disp.headline4 = NewsList[4].headline;
                disp.url4 = NewsList[4].url;
                disp.bullet_color4 = "Blue";
            }
            else
            {
                disp.headline4 = "";
                disp.url4 = "";
            }

            if (NewsList.Count() >= 5)
            {
                disp.headline5 = NewsList[5].headline;
                disp.url5 = NewsList[5].url;
                disp.bullet_color5 = "Blue";
            }
            else
            {
                disp.headline5 = "";
                disp.url5 = "";
            }
            this.DataContext = disp;
        }

        private void SearchNewsLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (tb.Text == "")
            {
                tb.Text = "Search News";
                tb.Foreground = Brushes.Gray;
                //tb.LostFocus -= SearchNewsLostFocus;
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {

        }


    }
}
