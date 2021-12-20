using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AdminMobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new About());
        }

        private async void ff(object sender, EventArgs e)
        {
            using (var http = new HttpClient())
            {
                var res = await http.GetAsync($"http://drankmin-001-site1.htempurl.com/api/lastdance/addFormat/{addFormat.Text}");
                res.EnsureSuccessStatusCode();
                await DisplayAlert("Отлично", "Все хорошо", "Ok");
            }
        }

        private async void ss(object sender, EventArgs e)
        {
            using (var http = new HttpClient())
            {
                var res = await http.GetAsync($"http://drankmin-001-site1.htempurl.com/api/lastdance/DeleteIzm/{deleteIzm.Text}");
                res.EnsureSuccessStatusCode();
                await DisplayAlert("Отлично", "Все хорошо", "Ok");
            }
        }
    }
}
