

namespace MauiApp1.Model
{
     public class TaskItem
    {
        public TaskItem(string name)
        {
            Name = name;
        }


        public  string Name { get; private set; }
        public string Description { get; private set; }
    }
}
