using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Connector;
using APIEntities.AccuWeather.Models.Interfaces.Connector;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIFlow.Entities.Interfaces.Workflow;
using APIFlow.Workflow;
using APIFlow.DataLayer;
using APIFlow.Entities.Interfaces.DataLayer;

namespace AccuWeatherAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(typeof(AccuWeatherAPI.Startup));

            services.AddSwaggerGen(c => c.SwaggerDoc(name: "v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Realcommerce Swagger", Version = "v1" }));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AllowNullDestinationValues = true;
            });

            #region DI and mappings

            services.AddSingleton<IWeatherWorkflow, WeatherWorkflow>();
            services.AddSingleton<IWeatherDataLayer, WeatherDataLayer>();
            services.AddSingleton<IWeatherConnector, WeatherConnector>();

            services.AddSingleton<Repository.Entities.Interfaces.Workflow.IWeatherWorkflow, Repository.Workflow.WeatherWorkflow>();
            services.AddSingleton<Repository.Entities.Interfaces.DataLayer.IWeatherDataLayer, Repository.DataLayer.WeatherDataLayer>();
            services.AddSingleton<Repository.Entities.Interfaces.Repository.IWeatherRepository, Repository.Repository.WeatherRepository>();

            #endregion

            #region Register key params from appsettings.json

            services.Configure<Repository.Entities.Configuration.AuthDBConnectionOptions>(options =>
            {
                options.WhatherConnectionString = Configuration.GetSection("WeatherDB:ConnectionString").Value;
            });

            services.Configure<Repository.Entities.Configuration.AuthAccuWeatherOptions>(options =>
            {
                options.ApiKey = Configuration.GetSection("AccuWeatherAPI:ApiKey").Value;
                options.AutocompleteUrl = Configuration.GetSection("AccuWeatherAPI:AutocompleteUrl").Value;
                options.CurrentConditionsUrl = Configuration.GetSection("AccuWeatherAPI:CurrentConditionsUrl").Value;
            });

            #endregion
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
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Realcommerce V1");

            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
