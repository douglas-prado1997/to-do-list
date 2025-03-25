namespace todo.Models.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTimeOffset CreationDate { get; set; }
        public bool IsSysAdmin { get; set; }
        public bool Removed { get; set; }
    }
}
