using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Host;
using Microsoft.Owin.Hosting;

namespace IntraGet.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:5000";
            using (var webApp = WebApp.Start(url, (app) => app.UseNuGetServer()))
            {
                Console.WriteLine("Server listening at {0}.", url);
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
            }
        }
    }
}
