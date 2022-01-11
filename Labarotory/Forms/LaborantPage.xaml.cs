using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
using MaterialDesignThemes;
using System.IO;
using System.Globalization;

namespace Labarotory.Forms
{
    /// <summary>
    /// Логика взаимодействия для LaborantPage.xaml
    /// </summary>
    public partial class LaborantPage : UserControl
    {
        Models.Order newOrder = new Models.Order();
        List<Models.ServiceOrders> serv = new List<Models.ServiceOrders>();
        public LaborantPage()
        {
            InitializeComponent();
            cbService.ItemsSource = Models.context.GetContext().Services.ToList();
            cbSearc.ItemsSource = new List<string> { "Пациент", "Услуги" };
        }

        private void tbid_Material_GotFocus(object sender, RoutedEventArgs e)
        {
            if(tbid_Material.Text.Length == 0)
            {
                try
                {
                    tbid_Material.Text = (Models.context.GetContext().Orders.OrderByDescending(p => p.Id_Order).FirstOrDefault().Id_Order + 1).ToString();
                }
                catch
                {
                    tbid_Material.Text = "1";
                }
                tbid_Material.SelectAll();
            }
        }

        private void convertStrix(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            tbid_Material.Text += $"{DateTime.Now.Day}"+$"{DateTime.Now.Month}" +$"{DateTime.Now.Year}" + $"{random.Next(100000, 999999)}";
            strix();
        }
        private double Draw(int width, double margin)
        {
            System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle();
            SolidColorBrush solid = new SolidColorBrush();
            solid.Color = System.Windows.Media.Color.FromRgb(0, 0, 0);
            rect.Fill = solid;
            rect.Height = 5 * 10;
            rect.Width = width * 0.15 * 20;
            if (width == 0)
            {
                return margin + 1.55;
            }
            Canvas.SetLeft(rect, margin);
            Canvas.SetTop(rect, 0);
            canva.Children.Add(rect);
            return margin + rect.Width + 0.2 * 20;
        }
        private void strix()
        {
            double margin = 3.63 * 20;
            for (int i = 0; i < tbid_Material.Text.Length; i++)
            {
                margin = Draw((int)Char.GetNumericValue(tbid_Material.Text[i]), margin);
            }
            TextBlock tb = new TextBlock();
            tb.Text = tbid_Material.Text;
            Canvas.SetLeft(tb, 3.63 * 20);
            Canvas.SetTop(tb, 60);
            canva.Children.Add(tb);
            try
            {
                PrintDialog dialog = new PrintDialog();

                if (dialog.ShowDialog() != true)
                    return;
                dialog.PrintVisual(canva, "IFMS Print Screen");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Print Screen", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void readUSB(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            tbid_Material.Text = rnd.Next(1000000, 9999999).ToString();
            tbid_Material.Text += rnd.Next(100000, 999999).ToString();
        }

        private void addNewLabelForService(object sender, RoutedEventArgs e)
        {
            ComboBox combo = new ComboBox();
            spServices.Children.Add(combo);
            combo.DisplayMemberPath = "Name";
            combo.ItemsSource = Models.context.GetContext().Services.ToList();
        }

        private void clFinishOrder(object sender, RoutedEventArgs e)
        {
            //try
            //{
                double price = 0;
                Models.Blood blood = new Models.Blood();
                blood.Barcode = tbid_Material.Text;
                Models.context.GetContext().Bloods.Add(blood);
                Models.context.GetContext().SaveChanges();
                //.Id_Order = Models.context.GetContext().Orders.OrderByDescending(p => p.Id_Order).FirstOrDefault().Id_Order + 1;
                newOrder.Blood = Models.context.GetContext().Bloods.Where(p => p.Barcode == blood.Barcode).FirstOrDefault();
                newOrder.Patient = Models.context.GetContext().Patients.Where(p => p.Full_Name == Name.Text).FirstOrDefault();
                newOrder.DateOrder = DateTime.Now.ToShortDateString();
                newOrder.ServicesOrders.AddRange(serv);
                foreach (ComboBox box in spServices.Children)
                {
                    serv.Add(new Models.ServiceOrders { Service = (Models.Service)(box).SelectedItem , Id_Order = newOrder.Id_Order});
                }
                foreach (var pr in serv)
                {
                    price += pr.Service.Price;
                    pr.Id_Order = Models.context.GetContext().Orders.OrderByDescending(p => p.Id_Order).FirstOrDefault().Id_Order + 1;
                }
                newOrder.Price = price;
                Models.context.GetContext().Orders.Add(newOrder);
                Models.context.GetContext().SaveChanges();
                Models.context.GetContext().ServiceOrders.AddRange(serv);
                Models.context.GetContext().SaveChanges();
                saveToPdf();
                serv.Clear();
            //}
            //catch(Exception ex)
            //{ MessageBox.Show(ex.Message); }
        }

        private void cbSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbSearch.Text = string.Empty;
            if(cbSearch.SelectedIndex != -1)
            {
                tbSearch.IsEnabled = true;
            }
        }

        private void clAddnewUser(object sender, RoutedEventArgs e)
        {
            AddpacientWindow add = new AddpacientWindow(null);
            add.Show();
        }

        private void clSearch(object sender, RoutedEventArgs e)
        {
            List<Models.Patient> pat = new List<Models.Patient>();
            List<Models.Service> ser = new List<Models.Service>();
            if (cbSearch.Items[0].ToString() == "Номер паспорта")
            {
                dgServices.ItemsSource = null;
                switch (cbSearch.SelectedItem.ToString())
                {
                    case "Номер паспорта":
                        dgPatients.ItemsSource = Models.context.GetContext().Patients.Where(p => p.Passport_n.Contains(tbSearch.Text)).ToList();
                        break;
                    case "Серия паспорта":
                        dgPatients.ItemsSource = Models.context.GetContext().Patients.Where(p => p.Passport_s.Contains(tbSearch.Text)).ToList();
                        break;
                    case "Имя":
                        foreach(Models.Patient p in Models.context.GetContext().Patients.ToList())
                        {
                            if (LevenshteinDistance(p.Full_Name, tbSearch.Text) <= 3)
                                pat.Add(p);
                        }
                        dgPatients.ItemsSource = pat;
                        break;
                    case "Телефон":
                        dgPatients.ItemsSource = Models.context.GetContext().Patients.Where(p => p.Telephone.Contains(tbSearch.Text)).ToList();
                        break;
                    case "Номер страхового полиса":
                        dgPatients.ItemsSource = Models.context.GetContext().Patients.Where(p => p.Social_Sec_Number.Contains(tbSearch.Text)).ToList();
                        break;
                }
            }
            else
            {
                dgPatients.ItemsSource = null;
                switch (cbSearch.SelectedItem.ToString())
                {
                    case "Название":
                        foreach (Models.Service p in Models.context.GetContext().Services.ToList())
                        {
                            if (LevenshteinDistance(p.Name, tbSearch.Text) <= 3)
                                ser.Add(p);
                        }
                        dgServices.ItemsSource = ser;
                        break;
                    case "Цена":
                        dgServices.ItemsSource = Models.context.GetContext().Patients.Where(p => p.Passport_s.Contains(tbSearch.Text)).ToList();
                        break;
                }

            }
        }

        public static int LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];
            if (n == 0)
            {
                return m;
            }
            if (m == 0)
            {
                return n;
            }
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            return d[n, m];
        }

        private void clEdit(object sender, RoutedEventArgs e)
        {
            AddpacientWindow window;
            Models.Patient patien = Models.context.GetContext().Patients.Where(p => p.Full_Name == Name.Text).FirstOrDefault();
            if (patien != null)
            {
                window = new AddpacientWindow(patien);
                window.Show();
            }
            else
                MessageBox.Show("Нет такого пользователя, проверьте правильность введенных данных");
        }

        private void cbSearc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSearc.SelectedIndex != -1)
            {
                if ((sender as ComboBox).SelectedItem.ToString() == "Пациент")
                {
                    cbSearch.ItemsSource = new List<string>() { "Номер паспорта", "Серия паспорта", "Имя", "Телефон", "Номер страхового полиса" };
                }
                else
                {
                    cbSearch.ItemsSource = new List<string>() { "Название", "Цена" };
                }
                cbSearch.IsEnabled = true;
            }
        }

        private void clAcceptBio(object sender, RoutedEventArgs e)
        {
            gOrder.Visibility = Visibility.Visible;
        }

        private void saveToPdf()
        {
            string ttf = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
            Document document = new Document();
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream($"Orders/result{newOrder.Id_Order}.pdf", FileMode.Create));
            document.Open();
            document.Add(new iTextSharp.text.Paragraph("Дата заказа: " + newOrder.DateOrder, font));
            document.Add(new iTextSharp.text.Paragraph("Номер заказа: " + newOrder.Id_Order, font));
            document.Add(new iTextSharp.text.Paragraph("Номер пробирки: " + newOrder.Id_Blood, font));
            document.Add(new iTextSharp.text.Paragraph("Номер страховаого полиса :" + newOrder.Patient.Social_Sec_Number, font));
            document.Add(new iTextSharp.text.Paragraph("ФИО :" + newOrder.Patient.Full_Name, font));
            document.Add(new iTextSharp.text.Paragraph("Дата рождения :" + newOrder.Patient.RealBirth, font));
            iTextSharp.text.Paragraph pg = new iTextSharp.text.Paragraph("", font);
            pg.Add("Список услуг: ");
            foreach (Models.ServiceOrders service in newOrder.ServicesOrders)
                pg.Add(service.Service.Name + " ");
            document.Add(pg);
            document.Add(new iTextSharp.text.Paragraph("Цена: " + newOrder.Price, font));
            DateTime date = Convert.ToDateTime(Convert.ToDateTime(newOrder.DateOrder), new CultureInfo("us-US"));
            var plainTextByte1 = System.Text.Encoding.UTF8.GetBytes(date.ToString());
            var res1 = System.Convert.ToBase64String(plainTextByte1);
            var plainTextByte2 = System.Text.Encoding.UTF8.GetBytes(newOrder.Id_Order.ToString());
            var res2 = System.Convert.ToBase64String(plainTextByte2);
            document.Add(new iTextSharp.text.Paragraph($"https://wsrussia.ru/?data=base64(date_Order={res1}&id_Order={res2})", font));
            document.Close();
        }
    }
}
