namespace DissertationProject.Models
{
    public class User
    {
        public int UserID { get; set; } // Primary Key
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

}
