

using MongoDB.Bson;

namespace MauiApp1.Model
{
     public partial class TaskItem : IRealmObject
    {
        
        private TaskItem(string name)
        {
            Name = name;
        }

        public static TaskItem Create(string name)
        {
            return new TaskItem(name);
        }

        public static TaskItem CreateForDeletion(ObjectId id)
        {
            return new TaskItem(id);
        }

        private TaskItem(ObjectId id)
        {
            Id = id;
        }

        [PrimaryKey]
        [MapTo("_id")]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        public  string Name { get; set; }
        public string Description { get; set; }
    }
}
