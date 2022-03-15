using ElectronNET.API;
using ElectronNET.API.Entities;
using MatBlazor;
using PayzmartElectron.BlazorApp.Data;

namespace PayzmartElectron.BlazorApp
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services) 
        {
            services.AddMatBlazor();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
        }
        public virtual void Configure(IApplicationBuilder app)
        {
            if (!Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoinds =>
            {
                endpoinds.MapBlazorHub();
                endpoinds.MapFallbackToPage("/_Host");
            });
            if (HybridSupport.IsElectronActive)
            {
                ElectronBootstrap();
            }
        }


        public async void ElectronBootstrap()
        {
            var browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
            {
                Width = 250,
                Height = 520,
                Show = false,
                Resizable = false,
            });
            await browserWindow.WebContents.Session.ClearCacheAsync();
            browserWindow.OnReadyToShow += () => browserWindow.Show();
            browserWindow.SetTitle("Hello from Blazor payzmart!");
        }
    }
}
