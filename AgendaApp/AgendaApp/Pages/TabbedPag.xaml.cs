using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Net.Http;
using System.Net;
using System.IO;

namespace AgendaApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class TabbedPag : Xamarin.Forms.TabbedPage
    {
        [Obsolete]
        public TabbedPag()
        {         
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom)
             .SetBarItemColor(Color.White)
             .SetBarSelectedItemColor(Color.FromRgb(69,83,153));
            ContentPage servicePage = new ServicePage();
            ContentPage newsPage = new NewsPage();
            newsPage.IconImageSource = "newspaper.png";
            newsPage.Title = "News";
            servicePage.IconImageSource = "customer.png";
            servicePage.Title = "Service";
            Children.Add(newsPage);
            Children.Add(servicePage);
        }
    }

}