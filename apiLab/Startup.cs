using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.Configuration;

namespace apiLab
{
    public class Startup
    {
        public static IConfiguration conf;
        
        public void ConfigureServices(IServiceCollection service)
        {
            string connectionString = "Data Source=db.db";
            //service.AddTransient<Models.context>();
            ////service.AddDbContext<Models.context>(opt => { opt.UseSqlite(conf())});
            //service.AddDbContext<Models.context>(options => options.UseSqlite(conf["ConnectionStrings:apiConnection"]));
            service.AddDbContext<Models.context>(options => options.UseSqlite(connectionString));
            service.AddControllers();
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
