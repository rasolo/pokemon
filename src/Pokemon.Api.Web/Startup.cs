using AutoMapper;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokemon.Api.Core.Logging;
using Pokemon.Api.Core.Repositories;
using Pokemon.Api.Core.Services;
using Pokemon.Api.Infrastructure.Data;
using Pokemon.Api.Infrastructure.Repositories;
using Pokemon.Api.Infrastructure.Services;
using Pokemon.Api.Web.Mapper;
using System.Buffers;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Pokemon.Api.Web.V1._1._0.Models;

namespace Pokemon.Api.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        }

        public IWebHostEnvironment HostingEnvironment { get; }
        public IConfiguration Configuration { get; }
        private SqliteConnection _inMemorySqlite;

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
            _inMemorySqlite = new SqliteConnection("Data Source=:memory:");
            _inMemorySqlite.Open();
            services.AddDbContext<PokemonContext>(options =>
            {
                options.UseSqlite(_inMemorySqlite);
            });

            DbContextOptions<PokemonContext> opts = new DbContextOptionsBuilder<PokemonContext>()
                    .UseSqlite(_inMemorySqlite)
                    .Options;

            services.AddScoped<IBufferWriter<byte>, ArrayBufferWriter>();

            //TODO: Move to service class. Error handling, logging.
            var path = Path.GetFullPath(Path.Combine(HostingEnvironment.ContentRootPath, @"..\..\")) + "\\appdata\\json\\pokemon";
            var files = Directory.GetFiles(path);
            using (var context = new PokemonContext(opts))
            {
                // Create the schema in the database
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


            services.AddScoped<IPokemonRepository, PokemonRepository>();
            services.AddScoped<IPokemonService, PokemonService>();
            services.AddSingleton<ILoggingService, Log4NetLoggingService>();

            //services.AddApiVersioning(o =>
            //{
            //    o.AssumeDefaultVersionWhenUnspecified = true;
            //    o.DefaultApiVersion = new ApiVersion(1, 0);
            //});

            services.AddMvc(options => options.EnableEndpointRouting = false)
                .AddNewtonsoftJson();
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
        }
    }
}
