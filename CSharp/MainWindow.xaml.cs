//This must be downloaded to project from NuGet
using Newtonsoft.Json;

//You have to add this referece to the project
using System.Web;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows;

namespace SendReceiveData
{
    class Girl
    {
        public string Name { get; set; }
        public string Car { get; set; }
        public string Country { get; set; }
    }
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnGetString_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(new Uri("http://localhost:80")))
                {
                    using (HttpContent content = response.Content)
                    {
                        HttpContentHeaders headers = content.Headers;
                        string mycontent = await content.ReadAsStringAsync();
                        this.labelA.Content = mycontent;
                    }
                }
            }

                
        }

        private async void btnSendString_Click(object sender, RoutedEventArgs e)
        {
            string strx = "C SHARP";
            HttpContent content = new StringContent(strx, Encoding.UTF8, "text/plain");
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsync(new Uri("http://localhost:80/getstring"), content))
                {
                    
                }
            }
        }

        private async void btnGetJson_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(new Uri("http://localhost:80/sendjson")))
                {
                    using (HttpContent content = response.Content)
                    {
                        HttpContentHeaders headers = content.Headers;
                        string mycontent = await content.ReadAsStringAsync();
                        var jsonx = JsonConvert.DeserializeObject<Girl>(mycontent);
                        this.labelA.Content = jsonx.Name;
                    }
                }
            }
        }

        private async void btnSendJson_Click(object sender, RoutedEventArgs e)
        {
            var xx = new Girl();
            xx.Name = "Nuria";
            xx.Car = "Ferrari";
            xx.Country = "Spain";

            string jsonGirl = JsonConvert.SerializeObject(xx);

            HttpContent content = new StringContent(jsonGirl, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsync(new Uri("http://localhost:80/getjson"), content))
                {
                    
                }
            }
        }

        private async void btnGetJsonArray_Click(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(new Uri("http://localhost:80/sendjsonarray")))
                {
                    using (HttpContent content = response.Content)
                    {
                        HttpContentHeaders headers = content.Headers;
                        string mycontent = await content.ReadAsStringAsync();
                        var jsonx = JsonConvert.DeserializeObject<Girl[]>(mycontent);
                        var sb = new StringBuilder();
                        for (int i = 0; i < jsonx.Length; i++)
                        {
                            sb.Append(jsonx[i].Name);
                            sb.Append(" ");
                            sb.Append(jsonx[i].Car);
                            sb.Append(" ");
                            sb.Append(jsonx[i].Country);
                            sb.Append("\n");
                        }
                        this.labelA.Content = sb.ToString();
                    }
                }
            }
        }

        private async void btnSendJsonArray_Click(object sender, RoutedEventArgs e)
        {
            var xx = new Girl();
            xx.Name = "Nuria";
            xx.Car = "Ferrari";
            xx.Country = "Spain";

            var zz = new Girl();
            zz.Name = "Isabel";
            zz.Car = "BMW";
            zz.Country = "Spain";

            var girls = new List<Girl>();
            girls.Add(xx);
            girls.Add(zz);

            string jsonGirls = JsonConvert.SerializeObject(girls);

            HttpContent content = new StringContent(jsonGirls, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsync(new Uri("http://localhost:80/getjsonarray"), content))
                {

                }
            }
        }

        private async void btnSendForm_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Name", "Natalia"),
                new KeyValuePair<string, string>("Car", "Porsche")

            };
            HttpContent content = new FormUrlEncodedContent(queries);
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.PostAsync(new Uri("http://localhost:80/getform"), content))
                {

                }
            }
        }

        private async void btnSendQueryString_Click(object sender, RoutedEventArgs e)
        {
            UriBuilder ub = new UriBuilder();
            ub.Scheme = "http";
            ub.Host = "localhost";
            ub.Port = 80;
            ub.Path = "getquerystring";
            //Uri myuri = ub.Uri;
            var query = HttpUtility.ParseQueryString(ub.Query);
            query["Name"] = "Marta";
            query["Car"] = "Volvo";
            ub.Query = query.ToString();
            var myurl = ub.ToString();

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(new Uri(myurl)))
                {
                    
                }
            }
        }
    }

    
}