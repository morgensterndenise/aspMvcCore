using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using webCats.Data;
using webCats.Models;
using webCats.Services;

namespace webCats
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CatsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //otgovaria za rolite i autentikaciata
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {

                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.Password.RequireLowercase = false;


            })
                .AddEntityFrameworkStores<CatsDbContext>()
                .AddDefaultTokenProviders();

            // servisa za majlite, ako ne polzvame mail mojem da si go iztriem (vnimanie ako se polzva regvane na usera da ne se trie)
            services.AddTransient<IEmailSender, EmailSender>();
            //regvam si cat servica; s transient vseki put shte i suzdava instancia pri startirane na app
            services.AddTransient<ICatService, CatService>();
          
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //tuk regvame midwarite
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //tva znachi pr ako updatnem niakoy css da go pokazva v browsera avtomatichno, a da ne triabva da relodvame
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //tva pravi vsichko v papka root dostupno chrez browsera
            app.UseStaticFiles();


            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                //title i date sa parametri
                routes.MapRoute(
                    name: "blog",
                    template: "Blog/{controller}/{action}/{title}/{date}");

                //pravene na nash si route; tuk niamame defaultni stoynosti
                routes.MapRoute(
                    name:"admin",
                    template: "Admin/{controller}/{action}/");
                  
                //tuk ima defaultni stoynosti Home i Index, tva znachi che ako ne se napishe neshto polzva tiah
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"); //id? oznachava che e optional
            });
        }
    }
}
