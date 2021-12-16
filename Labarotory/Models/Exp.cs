using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labarotory.Models
{
    public class Exp
    {
        [Key]
        public int Id_Exp { get; set; }
        public double s { get; set; }
        public double avg { get; set; }
        public string Date { get; set; }
        public int Id_Service { get; set; }
        public Service Service { get; set; }
    }
}
