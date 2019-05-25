using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Formatters;
using Halcyon.Web.HAL.Json;
using B2Handpicked.DomainServices;
using B2Handpicked.DomainModel;
using B2Handpicked.Infrastructure;
using Microsoft.EntityFrameworkCore;
using B2Handpicked.LabelAPI.Authentication;

namespace B2Handpicked.LabelAPI {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddMvcOptions(c => {
                    c.OutputFormatters.RemoveType<JsonOutputFormatter>();
                    c.OutputFormatters.Add(new JsonHalOutputFormatter(
                        new string[] { "application/hal+json", "application/vnd.example.hal+json", "application/vnd.example.hal.v1+json" }
                    ));
                });

            services.AddTransient<IAuthentication, LabelAuth>();

            services.AddTransient<IAppDbContext, ApplicationDbContext>();

            services.AddTransient<IRepository<ContactPerson>, EFContactPersonRepository>();
            services.AddTransient<IRepository<Customer>, EFCustomerRepository>();
            services.AddTransient<IRepository<Deal>, EFDealRepository>();
            services.AddTransient<IRepository<Employee>, EFEmployeeRepository>();
            services.AddTransient<IRepository<Invoice>, EFInvoiceRepository>();
            services.AddTransient<IRepository<Label>, EFLabelRepository>();

            services.AddDbContext<ApplicationDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
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
            app.UseMiddleware<AuthenticationMiddleware>();
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
