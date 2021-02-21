using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace ValueBlue.MovieSearch.Api.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(provider =>
            {
                var connectionString = configuration["DbContext:MongoDb:ConnectionString"];
                var databaseName = configuration["DbContext:MongoDb:DatabaseName"];
                
                var mongoClient = new MongoClient(connectionString);
                var mongoDatabase = mongoClient.GetDatabase(databaseName);

                return mongoDatabase;
            });
            
            return services;
        }
    }
}