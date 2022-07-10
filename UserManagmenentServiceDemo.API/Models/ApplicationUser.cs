using Microsoft.AspNetCore.Identity;

namespace UserManagmenentServiceDemo.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool ActivationStatus { get; set; } = true;
        public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.Now;
    }
}
