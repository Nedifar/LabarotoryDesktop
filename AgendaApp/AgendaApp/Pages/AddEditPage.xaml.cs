using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AgendaApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgendaApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditPage : ContentPage
    {
        Models.Patient _patient = new Models.Patient();
        public AddEditPage(Models.Patient patient)
        {
            InitializeComponent();
            if (patient != null)
            {
                //dpDate.Date = _patient.BirhDate();
                _patient = patient;
            }
            this.BindingContext = _patient;
        }

        private async void clSave(object sender, EventArgs e)
        {
            //_patient.Date = dpDate.Date.ToShortDateString();
            string s = _patient.Date;
            while (5 > 3)
            {
                try
                {
                    using (var http = new HttpClient())
                    {
                        string con;
                        if (_patient.Id_Patient == 0)
                            con = "https://bsite.net/Gldfdsf/api/datacontroll/addpatient";
                        else
                            con = "https://bsite.net/Gldfdsf/api/datacontroll/editpatients";
                        var resp = await http.PostAsync(con, _patient, new JsonMediaTypeFormatter());
                        //var resp = await http.PostAsync("http://192.168.56.1:55125/api/datacontroll/postpacient", patient, new JsonMediaTypeFormatter());
                        resp.EnsureSuccessStatusCode();
                        

                            await Navigation.PopAsync();
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
    }
}