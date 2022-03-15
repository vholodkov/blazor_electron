using ElectronNET.API;
using PayzmartElectron.BlazorApp;


static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder
                .UseElectron(args)
                .UseStartup<Startup>();
        });

CreateHostBuilder(args).Build().Run();