using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
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

namespace Labarotory.Forms
{
    /// <summary>
    /// Логика взаимодействия для ExplorerPage.xaml
    /// </summary>
    public partial class ExplorerPage : UserControl
    {
        List<Models.ServiceOrders> orders = new List<Models.ServiceOrders>();
        int _id;
        public ExplorerPage(int id)
        {
            InitializeComponent();
            _id = id;
            AddcbService();
            dg.DataContext = orders;
        }

        private async void clBioAnalyzer(object sender, RoutedEventArgs e)
        {
            try
            {
                cbOrderBio.IsEnabled = false;
            using (var http = new HttpClient())
            {
                Models.ServiceOrders analy = (Models.ServiceOrders)(cbOrderBio.SelectedItem);
                var analyz = new analyz { patient = analy.Order.Id_Patient.ToString(), services = new List<service> { new service { serviceCode = analy.Id_Service } } };
                var json = JsonConvert.SerializeObject(analyz);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var url = "http://localhost:5000/api/analyzer/Biorad/";



                    var result = await http.PostAsync(url, data);
                    result.EnsureSuccessStatusCode();
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        analy.Analyzer = "Biorad";
                        analy.Accepted = "true";
                        analy.Status = "Sended";
                        analy.Id_User = _id;
                        orders.Add(analy);
                        dg.ItemsSource = orders;
                        HttpStatusCode code = new HttpStatusCode();
                        code = HttpStatusCode.OK;
                        while (code == HttpStatusCode.OK)
                        {
                            var answear = await http.GetAsync(url);
                            if (answear.StatusCode == HttpStatusCode.OK)
                            {
                                code = HttpStatusCode.OK;
                                var res = answear.Content.ReadAsStringAsync().Result;
                                if (res.Contains("progress"))
                                {
                                    var resa = answear.Content.ReadAsAsync<hey>().Result;
                                    orders.FindAll(s => s.PK_ServiceOrders == analy.PK_ServiceOrders).ForEach(x => x.Status = "Finished" + resa.progress + "%");
                                    dg.ItemsSource = orders;
                                    dg.Items.Refresh();
                                }
                                else
                                {
                                    var t = await answear.Content.ReadAsAsync<analyz1>();

                                    orders.FindAll(s => s.PK_ServiceOrders == analy.PK_ServiceOrders).ForEach(x => x.Result = t.services.FirstOrDefault().result.Replace(".", ","));
                                    orders.FindAll(s => s.PK_ServiceOrders == analy.PK_ServiceOrders).ForEach(x => x.Status = "Finished");
                                    orders.FindAll(s => s.PK_ServiceOrders == analy.PK_ServiceOrders).ForEach(x => x.Finished = DateTime.Now.ToString());
                                    dg.ItemsSource = orders;
                                    dg.Items.Refresh();
                                    Models.context.GetContext().SaveChanges();
                                }
                            }
                            else
                            {
                                code = HttpStatusCode.BadRequest;
                            }
                        }
                        Models.ServiceOrders orderss;
                        orderss = Models.context.GetContext().ServiceOrders.Where(p => p.Id_Order == analy.Id_Order && p.Status != "Finished").FirstOrDefault();
                        if (orderss == null)
                        {
                            Models.Order ord = Models.context.GetContext().Orders.Where(p => p.Id_Order == analy.Id_Order).FirstOrDefault();
                            ord.Status = "Finished";
                        }
                        Models.context.GetContext().SaveChanges();
                    }

                    AddcbService();
                }
            cbOrderBio.IsEnabled = true;
            }
                            catch
            {
                MessageBox.Show("Включите анализатор!");
                cbOrderBio.IsEnabled = true;
            }
        }
        
        class analyz
        {
            public string patient { get; set; }
            public List<service> services { get; set; } = new List<service>();
        }
        class service
        {
            public int serviceCode { get; set; }
        }

        class analyz1
        {
            public string patient { get; set; }
            public List<service1> services { get; set; } = new List<service1>();
        }
        class service1
        {
            public int serviceCode { get; set; }
            public string result { get; set; }
        }

        private async void clLedAnalyzer(object sender, RoutedEventArgs e)
        {
            try
            {
                cbOrderLed.IsEnabled = false;
                using (var http = new HttpClient())
                {
                    Models.ServiceOrders analy = (Models.ServiceOrders)cbOrderLed.SelectedItem;
                    var analyz = new analyz { patient = analy.Order.Id_Patient.ToString(), services = new List<service> { new service { serviceCode = analy.Id_Service } } };
                    var json = JsonConvert.SerializeObject(analyz);
                    var data = new StringContent(json, Encoding.UTF8, "application/json");
                    var url = "http://localhost:5000/api/analyzer/Ledetect/";
                    var result = await http.PostAsync(url, data);
                    result.EnsureSuccessStatusCode();
                    if (result.StatusCode == HttpStatusCode.OK)
                    {
                        analy.Analyzer = "Ledetect";
                        analy.Accepted = "true";
                        analy.Status = "Sended";
                        analy.Id_User = _id;
                        orders.Add(analy);
                        dg.ItemsSource = orders;
                        HttpStatusCode code = new HttpStatusCode();
                        code = HttpStatusCode.OK;
                        while (code == HttpStatusCode.OK)
                        {
                            var answear = await http.GetAsync(url);
                            if (answear.StatusCode == HttpStatusCode.OK)
                            {
                                code = HttpStatusCode.OK;
                                var res = answear.Content.ReadAsStringAsync().Result;
                                if (res.Contains("progress"))
                                {
                                    var resa = answear.Content.ReadAsAsync<hey>().Result;
                                    orders.FindAll(s => s.PK_ServiceOrders == analy.PK_ServiceOrders).ForEach(x => x.Status = "Finished" + resa.progress + "%");
                                    dg.ItemsSource = orders;
                                    dg.Items.Refresh();
                                }
                                else
                                {
                                    var t = await answear.Content.ReadAsAsync<analyz1>();
                                    orders.FindAll(s => s.PK_ServiceOrders == analy.PK_ServiceOrders).ForEach(x => x.Result = t.services.FirstOrDefault().result.Replace(".", ","));
                                    orders.FindAll(s => s.PK_ServiceOrders == analy.PK_ServiceOrders).ForEach(x => x.Status = "Finished");
                                    orders.FindAll(s => s.PK_ServiceOrders == analy.PK_ServiceOrders).ForEach(x => x.Finished = DateTime.Now.ToString());
                                    dg.ItemsSource = orders;
                                    dg.Items.Refresh();
                                    Models.context.GetContext().SaveChanges();
                                }
                            }
                            else
                            {
                                code = HttpStatusCode.BadRequest;
                            }
                        }
                        Models.ServiceOrders orderss;
                        orderss = Models.context.GetContext().ServiceOrders.Where(p => p.Id_Order == analy.Id_Order && p.Status != "Finished").FirstOrDefault();
                        if (orderss == null)
                        {
                            Models.Order ord = Models.context.GetContext().Orders.Where(p => p.Id_Order == analy.Id_Order).FirstOrDefault();
                            ord.Status = "Finished";
                        }
                        Models.context.GetContext().SaveChanges();

                    }
                    AddcbService();
                }
                cbOrderLed.IsEnabled = true;
            }
            catch
            {
                MessageBox.Show("Включите анализатор!");
                cbOrderLed.IsEnabled = true;
            }
        }
        private void AddcbService()
        {
            List<Models.ServiceOrders> f1 = new List<Models.ServiceOrders>();
            List<Models.ServiceOrders> f2 = new List<Models.ServiceOrders>();
            foreach (var a in Models.context.GetContext().ServiceOrders.Where(p => p.Accepted != "true").Include("Service").Include("Order"))
            {
                if (a.Service.Analyzers.Contains("Ledetect"))
                {
                    f1.Add(a);
                }
                if (a.Service.Analyzers.Contains("Biorad"))
                {
                    f2.Add(a);
                }
            }
            cbOrderBio.ItemsSource = f2;
            cbOrderLed.ItemsSource = f1;
        }
        class hey
        {
            public string progress { get; set; }
        }
    }
}
