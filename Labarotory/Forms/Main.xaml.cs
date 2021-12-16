using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace Labarotory.Forms
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        ushort hours = 2;
        ushort minutes = 10;
        public Main(UserControl control, BitmapImage source, Models.User user)
        {
            InitializeComponent();
            gridUserContext.Children.Add(control);
            imgRole.Source = source;
            if (user != null)
            {
                tbRole.Text = user.Role.Name;
                tbFullName.Text = user.Name;
            }
            else
                tbFullName.Text = "Admin";
            DispatcherTimer dispatcher = new DispatcherTimer();
            dispatcher.Interval = TimeSpan.FromSeconds(1);
            dispatcher.Tick += timer_Tick;
            dispatcher.Start();            
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (minutes == 5 && hours == 0)
            {
                Task.Run(() =>
                {
                    MessageBox.Show($"Осталось {hours} минут {minutes} секунд");
                });
            }
            if (minutes == 0)
            {
                if (hours == 0)
                {
                    MainWindow main = new MainWindow();
                    main.Show();
                    Close();
                }
                else
                {
                    hours -= 1;
                    minutes = 60;
                }
            }
                minutes -= 1;
                timer.Text = hours.ToString()+" минут : "+minutes.ToString() + " секунд";
        }

        private void clExit(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }
    }
}
