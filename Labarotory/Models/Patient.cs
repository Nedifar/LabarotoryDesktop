using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labarotory.Models
{
    public class Patient
    {
        [Key]
        public int Id_Patient { get; set; }
        public string Full_Name { get; set; }
        public string Email { get; set; }
        public string Birthday { get; set; }
        public string Passport_s { get; set; }
        public string Passport_n { get; set; }
        public string Telephone { get; set; }
        public string Social_Sec_Number { get; set; }
        public int Id_Social_Name { get; set; }
        public int Id_Social_Sec_Type { get; set; }
        public SocialSec SocialSec { get; set; }
        public SocialSecType SocialSecType {get; set;}
        public List<Order> Orders { get; set; } = new List<Order>();
        public string RealBirth
        {
            get
            {
                try
                {
                    DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    return dt.AddMilliseconds(Double.Parse(Birthday)).Date.ToShortDateString();
                }
                catch
                {
                    return Birthday;
                }
            }
        }  
    }
}
