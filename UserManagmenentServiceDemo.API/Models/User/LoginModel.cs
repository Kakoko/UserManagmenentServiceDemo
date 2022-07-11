using System.ComponentModel.DataAnnotations;

namespace UserManagmenentServiceDemo.API.Models.User
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = String.Empty;
    }
}
