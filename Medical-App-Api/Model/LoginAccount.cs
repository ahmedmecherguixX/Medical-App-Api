using System.ComponentModel.DataAnnotations;

namespace Medical_App_Api.Model
{
    public class LoginAccount
    {
        [Key]
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}