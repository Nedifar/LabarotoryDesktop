using iTextSharp.text;
using iTextSharp.text.pdf;
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
using System.Windows.Shapes;
using System.Windows.Forms.DataVisualization.Charting;

namespace Labarotory.Forms
{
    /// <summary>
    /// Логика взаимодействия для DateReport.xaml
    /// </summary>
    public partial class DateReport : Window
    {
        bool a;
        public DateReport(bool b)
        {
            InitializeComponent();
            a = b;
            if (a == true)
            {
                cbViewReport.SelectedIndex = 1;
                cbViewReport.Visibility = Visibility.Collapsed;

            }
            cbViewReport.ItemsSource = new List<string> { "Текстовая информация", "Текстовая информация и график", "Текстовая информация и таблица", "Все" };
        }

        private void clAccept(object sender, RoutedEventArgs e)
        {
            if (a)
            {
                double pricePat = 0;
                double Pricecomp = 0;
                double price = 0;
                DateTime f = beginDate.SelectedDate.Value;
                DateTime p = endDate.SelectedDate.Value;
                string f1 = String.Format("{0:s}", f);
                string p1 = String.Format("{0:s}", p);
                List<Models.ServiceOrders> h = Models.context.GetContext().ServiceOrders.Include("Order").Include("Order.Patient").Include("Service").ToList();
                List<Models.ServiceOrders> list = h.Where(l => String.Compare(l.FinishedDate, f1) != -1 && String.Compare(l.FinishedDate, p1) != 1).ToList();
                List<Models.SocialSec> socials = Models.context.GetContext().SocialSecs.ToList();
                string ttf = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
                var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                var font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream($"Reports/result{beginDate.SelectedDate.Value.ToShortDateString() + "-" + endDate.SelectedDate.Value.ToShortDateString()}.pdf", FileMode.Create));
                document.Open();
                foreach (Models.SocialSec social in socials)
                {
                    List<int> pat = new List<int>();
                    List<Models.ServiceOrders> addinglist = new List<Models.ServiceOrders>();
                    foreach (Models.ServiceOrders orders in list)
                    {
                        if (social.Id_Social_name == orders.Order.Patient.SocialSec.Id_Social_name)
                        {
                            addinglist.Add(orders);
                        }
                    }
                    if (addinglist.Count != 0)
                    {
                        document.NewPage();
                        document.Add(new iTextSharp.text.Paragraph("Компания: " + social.Social_Name, font));
                        document.Add(new iTextSharp.text.Paragraph("Перид оплаты: " + beginDate.SelectedDate + "-" + endDate.SelectedDate, font));
                        addinglist.OrderBy(g => g.Order.Id_Patient);
                        foreach (Models.ServiceOrders service in addinglist)
                        {
                            if (pat.Contains(service.Order.Id_Patient))
                            {
                                document.Add(new iTextSharp.text.Paragraph("Услуга: " + service.Service.Name + " " + service.Service.Price, font));
                                pricePat += service.Service.Price;
                            }
                            else
                            {
                                document.Add(new iTextSharp.text.Paragraph("Пациент: " + service.Order.Patient.Full_Name, font));
                                document.Add(new iTextSharp.text.Paragraph("Услуга: " + service.Service.Name + " " + service.Service.Price, font));
                                pricePat += service.Service.Price;
                                pat.Add(service.Order.Id_Patient);
                            }
                        }
                        document.Add(new iTextSharp.text.Paragraph("Итого по пациенту: " + pricePat, font));
                        Pricecomp += pricePat;
                        document.Add(new iTextSharp.text.Paragraph("Итого по комании: " + Pricecomp, font));
                        price += Pricecomp;
                        pricePat = 0;
                        Pricecomp = 0;
                    }

                }
                document.Add(new iTextSharp.text.Paragraph("Итого: " + price, font));
                document.Close();
                Close();
            }
            else
            {
                DateTime f = beginDate.SelectedDate.Value;
                DateTime p = endDate.SelectedDate.Value;
                string f1 = String.Format("{0:s}", f);
                string p1 = String.Format("{0:s}", p);
                List<Models.ServiceOrders> h = Models.context.GetContext().ServiceOrders.Include("Order").Include("Order.Patient").Include("Service").ToList();
                List<Models.ServiceOrders> list = h.Where(l => String.Compare(l.FinishedDate, f1) != -1 && String.Compare(l.FinishedDate, p1) != 1).ToList();
                List<Models.SocialSec> socials = Models.context.GetContext().SocialSecs.ToList();
                string ttf = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIAL.TTF");
                var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                var font = new iTextSharp.text.Font(baseFont, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL);
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream($"AdminReports/Report{beginDate.SelectedDate.Value.ToShortDateString() + "-" + endDate.SelectedDate.Value.ToShortDateString()}.pdf", FileMode.Create));
                document.Open();
                List<int> ar = new List<int>();
                chartP.ChartAreas.Add(new ChartArea("Main"));
                var currentSeries = new Series("Patients")
                {
                    IsValueShownAsLabel = true
                };
                chartP.Series.Add(currentSeries);
                document.Add(new iTextSharp.text.Paragraph($"За период временени {beginDate.SelectedDate.Value.ToShortDateString() + " - " + endDate.SelectedDate.Value.ToShortDateString()} было оказано {list.Count} услуг и {list.Select(x=>x.Order.Id_Patient).Distinct().Count()} пациента(ов)", font));
                foreach (Models.ServiceOrders sv in list)
                {
                    PdfPTable table = new PdfPTable(2);
                    if (ar.Contains(sv.Id_Service) == false)
                    {
                        ar.Add(sv.Id_Service);
                        document.Add(new iTextSharp.text.Paragraph($"Услуга {sv.Service.Name}", font));                         
                            PdfPCell cell = new PdfPCell(new Phrase(beginDate.SelectedDate.Value.ToShortDateString() + " - " + endDate.SelectedDate.Value.ToShortDateString(), font));
                            PdfPCell cel1 = new PdfPCell(new Phrase(sv.Service.Name, font));
                            cell.Colspan=2;
                            cel1.Colspan=2;
                            table.AddCell(cell);
                            table.AddCell(cel1);
                            table.AddCell("Дата");
                            table.AddCell("Количество людей");
                        currentSeries.Points.Clear();
                        for (DateTime i = f; i <= p;)
                        {
                            int count = 0;
                            List<int> pa = new List<int>();
                            foreach (Models.ServiceOrders service in list.Where(v => v.Finished.Contains(i.ToShortDateString()) && v.Id_Service == sv.Id_Service))
                            {
                                if (pa.Contains(service.Order.Id_Patient) == false)
                                {
                                    pa.Add(service.Order.Id_Patient);
                                    count++;
                                }
                            }
                            
                            document.Add(new iTextSharp.text.Paragraph($"Количество людей за {i.ToShortDateString()} по данной услуге составляет {count}", font));
                            currentSeries.Points.AddXY(i.Date, count);
                            currentSeries.ChartType = SeriesChartType.Line;
                            chartP.SaveImage($"{AppDomain.CurrentDomain.BaseDirectory}Graphics/first.jpeg", ChartImageFormat.Jpeg);
                            i = i.AddDays(1);
                                table.AddCell(i.ToShortDateString());
                                table.AddCell(count.ToString());
                        }
                        iTextSharp.text.Image pic = iTextSharp.text.Image.GetInstance($"{AppDomain.CurrentDomain.BaseDirectory}Graphics/first.jpeg");
                        pic.ScaleAbsolute(300, 300);
                        if(cbViewReport.SelectedIndex == 2 || cbViewReport.SelectedIndex == 3)
                        document.Add(table);
                        if (cbViewReport.SelectedIndex == 1 || cbViewReport.SelectedIndex == 3)
                            document.Add(pic);
                        document.NewPage();
                    }
                }
                document.Close();
                Close();
            }
        }
    }
}
