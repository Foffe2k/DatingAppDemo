using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    /*"Because we're extending the Service we don't need to include an IServiceCollection as a parameter"*/
    {

        // Services används bla som dependencies för injections. Ramverket skapar dem när det startar 
        // upp och håller själv koll på de objekten under runtime
        services.AddControllers();
        services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection")); //Hur man läser värde från config fil (i detta fall connection string ("DefaultConnection"))
            });

        services.AddCors();
        services.AddScoped<ITokenService, TokenService>(); //<Abstraction, ImplementationClass> Vanligt mönster. 
                                                                   //Funkar lite som en delegat i det att abstraktionen 
                                                                   //lyssnar efter att bli åkallad och sedan avfyrar den 
                                                                   //valda implementationen. Här kan däför ändra implementationsmetoden 
                                                                   //till att använda ett mockinterface istället t.ex. Ska gå att ändra dynamiskt.
        return services;
    }
}