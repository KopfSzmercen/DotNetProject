using DotNetBoilerplate.Core.BookStores;
using DotNetBoilerplate.Core.Users;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal static class Extensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, PostgresUserRepository>();
        services.AddSingleton<IBookStoreRepository, InMemoryBookStoreRepository>();

        return services;
    }
}