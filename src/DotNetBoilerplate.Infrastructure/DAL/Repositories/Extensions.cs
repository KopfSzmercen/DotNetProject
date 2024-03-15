using DotNetBoilerplate.Core.Entities.ShoppingLists;
using DotNetBoilerplate.Core.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, PostgresUserRepository>();
        services.AddScoped<ITicketRepository, PostgresTicketRepository>();

        services.AddSingleton<IShoppingListRepository, InMemoryShoppingListRepository>();

        return services;
    }
}