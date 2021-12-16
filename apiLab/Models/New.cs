using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace apiLab.Models
{
    public class New
    {
        [Key]
        public int Id_New { get; set; }
        public string Heading { get; set; }
        public string datePublic { get; set; }
        public string Text { get; set; }
    }
}
