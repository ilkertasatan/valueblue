using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using ValueBlue.MovieSearch.Domain;
using ValueBlue.MovieSearch.Domain.RequestEntries;
using ValueBlue.MovieSearch.Infrastructure.DataAccess.Repositories;

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
            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            BsonClassMap.RegisterClassMap<RequestEntry>(
                map =>
                {
                    map.AutoMap();
                    map.MapProperty(x => x.Id).SetSerializer(new GuidSerializer(BsonType.String));
                    map.MapProperty(x => x.SearchToken).SetElementName("search_token");
                    map.MapProperty(x => x.ImdbId).SetElementName("imdbID");
                    map.MapProperty(x => x.ProcessingTime).SetElementName("processing_time_ms");
                    map.MapProperty(x => x.Timestamp).SetElementName("timestamp");
                    map.MapProperty(x => x.IpAddress).SetElementName("ip_address");
                });

            return services;
        }
    }
}