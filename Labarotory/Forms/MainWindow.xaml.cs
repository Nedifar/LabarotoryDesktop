using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Labarotory.Forms;
using Labarotory.Models;

namespace Labarotory
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int count;
        private string text;
        public MainWindow()
        {
            InitializeComponent();         
        }

        private void clickRegistration(object sender, RoutedEventArgs e)
        {

        }

        private void clickSign(object sender, RoutedEventArgs e)
        {

        }

        private void clicktoSign(object sender, RoutedEventArgs e)
        {
            if (tbPasswordVisible.Visibility == Visibility.Visible)
                tbPassword.Password = tbPasswordVisible.Text;
            if (rbAdmin.IsChecked == true)
                {
                    Administrator user = context.GetContext().Administrators.Where(p => p.Login == tbEmail.Text && p.Password == tbPassword.Password).FirstOrDefault();
                    if (user != null)
                    {
                        context.GetContext().Histories.Add(new History { Login = user.Login, dateSign = DateTime.Now.ToString(), Attempt = "Вход выполнен" });
                        context.GetContext().SaveChanges();
                        Forms.Main main = new Forms.Main(new AdministratorPage(), new BitmapImage(new Uri($"pack://application:,,,/Resources/Admin.png")), null);
                        main.Show();
                        Close();
                    }
                    else
                    {
                        toSign();
                    }
                }
                if(rbLaborant.IsChecked == true)
                {
                    User user = context.GetContext().Users.Where(p => p.Login == tbEmail.Text && p.Password == tbPassword.Password && p.Id_Role == 2).Include("Role").FirstOrDefault();
                    if (user != null)
                    {
                        context.GetContext().Histories.Add(new History { Login = user.Login, dateSign = DateTime.Now.ToString(), Attempt = "Вход выполнен" });
                        context.GetContext().SaveChanges();
                        Forms.Main main = new Forms.Main(new LaborantPage(), new BitmapImage(new Uri($"pack://application:,,,/Resources/laborant_1.jpeg")), user);
                        main.Show();
                        Close();
                    }
                    else
                    {
                        toSign();
                    }
                }
                if (rbExplorer.IsChecked == true)
                {
                    User user = context.GetContext().Users.Where(p => p.Login == tbEmail.Text && p.Password == tbPassword.Password && p.Id_Role == 2).Include("Role").FirstOrDefault();
                    if (user != null)
                    {
                        context.GetContext().Histories.Add(new History { Login = user.Login, dateSign = DateTime.Now.ToString(), Attempt = "Вход выполнен" });
                        context.GetContext().SaveChanges();
                        Forms.Main main = new Forms.Main(new ExplorerPage(user.Id_User), new BitmapImage(new Uri($"pack://application:,,,/Resources/laborant_2.png")), user);
                        main.Show();
                        Close();
                    }
                    else
                    {
                        toSign();
                    }
                }
                if (rbAccountant.IsChecked == true)
                {
                User user = context.GetContext().Users.Where(p => p.Login == tbEmail.Text && p.Password == tbPassword.Password && p.Id_Role == 2).Include("Role").FirstOrDefault();
                if (user != null)
                {
                    context.GetContext().Histories.Add(new History { Login = user.Login, dateSign = DateTime.Now.ToString(), Attempt = "Вход выполнен" });
                    context.GetContext().SaveChanges();
                    Forms.Main main = new Forms.Main(new Accountant(), new BitmapImage(new Uri($"pack://application:,,,/Resources/Accountant.jpeg")), user);
                    main.Show();
                    Close();
                }
                else
                {
                    toSign();
                }
            }
        }

        private void toSign()
        {
                MessageBox.Show("Проверьте правильность введенных данных");
                context.GetContext().Histories.Add(new History { Login = tbEmail.Text, dateSign = DateTime.Now.ToString(), Attempt = "Ошибка входа" });
                context.GetContext().SaveChanges();
                stackCaptcha.Visibility = Visibility.Visible;
                count++;
                bSign.IsEnabled = false;
                if (count == 2)
                {
                    MessageBox.Show("Не верно, жди 10 секунд");
                    bSign.IsEnabled = false;
                    textCaptcha.Text = "";
                    CheckIcon.Visibility = Visibility.Collapsed;
                    DispatcherTimer timer = new DispatcherTimer();
                    timer.Interval = TimeSpan.FromSeconds(10);
                    timer.Tick += Timer_Tick;
                    timer.Start();
                    count = 0;
                }
                captchaBox.Source = CreateImage(200, 60);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            (sender as DispatcherTimer).Stop();
            bSign.IsEnabled = true;
        }

        private void Click_Visible(object sender, RoutedEventArgs e)
        {
            switch (tbPasswordVisible.Visibility)
            {
                case Visibility.Visible:
                    tbPassword.Password = tbPasswordVisible.Text;
                    tbPasswordVisible.Visibility = Visibility.Collapsed;
                    tbPassword.Visibility = Visibility.Visible;
                    eye.Kind = MaterialDesignThemes.Wpf.PackIconKind.Eye;
                    tbPassword.Focus();
                    break;
                case Visibility.Collapsed:
                    tbPasswordVisible.Text = tbPassword.Password;
                    tbPassword.Visibility = Visibility.Collapsed;
                    tbPasswordVisible.Visibility = Visibility.Visible;
                    eye.Kind = MaterialDesignThemes.Wpf.PackIconKind.EyeCheckOutline;
                    tbPasswordVisible.Focus();
                    break;
            }
        }

        private BitmapSource CreateImage(int Width, int Height)
        {
            Random rnd = new Random();
            Bitmap result = new Bitmap(Width, Height);
            int Xpos = 10;
            int Ypos = 10;
            Brush[] colors = {
             Brushes.Black,
             Brushes.Red,
             Brushes.RoyalBlue,
             Brushes.Green,
             Brushes.Yellow,
             Brushes.White,
             Brushes.Tomato,
             Brushes.Sienna,
             Brushes.Pink };
            Pen[] colorpens = {
             Pens.Black,
             Pens.Red,
             Pens.RoyalBlue,
             Pens.Green,
             Pens.Yellow,
             Pens.White,
             Pens.Tomato,
             Pens.Sienna,
             Pens.Pink };
            System.Drawing.FontStyle[] fontstyle = {
             System.Drawing.FontStyle.Bold,
             System.Drawing.FontStyle.Italic,
             System.Drawing.FontStyle.Regular,
             System.Drawing.FontStyle.Strikeout,
             System.Drawing.FontStyle.Underline};
            Int16[] rotate = { 1, -1, 2, -2, 3, -3, 4, -4, 5, -5, 6, -6 };
            Graphics g = Graphics.FromImage((System.Drawing.Image)result);
            g.Clear(Color.Gray);
            g.RotateTransform(rnd.Next(rotate.Length));
            text = String.Empty;
            string ALF = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];
            g.DrawString(text,
                         new Font("Arial", 25, fontstyle[rnd.Next(fontstyle.Length)]),
                         colors[rnd.Next(colors.Length)],
                         new PointF(Xpos, Ypos));
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)],
                       new System.Drawing.Point(0, 0),
                       new System.Drawing.Point(Width - 1, Height - 1));
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)],
                       new System.Drawing.Point(0, Height - 1),
                       new System.Drawing.Point(Width - 1, 0));
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, Color.White);
            System.Drawing.Bitmap br = result;
            System.Windows.Media.Imaging.BitmapSource b =
                System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                       br.GetHbitmap(),
                       IntPtr.Zero,
                       Int32Rect.Empty,
                       BitmapSizeOptions.FromEmptyOptions());
            return b;
        }

        private void clUpdate(object sender, RoutedEventArgs e)
        {
            captchaBox.Source = CreateImage(200, 60);
            textCaptcha.Text = "";
        }

        private void clAccept(object sender, RoutedEventArgs e)
        {
            if (text == textCaptcha.Text)
            {
                CheckIcon.Visibility = Visibility.Visible;
                bSign.IsEnabled = true;
            }
            else
            {
                clUpdate(null, null);
                textCaptcha.Text = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //var filedata = File.ReadAllLines(@"C:\Users\gazimov.ii0794\Desktop\list.txt");
            //foreach (var line in filedata)
            //{
            //    var data = line.Split('\t');
            //    var temp = new Patient
            //    {
            //        Full_Name = data[0],
            //        Email = data[1],
            //        Social_Sec_Number = data[2],
            //        Id_Social_Sec_Type = int.Parse(data[3]),
            //        Telephone = data[4],
            //        Passport_s = data[5],
            //        Passport_n =data[6],
            //        Birthday = data[7],

            //    };
            //    string tmp = data[8];
            //    var currentType = context.GetContext().SocialSecs.FirstOrDefault(p => p.Social_Name.Contains(tmp));
            //    temp.Id_Social_name = currentType.Id_Social_name;
            //    context.GetContext().Patients.Add(temp);

            //}
            //context.GetContext().SaveChanges();


            //var filedata = File.ReadAllLines(@"C:\Users\gazimov.ii0794\Desktop\userss.txt");
            //foreach (var line in filedata)
            //{
            //    var data = line.Split('\t');
            //    CultureInfo info = new CultureInfo("us-US");
            //    var temp = new User
            //    {
            //        Name = data[1].Trim(),
            //        Login = data[2].Trim(),
            //        Password = data[3].Trim(),
            //        dateSign = DateTime.Parse(data[4].Trim(), info).ToShortDateString(),
            //        Id_Role = int.Parse(data[6])
            //    };
            //    foreach(var tp in data[5].Split(new string[] { ","}, StringSplitOptions.RemoveEmptyEntries))
            //    {
            //        int a = int.Parse(tp);
            //        var currentType = context.GetContext().Services.FirstOrDefault(p => p.Id_Service == a);
            //        if (currentType != null)
            //            temp.Services.Add(currentType);
            //    }
            //    context.GetContext().Users.Add(temp);
            //    context.GetContext().SaveChanges();
            //}
            List<Patient> p = new List<Patient>();
            foreach (ServiceOrders f in context.GetContext().ServiceOrders)
            {
                if (f.Result != null)
                {
                    f.Result = f.Result.Trim();
                    f.Finished = f.Finished.Trim();
                    f.Status = f.Status.Trim();
                    f.Analyzer = f.Analyzer.Trim();
                    f.Result = f.Result.Replace(".", ",");
                }
            }
            context.GetContext().SaveChanges();
            //double pr =0;
            //foreach (ServiceOrders p in Models.context.GetContext().ServiceOrders.Include("Service").Where(g => g.Result != null))
            //    pr += p.Service.Price;
        }

        private void check(object sender, RoutedEventArgs e)
        {
            if ((sender as RadioButton).IsChecked == true)
                bSign.IsEnabled = true;
        }
    }
}
