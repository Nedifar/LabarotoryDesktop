using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Labarotory.Forms
{
    /// <summary>
    /// Логика взаимодействия для AdministratorPage.xaml
    /// </summary>
    public partial class AdministratorPage : UserControl
    {
        public AdministratorPage()
        {
            InitializeComponent();
            dgHistorySign.ItemsSource = Models.context.GetContext().Histories.ToList();
            cbLoginFiltr.ItemsSource = Models.context.GetContext().Histories.ToList();
            cbServiceFilter.ItemsSource = Models.context.GetContext().Services.ToList();
        }

        private void clReport(object sender, RoutedEventArgs e)
        {
            DateReport dateReport = new DateReport(false);
            dateReport.Show();
        }

        private void clQuality(object sender, RoutedEventArgs e)
        {
            dgHistorySign.Visibility = Visibility.Collapsed;
            gr.Visibility = Visibility.Visible;
            Models.Service serv = cbServiceFilter.SelectedItem as Models.Service;
      
            int count = 0;
            double sum = 0;
            List<double> arr = new List<double>();
            foreach(Models.ServiceOrders a in Models.context.GetContext().ServiceOrders.Where(p=>p.Id_Service == serv.Id_Service))
            {
                try
                {
                    double b = Double.Parse(a.Result);
                    arr.Add(b);
                    sum += b;
                    count++;
                }
                catch
                { }
            }
            double res = 0;
            double avg = sum / count;
            foreach (double n in arr)
            {
                res += Math.Pow(n - avg, 2);  
            }
            double s = Math.Sqrt(res/avg);
            double koef = (s / avg) * 100;
            Models.Exp l = new Models.Exp
            {
                Date = DateTime.Now.ToString(),
                avg = avg,
                Id_Service = serv.Id_Service,
                s = s
            };
            Models.context.GetContext().Exps.Add(l);
            Models.context.GetContext().SaveChanges();
            Chart chart = new Chart();
            gr.Child = chart;
            chart.ChartAreas.Add(new ChartArea("Function"));
            Series series = new Series("Points");
            series.ChartType = SeriesChartType.Line;
            series.ChartArea = "Function";
            foreach(Models.Exp exp1 in Models.context.GetContext().Exps.Where(b=>b.Id_Service == serv.Id_Service))
            {
                series.Points.AddXY(exp1.Date, exp1.s);
            }
            chart.Series.Add(series);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void clHistory(object sender, RoutedEventArgs e)
        {
            dgHistorySign.Visibility = Visibility.Visible;
            gr.Visibility = Visibility.Collapsed;
        }
    }
}
