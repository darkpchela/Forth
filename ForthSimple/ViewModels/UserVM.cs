namespace ForthSimple.ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Blocked { get; set; }
    }
}