namespace UserManagmenentServiceDemo.API.Models.User
{
    public class ChangePasswordResetModel
    {
        public string Username { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string CurrentPassword { get; set; } = string.Empty;
    }
}
