using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using AgendaApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgendaApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServicePage : ContentPage
    {
        private Patient _patient;
        public ServicePage()
        {
            InitializeComponent();
            this.BindingContext = this;
            Services();
        }
        private async void Services()
        {
            //while(5>3)
            //{
            //    try
            //    {
            using (var http = new HttpClient())
            {
                Service @service = new Service();
                        var resp = await http.GetAsync(new Uri("https://bsite.net/Gldfdsf/api/datacontroll/getservices"));
                        //var resp = await http.GetAsync(new Uri("http://192.168.56.1:55125/api/datacontroll/getservices"));
                        //resp.EnsureSuccessStatusCode();
                        var rr = await resp.Content.ReadAsAsync<ObservableCollection<Service>>();
                        cvSchedule.ItemsSource = @service.MyServices(rr);
            }
            //        break;
            //    }
            //    catch
            //    {
            //        bool result = await DisplayAlert("Connection Failed", "Check your internet connection!", "Try again", "Cancel");
            //        if (result)
            //        {
            //            continue;
            //        }
            //        else
            //            Environment.Exit(0);
            //    }
            //}
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}