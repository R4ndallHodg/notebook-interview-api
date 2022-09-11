﻿using Microsoft.EntityFrameworkCore;
using notebook_api.Data;
using notebook_api.Services;

namespace notebook_api
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly string PolicyName = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers();

            // Entity framework core configuration. You can use this instruction to configure database related functionality and services.
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            // Automapper configuration
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<INoteService, NoteService>();

            services.AddCors(options =>
            {
                string frontendURL = configuration.GetValue<string>("frontend_url");
                options.AddPolicy(name:PolicyName, builder =>
                {
                    builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(PolicyName);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
