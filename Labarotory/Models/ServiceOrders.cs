using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labarotory.Models
{
    public class ServiceOrders
    {
        [Key]
        public int PK_ServiceOrders { get; set; }
        public int Id_Order { get; set; }
        public Order Order { get; set; }
        public int Id_Service { get; set; }
        public Service Service { get; set; }
        public string Result { get; set; }
        public string Finished { get; set; }
        public string Accepted { get; set; }
        public string Status { get; set; }
        public string Analyzer { get; set; }
        public int? Id_User { get; set; }
        public User User { get; set; }
        public string FinishedDate { get 
            {   
                try
                {
                    DateTime f = Convert.ToDateTime(Finished).Date;
                    string f1 = String.Format("{0:s}", f);
                    return f1;
                }
                catch
                {
                    return Finished;
                }
            } }
    }
}
