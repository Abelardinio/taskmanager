using MongoDB.Bson;
using MongoDB.Driver;
using TaskManager.HomeService.DbConnection.Entities;

namespace TaskManager.HomeService.DbConnection
{
    public interface IContext
    {
        IMongoCollection<UserEntity> Users { get; }
    }
}