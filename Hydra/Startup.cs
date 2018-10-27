﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hydra.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Hydra.Models;

namespace Hydra
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
			services.AddMvc();

            var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("HydraContext"));
            //{
            //    Password = Configuration["Secret:DbPassword"]
            //};
            services.AddDbContext<HydraContext>(options =>
                                                options.UseSqlServer(builder.ConnectionString));

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddSingleton<ISecretSettings>(
                new SecretSettings(Configuration["Secret:MapCredantials"], 
                                   Configuration["Secret:DbPassword"],
                                   Configuration["Secret:WeatherKey"]));

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{ 
			if (env.IsDevelopment())
			{
                app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();

            app.UseSession();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
