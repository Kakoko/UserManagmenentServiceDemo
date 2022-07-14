namespace UserManagmenentServiceDemo.API.Models.User
{
    public class EditUserModel
    {
        public string UserName { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
