using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AdminMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class About : ContentPage
    {
        public About()
        {
            InitializeComponent();
            using(var http = new HttpClient())
            {
                var res = http.GetAsync($"http://drankmin-001-site1.htempurl.com/api/lastdance/allIzm");
                var response = res.Result.Content.ReadAsStringAsync();
                grip.Text = response.Result;
            }
        }
    }
}