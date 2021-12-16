using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    /// Логика взаимодействия для Accountant.xaml
    /// </summary>
    public partial class Accountant : UserControl
    {
        public Accountant()
        {
            InitializeComponent();
        }

        private void clReport(object sender, RoutedEventArgs e)
        {
            DateReport dateReport = new DateReport(true);
            dateReport.Show();
        }

        private void clickViewPDF(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start($"{AppDomain.CurrentDomain.BaseDirectory}Orders/result{((sender as Button).DataContext as PDF).path.Replace("Заказ ", "")}.pdf");
        }

        private void clviewList(object sender, RoutedEventArgs e)
        {
            List<PDF> pDFs = new List<PDF>();
            string[] vs = Directory.GetFiles($"{AppDomain.CurrentDomain.BaseDirectory}Orders", "*.pdf").Select(System.IO.Path.GetFileName).ToArray();
            foreach (string vss in vs)
            {
                
                pDFs.Add(new PDF { path = vss.Substring(0, vss.Length-4).Replace("result", "Заказ ") });
            }
                lw.ItemsSource = pDFs;
        }
        class PDF
        {
            public string path { get; set; }
        }
    }
}
