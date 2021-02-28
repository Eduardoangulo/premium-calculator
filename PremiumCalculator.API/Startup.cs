using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PremiumCalculator.Application.Exceptions;
using PremiumCalculator.Application.Infraestructure.Repositories;
using PremiumCalculator.Application.Interface.Repositories;
using PremiumCalculator.Application.Interface.UseCases;
using PremiumCalculator.Application.UseCases;

namespace PremiumCalculator.API
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
            services.AddScoped<IPremiumCalculator, PremiumCalculatorUseCase>();
            services.AddScoped<IPremiumCalculatorRepository, PremiumCalculatorRepository>();
            services.AddControllers();
            services.AddControllers(options =>
                options.Filters.Add(new HttpResponseExceptionFilter()));
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
