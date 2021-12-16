using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace apiLab.Models
{
    public class context : DbContext
    {
        //public context(DbContextOptions options) : base(options) { }
        //public static context AgetContext()
        //{
        //    //if (_context == null)
        //    //    _context = new context(new DbContextOptions);
        //    //return _context;
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={System.AppDomain.CurrentDomain.BaseDirectory}Resources/db.db");
        }
        public context(DbContextOptions<context> options)
            : base(options) { }
        public DbSet<New> News { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}
