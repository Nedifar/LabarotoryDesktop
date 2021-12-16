using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labarotory.Models
{
    public class User
    {
        [Key]
        public int Id_User { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string dateSign { get; set; }
        public int Id_Role { get; set; }
        public Role Role { get; set; }
        public List<Service> Services { get; set; } = new List<Service>();
        public List<ServiceOrders> ServiceOrders { get; set; } = new List<ServiceOrders>();

    }
}
