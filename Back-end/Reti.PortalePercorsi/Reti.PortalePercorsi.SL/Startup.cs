using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Reti.PortalePercorsi.DAL.Entity;
using Reti.PortalePercorsi.DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reti.PortalePercorsi.SL
{
    public class Startup
    {
        private string AllowOrigins = "MyPolicyForCors";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
                options.AddPolicy(name: AllowOrigins,
                builder =>
                {
                    builder.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                })
            );

            services.AddDbContext<PortalePercorsiContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Portale Percorsi");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(AllowOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
