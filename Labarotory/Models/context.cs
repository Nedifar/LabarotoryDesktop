using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labarotory.Models
{
    public class context : DbContext
    {
        private static context _context;
        public context()
            : base("name=sql")
        {
        }
        public static context GetContext()
        {
            if (_context == null)
                _context = new context();
            return _context;
        }
        public DbSet<History> Histories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<SocialSec> SocialSecs { get; set; }
        public DbSet<SocialSecType> SocialSecTypes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Blood> Bloods { get; set; }
        public DbSet<ServiceOrders> ServiceOrders { get; set; }
        public DbSet<Exp> Exps { get; set; }
    }
}
