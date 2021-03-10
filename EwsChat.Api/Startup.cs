using EwsChat.Api.Extensions;
using EwsChat.Api.Middlewares;
using EwsChat.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EwsChat.Api
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
            services.AddSingleton<ILogger>(provider =>
                provider.GetRequiredService<ILogger<ExceptionHandlerMiddleware>>());

            services.ConfigureSwagger();
            services.ConfigureAuthentication(Configuration);
            services.ConfigureAuthorization(Configuration);
            services.AddDbContext<ChatContext>(options => options.UseSqlite("Data Source=ewschat.db"));
            services.AddScoped<IRepositoryFactory, RepositoryFactory>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ChatContext chatContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EwsChat.Api v1"));

            }

            chatContext.Database.EnsureDeleted();
            chatContext.Database.EnsureCreated();

            app.UseExceptionHandlerMiddleware();

            app.UseAuthentication();

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
