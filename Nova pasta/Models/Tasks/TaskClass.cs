namespace todo.Models.Tasks
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IdUser { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public bool Completed { get; set; }
        public bool Removed { get; set; }
    }
}
