using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokemon.Api.Mapper;

namespace Pokemon.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private SqliteConnection inMemorySqlite;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Sql lite in-memory DB
            inMemorySqlite = new SqliteConnection("Data Source=:memory:");
            inMemorySqlite.Open();
            services.AddDbContext<Pokemon.Infrastructure.Data.AppDbContext>(options => {
                options.UseSqlite(inMemorySqlite);
            });

            DbContextOptions<Pokemon.Infrastructure.Data.AppDbContext> opts = new DbContextOptionsBuilder<Pokemon.Infrastructure.Data.AppDbContext>()
                    .UseSqlite(inMemorySqlite)
                    .Options;

            using (var context = new Pokemon.Infrastructure.Data.AppDbContext(opts))
            {
                // Create the schema in the database
                context.Database.EnsureCreated();
                context.Pokemon.Add(new Core.Entities.Pokemon() { Name = "Pikachu" });
                context.SaveChanges();
            }

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
