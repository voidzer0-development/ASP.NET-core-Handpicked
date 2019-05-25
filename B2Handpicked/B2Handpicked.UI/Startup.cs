using B2Handpicked.DomainModel;
using B2Handpicked.DomainServices;
using B2Handpicked.Infrastructure;
using B2Handpicked.UI.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace B2Handpicked.UI {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IAppDbContext, ApplicationDbContext>();

            services.AddTransient<IRepository<ContactPerson>, EFContactPersonRepository>();
            services.AddTransient<IRepository<Customer>, EFCustomerRepository>();
            services.AddTransient<IRepository<Deal>, EFDealRepository>();
            services.AddTransient<IRepository<Employee>, EFEmployeeRepository>();
            services.AddTransient<IRepository<Invoice>, EFInvoiceRepository>();
            services.AddTransient<IRepository<Label>, EFLabelRepository>();

            services.AddDbContext<ApplicationDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("identityConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthenticationDbContext>().AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            } else {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseAuthentication();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "defaultWithId",
                    template: "{controller=Home}/{action=Index}/{id:int}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
