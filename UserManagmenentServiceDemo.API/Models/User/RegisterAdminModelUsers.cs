using System.ComponentModel.DataAnnotations;

namespace UserManagmenentServiceDemo.API.Models.User
{
    public class RegisterAdminModelUsers
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last  Name is required")]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [Required(ErrorMessage = "Phone Number is required")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
