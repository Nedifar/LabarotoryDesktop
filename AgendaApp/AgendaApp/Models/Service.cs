using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class Service
    {
        public int Id_Service { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public ObservableCollection<Service> MyServices(ObservableCollection<Service> nss) { return GetServices(nss); }
        private ObservableCollection<Service> GetServices(ObservableCollection<Service> obsnews)
        {
            Random rnd = new Random();
            string[] colors = new string[] { "#B96CBD", "#49A24D", "#FDA838", "#F75355", "#00C6AE", "#455399" };
            foreach (Service @service in obsnews)
            {
                @service.Color = colors[rnd.Next(0, 6)];
                @service.Name += "\n\n";
            }
            return obsnews;
        }
    }
}
