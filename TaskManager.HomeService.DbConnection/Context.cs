using MongoDB.Driver;
using TaskManager.Core;
using TaskManager.HomeService.DbConnection.Entities;

namespace TaskManager.HomeService.DbConnection
{
    public class Context : IContext
    {
        private readonly IMongoDatabase _database;

        public Context(IDbConnectionSettings connectionSettings)
        {
            _database = new MongoClient(connectionSettings.ConnectionString).GetDatabase("TaskManagerHome");
        }

        public IMongoCollection<UserEntity> Users => _database.GetCollection<UserEntity>("Users");
    }
}