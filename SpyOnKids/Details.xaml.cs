using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json.Linq;
using Microsoft.Phone.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Device.Location;
using Microsoft.Phone.Maps.Controls;

namespace SpyOnKids
{
    public partial class Details : PhoneApplicationPage
    {
        string name = "Karthik";
        string number = "3126475547";
        double lat;
        double lng;
        public Details()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                loadMap();
            };
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string id = NavigationContext.QueryString["id"];
            JObject aItem = (JObject)App.ObjectNavigationData; ;
            string imageStr;
            pageName.Text = id;
            imageStr = (String)aItem["image"]["standard_resolution"]["url"];

            JObject location = (JObject)aItem["location"];
            lat = (double)location["latitude"];
             lng = (double)location["longitude"];
            
            BitmapImage image = new BitmapImage(new Uri(imageStr, UriKind.Absolute));
            currentImage.Width = 200;
            currentImage.Height = 200;
            currentImage.Source = image;  
  
               
       }

        private void makePin( double lat, double lng)
        {


            Uri uri = new Uri("pushpinred.png", UriKind.RelativeOrAbsolute);
            MapOverlay overlay = new MapOverlay
            {
                GeoCoordinate = new GeoCoordinate(lat, lng),
                /*Content =  new System.Windows.Shapes.Ellipse
                {
                    Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue),
                    Width = 15,
                    Height = 15
                    
                }*/
                Content = new Image
                {
                    Source = new BitmapImage(uri),
                    Width = 30,
                    Height = 30

                }

            };
            MapLayer layer = new MapLayer();
            layer.Add(overlay);

            kidMap.Layers.Add(layer);
        }
        public void loadMap()
        {
            kidMap.SetView(new GeoCoordinate(lat, lng), 12);
            makePin(lat, lng);
        }

        private void SMS_Click(object sender, RoutedEventArgs e)
        {
            SmsComposeTask smsComposeTask = new SmsComposeTask();

            smsComposeTask.To = number;
            smsComposeTask.Body = "Kiddo! Where are you??. I didn't get any message from for last 7 hours";
            
            smsComposeTask.Show();
        }

        private void Call_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PhoneCallTask phoneCallTask = new PhoneCallTask();

                phoneCallTask.PhoneNumber = number;
                phoneCallTask.DisplayName = name;

                phoneCallTask.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Not able to Call");
            }

        }
    }
}