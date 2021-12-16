using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AgendaApp.Models;
using System.Collections.ObjectModel;
using System.Net.Http.Formatting;
using System.IO;

namespace AgendaApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {
        private Patient _patient;
        HttpClient http = new HttpClient();
        public NewsPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            CheckSign();
            News();
        }

        private async void News()
        {
            while (5 > 3)
            {
                try
                {
                    using (var http = new HttpClient())
                    {
                        New @new = new New();
                        var resp = await http.GetAsync(new Uri("https://bsite.net/Gldfdsf/api/datacontroll/getnews"));
                        //var resp = await http.GetAsync(new Uri("http://192.168.56.1:55125/api/datacontroll/getnews"));
                        //resp.EnsureSuccessStatusCode();
                        var rr = await resp.Content.ReadAsAsync<ObservableCollection<New>>();
                        cvSchedule.ItemsSource = @new.MyNews(rr);
                    }
                    break;
                }
                catch
                {
                    bool resault = await DisplayAlert("Connection Failed", "Check your internet connection!", "Try again", "Cancel");
                    if (resault)
                    {
                        continue;
                    }
                    else
                        Environment.Exit(0);
                }
            }
        }

        private async void clSign(object sender, EventArgs e)
        {
            
            while (5 > 3)
            {
                try
                {
                    using (var http = new HttpClient())
                    {
                        string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}Data.txt";
                        Service @service = new Service();                        
                        Patient patient = new Patient { Login = eLogin.Text, Password = ePass.Text };
                        var resp = await http.PostAsync("https://bsite.net/Gldfdsf/api/datacontroll/postpacient", patient, new JsonMediaTypeFormatter());
                        //var resp = await http.PostAsync("http://192.168.56.1:55125/api/datacontroll/postpacient", patient, new JsonMediaTypeFormatter());
                        resp.EnsureSuccessStatusCode();
                        var rr = await resp.Content.ReadAsAsync<Patient>();
                        if (rr == null)
                        {
                            await DisplayAlert("Sign failed", "Check your entered data", "Ok");
                        }
                        else
                        {
                            _patient = rr;
                            sign();
                            using(StreamWriter sw = new StreamWriter(path, false))
                            {
                                sw.WriteLine(eLogin.Text + "\n" + ePass.Text);
                            }
                            RegBt.Source = "exit.png";
                            eLogin.Text = String.Empty;
                            ePass.Text = String.Empty;
                        }
                    }
                    break;
                }
                catch
                {
                    bool result = await DisplayAlert("Connection Failed", "Check your internet connection!", "Try again", "Cancel");
                    if (result)
                    {
                        continue;
                    }
                    else
                        Environment.Exit(0);
                }
            }
        }
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEditPage(_patient));
        }
        public void sign()
        {
            lWelcome.Text = "Hi, " + _patient.Name;
            laySigned.IsVisible = true;
            laySign.IsVisible = false;
            EditBt.IsVisible = true;
        }

        private void RegClicked_Clicked(object sender, EventArgs e)
        {
            if(RegBt.Source.ToString() == "File: exit.png")
            {
                string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}Data.txt";
                using (StreamWriter sw = new StreamWriter(path, false))
                {
                    sw.WriteLine("");
                }
                laySigned.IsVisible = false;
                laySign.IsVisible = true;
                RegBt.Source = "reg.png";
                EditBt.IsVisible = false;

            }
            else
            {
                Navigation.PushAsync(new AddEditPage(null));
            }
        }

        private void CheckSign()
        {
            string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}Data.txt";
            string data;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                    data = sr.ReadToEnd();
                if (data != "")
                {
                    string[] mas = data.Split('\n');
                    eLogin.Text = mas[0];
                    ePass.Text = mas[1];
                    clSign(null, null);
                }
            }   
            catch
            {
                System.IO.File.WriteAllText(path, "");
            }
        }
    }
}