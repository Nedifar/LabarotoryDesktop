using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labarotory.Models
{
    public class SocialSec
    {
        [Key]
        public int Id_Social_name { get; set; }
        public string Social_Name { get; set; }
        public string Adress { get; set; }
        public string INN { get; set; }
        public string BIK { get; set; }
        public string r_c { get; set; }
        public List<Patient> Patients { get; set; } = new List<Patient>();
    }
}
