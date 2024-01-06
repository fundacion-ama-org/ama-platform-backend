

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace PlatformAMA
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();

      services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
      );

      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen(config =>
           {
             config.SwaggerDoc("v1", new OpenApiInfo
             {
               Title = "Plataforma Fundación AMA",
               Version = "v1",
               Description = "REST API para la Plataforma Fundación AMA",
               Contact = new OpenApiContact
               {
                 Email = "fundacionamasoftware@gmail.com",
                 Name = "Fundación AMA",
               }
             });

             var XMLFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
             var XMLPath = Path.Combine(AppContext.BaseDirectory, XMLFile);

             config.IncludeXmlComments(XMLPath);
           });

      services.AddAutoMapper(typeof(Startup));

      services.AddApplicationInsightsTelemetry();

      services.AddCors(options =>
      {
        options.AddDefaultPolicy(builder =>
        {
          builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
      });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // Configure the HTTP request pipeline.
      if (env.IsDevelopment())
      {
      }

      app.UseSwagger();
      app.UseSwaggerUI();


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