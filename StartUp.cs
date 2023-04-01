using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tema_1.DataBase;
using static Tema_1.DataBase.Reports;

namespace Tema_1
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });
            /*Dependency Injection used to add external database => AddDbContext
            SSMS usage => UseSqlServer()*/
            services.AddDbContext<Reports>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("SQLEXPRESS")));
            services.AddSingleton<IConfiguration>(_configuration);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

