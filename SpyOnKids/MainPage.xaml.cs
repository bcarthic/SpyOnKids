using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SpyOnKids.Resources;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace SpyOnKids
{
    public partial class MainPage : PhoneApplicationPage
    {
        ObservableCollection<Kids>  kidsList = new ObservableCollection<Kids>();
        //public ObservableCollection<Kids> kidsList { get; set; }
        // Constructor
        Dictionary<String, JObject> kidDict = new Dictionary<String, JObject>();
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += (s, e) =>
            {
                loadList();
            };
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        async public void loadList()
        {
            kidsList.Clear();
            kidDict.Clear();
            string json = @"{
                  'Email': 'james@example.com',
                  'Active': true,
                  'CreatedDate': '2013-01-20T00:00:00Z'
                }";

            string jsonx = @"{
                  'Table1': [
                    {
                      'username': 0,
                      'imageurl': 'item 0',
                      'flag' : 'green'  
                    },
                    {
                      'id': 1,
                      'item': 'item 1',
                       'flag' : 'red'  
                    },
                    {
                      'id': 2,
                      'item': 'item 2',
                       'flag' : 'orange'  
                    }
                  ]
                }";
            string locationURL = "http://instagram-testing.herokuapp.com/activity";
            HttpClient client = new HttpClient();
            var getResult = await client.GetStringAsync(new Uri(locationURL, UriKind.RelativeOrAbsolute));
            JArray list = JArray.Parse(getResult);
            
            string id, item,flag;
            for (int i = 0; i < list.Count; i++)
            {
                JObject aItem = (JObject)list[i];
                //JObject location = (JObject)aItem["location"];
                id = (String)aItem["username"];

                item = "";

                flag = (String)aItem["status"];

                kidsList.Add(new Kids(id, item, getImageName(flag)));
               
                kidDict.Add(id, aItem);
       
            }
            followingList.ItemsSource = kidsList;
            
        }

        
        public string getImageName(string flag)
        {

            if (flag == "green")
                return "green.png";
            else if (flag == "red")
                return "red.png";
            else if (flag == "orange")
                return "orange.png";
            return "";
        }

        private void followingList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (followingList != null && followingList.SelectedItem != null)
            {
                var selectedItem = (Kids)followingList.SelectedItem;
                //followingList.SelectedItem = -1;
                var id = selectedItem.Id;
                App.ObjectNavigationData = kidDict[id];
                NavigationService.Navigate(new Uri("/Details.xaml?id=" + id, UriKind.Relative));
            }

        }
    }
    
}