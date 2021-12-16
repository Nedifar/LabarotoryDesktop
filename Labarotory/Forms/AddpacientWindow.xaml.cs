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

namespace Labarotory.Forms
{
    /// <summary>
    /// Логика взаимодействия для AddpacientWindow.xaml
    /// </summary>
    public partial class AddpacientWindow : Window
    {
        private Models.Patient Patient;
        public AddpacientWindow(Models.Patient _patient)
        {
            InitializeComponent();
            cbnamesoc.ItemsSource = Models.context.GetContext().SocialSecs.ToList();
            cbtypesoc.ItemsSource = Models.context.GetContext().SocialSecTypes.ToList();
            if (_patient != null)
            {
                Patient = _patient;
            }
            DataContext = Patient;
        }

        private void clAdd(object sender, RoutedEventArgs e)
        {
            if(Patient.Id_Patient ==0)
            Models.context.GetContext().Patients.Add(Patient);
            Models.context.GetContext().SaveChanges();
            Close();
        }
    }
}
