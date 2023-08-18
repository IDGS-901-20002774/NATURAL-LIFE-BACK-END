using Microsoft.EntityFrameworkCore;
using NaturalLife.Context;

namespace NaturalLife
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.SetIsOriginAllowedToAllowWildcardSubdomains()
                           .WithOrigins("http://localhost:4200", "http://192.168.1.74:4200", "http://10.16.12.175:4200", "http://192.168.147.53")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Conexion")));
            // ...
        }



        public void Configure(IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            // ...
        }
    }
}
