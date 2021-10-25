namespace backend.Models
{
    public class ToDoStoreDatabaseSettings : IToDoStoreDatabaseSettings
    {
        public string ToDosCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IToDoStoreDatabaseSettings
    {
        string ToDosCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}