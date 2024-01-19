
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecret"])),
          ClockSkew = TimeSpan.Zero
        });

      services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

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

             config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
             {
               Name = "Authorization",
               Type = SecuritySchemeType.ApiKey,
               Scheme = "Bearer",
               BearerFormat = "JWT",
               In = ParameterLocation.Header
             });

             config.AddSecurityRequirement(new OpenApiSecurityRequirement
              {
                {
                  new OpenApiSecurityScheme
                  {
                      Reference = new OpenApiReference
                      {
                          Type = ReferenceType.SecurityScheme,
                          Id = "Bearer"
                      }
                  },
                  new string[] {}
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
