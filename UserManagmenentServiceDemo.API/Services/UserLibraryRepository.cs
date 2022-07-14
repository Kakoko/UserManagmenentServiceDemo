using UserManagmenentServiceDemo.API.Models;

namespace UserManagmenentServiceDemo.API.Services
{
    public class UserLibraryRepository : IUserLibraryRepository
    {
        public Task<ApplicationUser> EditUser(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<List<ApplicationUser>> GetAllUsers(string flag)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
