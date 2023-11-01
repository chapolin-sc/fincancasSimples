using financasSimples.Api.Extensions;
using financasSimples.Api.interfaces;
using financasSimples.Api.Services;
using financasSimples.IoC;

namespace financasSimples.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public async void ConfigureServices(IServiceCollection services)
    {
       
        
        await services.AddDbInfraAsync(Configuration, "sqlite");
        services.AddIdentity(Configuration);
        services.AddRepositeriesInfra();
        services.AddAuthenticationSetup(Configuration);

        services.AddControllers();
                
        services.AddScoped<IFileSaveService, FileSaveService>();
        services.AddScoped<IMetodosAuxiliares, MetodosAuxiliares>();
        services.AddScoped<IFileS3Transfer, FileS3Transfer>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}