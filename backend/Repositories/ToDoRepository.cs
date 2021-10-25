using MongoDB.Driver;
using backend.Models;

namespace backend.Repositories
{
    public class ToDoRepository
    {
        public IMongoCollection<ToDo> ToDos {get;}

        public ToDoRepository(IToDoStoreDatabaseSettings settings)
        {
            MongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.DatabaseName);

            ToDos = database.GetCollection<ToDo>(settings.ToDosCollectionName);
        }
    }
}