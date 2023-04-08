using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace S3_Individual_Back_end
{
    public class Startup
    {
	    private readonly IConfiguration _configuration;

		public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

		// This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            string conStr = this._configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DbContext>(options => options.UseSqlServer(conStr));
            services.AddEndpointsApiExplorer();
            services.AddSession();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowVueJS",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:8080") // Update with the URL of your Vue.js app
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else

            app.UseCors("AllowVueJS");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();
            app.UseSession();



            // builder._configuration()
        }
    }
}
