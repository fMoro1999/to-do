using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using backend.Repositories;
using Microsoft.VisualBasic;

namespace backend
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
            /* 
             * Mapping con appsettings e ToDoStoreDatabaseSettings
             * L'istanza di configurazione a cui viene associata la sezione del appsettings.json file viene registrata in ToDoStoreDatabaseSettings
             * Ad esempio, alla proprietà ConnectionString di ToDoStoreDatabaseSettings verrà assegnata quella in ToDoStoreDatabaseSettings:ConnectionString del file appsettings.json .
             */
            services.Configure<ToDoStoreDatabaseSettings>(
                Configuration.GetSection(nameof(ToDoStoreDatabaseSettings)));

            // Viene fatta dependency-injection di IToDoStoreDatabaseSettings, definendola come singleton
            services.AddSingleton<IToDoStoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ToDoStoreDatabaseSettings>>().Value);

            services.AddTransient<ToDoRepository>();

            services.AddSingleton<ToDoService>();

            services.AddMvc(options => options.SuppressAsyncSuffixInActionNames = false);

            services.AddCors(o => o.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "backend", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAllOrigins");

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "backend v1"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}