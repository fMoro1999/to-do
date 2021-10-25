using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public class ToDoService
    {
        private readonly ToDoRepository _toDoRepo;

        public ToDoService(ToDoRepository repo)
        {
            _toDoRepo = repo;
        }

        public List<ToDo> GetAll() => _toDoRepo.ToDos.Find(toDo => true).ToList();

        public ToDo Get(string id) => _toDoRepo.ToDos.Find(toDo => toDo.Id == id)
                                                     .First();

        public ToDo Add(ToDo toDo)
        {
            _toDoRepo.ToDos.InsertOne(toDo);
            return toDo;
        }

        public void Update(string id, ToDo toDo)
        { 
            var filter = Builders<ToDo>.Filter.Eq(x => x.Id, id);
            var update = Builders<ToDo>.Update.Set(x => x.Description, toDo.Description).Set(x => x.IsDone, toDo.IsDone);
            var result = _toDoRepo.ToDos.UpdateOne(filter, update);
        }

        public void Delete(ToDo toDo) => _toDoRepo.ToDos.DeleteOne(e => e.Id == toDo.Id);

        public void DeleteAll() => _toDoRepo.ToDos.DeleteMany(_ => true);

        public void Delete(string id) => _toDoRepo.ToDos.DeleteOne(e => e.Id == id);
    }
}