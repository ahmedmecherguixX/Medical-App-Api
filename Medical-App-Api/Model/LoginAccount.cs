using System.ComponentModel.DataAnnotations;

namespace Medical_App_Api.Model
{
    public class LoginAccount
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}