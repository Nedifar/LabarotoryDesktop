using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labarotory.Models
{
    public class Order
    {
        [Key]
        public int Id_Order { get; set; }
        public string DateOrder { get; set; }
        public int Id_Blood { get; set; }
        public int Id_Patient { get; set; }
        public double? Price { get; set; }
        public string Status { get; set; }
        public Blood Blood { get; set; }
        public Patient Patient { get; set; }
        public List<ServiceOrders> ServicesOrders { get; set; } = new List<ServiceOrders>();
    }
}
