using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace apiLab.Models
{
    public class Service
    {
        [Key]
        public int Id_Service { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
