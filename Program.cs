using KlevelBrowser.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KlevelBrowser.Controller;
using Model;
using KlevelBrowser.Model;

namespace KlevelBrowser
{
    static class Program
    {
       
        
        
        
        
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Model.Browser model = new Browser();
            MainPage view = new MainPage();
            ViewHTML view1 = new ViewHTML();


            WebBrowserController controller = new WebBrowserController(model,view,view1);

            controller.startupView();
            

        }
    }
}
