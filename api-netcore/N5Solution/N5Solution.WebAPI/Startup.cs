using Confluent.Kafka;
using Elastic.Clients.Elasticsearch;
//using Elastic.Transport;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using N5Solution.Core.Interfaces;
using N5Solution.Core.Interfaces.Repositories;
using N5Solution.Core.Interfaces.Services;
using N5Solution.Infraestructure.Data;
using N5Solution.Infraestructure.Repositories;
using N5Solution.Services;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace N5Solution.WebAPI
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
            services.AddCors(options => {
                options.AddDefaultPolicy(
                    builder => {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "N5Solution.WebAPI", Version = "v1" });
            });

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(ITipoPermisoRepository), typeof(TipoPermisoRepository));
            services.AddScoped(typeof(IPermisoRepository), typeof(PermisoRepository));
            services.AddScoped(typeof(IPermisoService), typeof(PermisoService));
            
            /* ELASTIC SEARCH */
            string cloudId = Configuration.GetConnectionString("ELASTIC_CLOUD_ID");
            string apiKey = Configuration.GetConnectionString("ELASTIC_API_KEY");
                        
            //var clientElastic = new ElasticsearchClient(cloudId, new ApiKey(apiKey));
            //var elastic = new ElasticService(clientElastic, "N5Index");
            var elasticConnectionSettings =
                new ConnectionSettings(cloudId, new Elasticsearch.Net.ApiKeyAuthenticationCredentials(apiKey))
                .DisableDirectStreaming().EnableApiVersioningHeader().EnableDebugMode();
            
            var client = new ElasticClient(elasticConnectionSettings);
            
            services.AddSingleton<IElasticClient>(client);
            services.AddScoped<ElasticService>();
            /* ELASTIC SEARCH */

            /* KAFKA */

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                EnableDeliveryReports = true,
                ClientId = Dns.GetHostName()
            };

            services.AddSingleton<ProducerConfig>(producerConfig);

            services.AddScoped<KafkaService>();

            /* KAFKA */
            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<N5DataDBContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly("N5Solution.Infraestructure"))
            );

            
                        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "N5Solution.WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<N5DataDBContext>();
                context.Database.Migrate();

                //var elastic = serviceScope.ServiceProvider.GetRequiredService<ElasticService>();
                
            }

            
        }
    }
}
