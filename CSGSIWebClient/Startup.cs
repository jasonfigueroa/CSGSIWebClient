using CSGSIWebClient.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CSGSIWebClient
{
    public class Startup
    {
        private IHostingEnvironment _env;

        public Startup(IHostingEnvironment env)
        {
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/";
                    options.CookieSecure = CookieSecurePolicy.Always;
                    // options.CookieHttpOnly = false;
                });

            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<ISteamApiService, SteamApiService>();

            services.AddMvc();

            if (_env.IsDevelopment())
            {
                services.AddWebOptimizer(minifyJavaScript: false, minifyCss: false);
            }
            else
            {
                services.AddWebOptimizer(pipeline =>
                {
                    pipeline.AddJavaScriptBundle(
                        "js/dist/bundle.js", // destination
                        //"js/*.js" // This would be used to bundle all js files in js directory
                        "js/urls.js",
                        "js/secrets.js",
                        "js/globals.js",
                        "js/util.js",
                        "js/charts.js",
                        "js/APIInterface.js"
                    );
                    pipeline.MinifyJsFiles(
                        "js/views/*.js"
                    );
                    pipeline.MinifyCssFiles(
                        "css/*.css"
                    );
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // in this method order of the added middleware components is important
            _env = env;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebOptimizer();

            app.UseAuthentication();

            app.UseStatusCodePages();

            app.UseStaticFiles(); // for wwwroot

            // may be able to remove the following block since download file is being moved to wwwroot
            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //    Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
            //    RequestPath = "/StaticFiles"
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
