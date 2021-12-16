using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labarotory.Models
{
    public class Administrator
    {
        [Key]
        public int Id_Administrator { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
