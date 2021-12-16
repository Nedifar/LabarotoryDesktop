using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labarotory.Models
{
    public class History
    {
        [Key]
        public int Id_History { get; set; }
        public string Login { get; set; }
        public string dateSign { get; set; }
        public string Attempt { get; set; }
    }
}
