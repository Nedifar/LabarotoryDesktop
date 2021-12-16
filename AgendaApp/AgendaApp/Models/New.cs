using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class New
    {
        public int Id_New { get; set; }
        public string Heading { get; set; }
        public string datePublic { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }
        public int Numerable { get; set; }
        public IEnumerable<New> MyNews(IEnumerable<New> nss) { return GetNews(nss); }
        private IEnumerable<New> GetNews(IEnumerable<New> obsnews)
        {
            Random rnd = new Random();
            string[] colors = new string[] { "#B96CBD", "#49A24D", "#FDA838", "#F75355", "#00C6AE", "#455399" };
            int count = 1;
            obsnews = obsnews.Reverse();
            foreach(New @new in obsnews)
            {
                @new.Color = colors[rnd.Next(0, 6)];
                @new.Heading += "\n\n";
                @new.Numerable = count;
                count ++;
            }
            return obsnews;
        }
    }
}
