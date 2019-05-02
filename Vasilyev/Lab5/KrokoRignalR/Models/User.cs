namespace KrokoRignalR.Models
{
    public class User
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public Role UserRole { get; set; }
    }

    public enum Role
    {
        Admin,
        User
    }
}