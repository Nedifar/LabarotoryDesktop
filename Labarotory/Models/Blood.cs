using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labarotory.Models
{
    public class Blood
    {
        [Key]
        public int Id_Blood { get; set; }
        public string Barcode { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
