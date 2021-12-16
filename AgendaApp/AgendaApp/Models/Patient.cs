using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AgendaApp.Models
{
    public class Patient
    {
        public int Id_Patient { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Passport_s { get; set; }
        public string Passport_n { get; set; }
        public string SocialSecN { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string FullYear
        {
            get
            {
                DateTime dt = Convert.ToDateTime(Date);
                int res = DateTime.Now.Year - dt.Year;
                if (DateTime.Now.DayOfYear > dt.DayOfYear)
                    return (res--).ToString();
                else
                    return res.ToString();
            }
        }
        public DateTime BirhDate()
        {
            return DateTime.Parse(Date);
        }
    }
}
