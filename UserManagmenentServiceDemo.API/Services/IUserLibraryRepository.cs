using UserManagmenentServiceDemo.API.Models;

namespace UserManagmenentServiceDemo.API.Services
{
    public interface IUserLibraryRepository
    {
        Task<List<ApplicationUser>> GetAllUsers(string flag);
        Task<ApplicationUser> GetUserByUsername(string username);
        Task<ApplicationUser> EditUser(ApplicationUser user);


    }
}
