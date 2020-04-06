using System.IO;
using System.Reflection;
using AutoMapper;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Pokemon.Api.Core.Logging;
using Pokemon.Api.Core.Repositories;
using Pokemon.Api.Core.Services;
using Pokemon.Api.Infrastructure.Data;
using Pokemon.Api.Infrastructure.Repositories;
using Pokemon.Api.Infrastructure.Services;
using Pokemon.Api.Web.Mapper;
using Pokemon.Api.Web.Models;

namespace Pokemon.Api.Web
{
    public class Startup
    {
        private SqliteConnection _inMemorySqlite;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        public IWebHostEnvironment HostingEnvironment { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var mapper = GetMapper();
            services.AddSingleton(mapper);

            //Sql lite in-memory DB
            _inMemorySqlite = new SqliteConnection("Data Source=:memory:");
            _inMemorySqlite.Open();
            services.AddDbContext<PokemonContext>(options =>
            {
                options.UseSqlite(_inMemorySqlite);
            });

            services.AddScoped<IPokemonRepository, PokemonRepository>();
            services.AddScoped<IPokemonService, PokemonService>();
            services.AddSingleton<ILoggingService, Log4NetLoggingService>();

            services.AddMvc(options => options.EnableEndpointRouting = false)
                .AddNewtonsoftJson();
        }

        IMapper GetMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            return mappingConfig.CreateMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePagesWithReExecute("/api/error");
            app.UseExceptionHandler("/api/error");
            app.UseMvc();

            var opts = new DbContextOptionsBuilder<PokemonContext>()
                .UseSqlite(_inMemorySqlite)
                .Options;

            //TODO: Move to service class. Error handling, logging.
            var path = Path.Combine(env.WebRootPath,"pokemon");
            var files = Directory.GetFiles(path);
            using (var context = new PokemonContext(opts))
            {
                // Create the schema in the database
                var mapper = GetMapper();
                context.Database.EnsureCreated();
                foreach (var file in files)
                {
                    var jsonString = File.ReadAllText(file);
                    var pokemonDto = JsonConvert.DeserializeObject<PokemonDto>(jsonString);
                    var pokemon = mapper.Map(pokemonDto, new Core.Entities.Pokemon());
                    context.Pokemon.Add(mapper.Map(pokemonDto, pokemon));
                }

                context.SaveChanges();
            }
        }
    }
}