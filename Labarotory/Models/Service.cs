using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labarotory.Models
{
    public class Service
    {
        [Key]
        public int Id_Service { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ResultType { get; set; }
        public string Analyzers { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public List<ServiceOrders> ServiceOrders { get; set; } = new List<ServiceOrders>();
        public List<Exp> Exps { get; set; } = new List<Exp>();

    }
}
