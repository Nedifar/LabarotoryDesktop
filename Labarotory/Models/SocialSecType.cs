using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labarotory.Models
{
    public class SocialSecType
    {
        [Key]
        public int Id_Social_Sec_Type { get; set; }
        public string Name { get; set; }
        public List<Patient> Patients { get; set; } = new List<Patient>();
    }
}
