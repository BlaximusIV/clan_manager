namespace ClanManagerApi.Models.Security
{
    public class User
    {
        public string UserName { get; set; }
        // Need a secure alternative for protecting in-memory sensitive data
        public string Password { get; set; }
    }
}
