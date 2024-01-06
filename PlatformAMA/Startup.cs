using Microsoft.EntityFrameworkCore;

namespace PlatformAMA {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services) {
			services.AddControllers();
			
			// database conection
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
			);

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			services.AddAutoMapper(typeof(Startup));
		}

		public void Configure(WebApplication app, IWebHostEnvironment env) {
			// Configure the HTTP request pipeline.
			if (env.IsDevelopment()) {
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			// ==== database creation ====
			using (var serviceScope = app.Services.CreateScope()) {
				var services = serviceScope.ServiceProvider;
				var context = services.GetRequiredService<ApplicationDbContext>();
				context.Database.EnsureCreated();
				// await context.Database.MigrateAsync();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
		}
	}
}