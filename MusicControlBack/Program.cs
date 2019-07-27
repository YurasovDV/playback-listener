using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace MusicControlBack
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseStartup<Startup>()
                .UseKestrel(opts => { opts.ListenAnyIP(Constants.Port); })
                .Build();

            host.Run();
        }
    }
}
