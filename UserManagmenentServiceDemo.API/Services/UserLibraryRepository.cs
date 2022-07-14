using UserManagmenentServiceDemo.API.Models;
using UserManagmenentServiceDemo.API.Models.User;

namespace UserManagmenentServiceDemo.API.Services
{
    public class UserLibraryRepository : IUserLibraryRepository
    {
        public Task<UserInfoModel> EditUser(UserInfoModel user)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserInfoModel>> GetAllUsers(UserResourceParameters userResourceParameters)
        {
            throw new NotImplementedException();
        }   

        public Task<UserInfoModel> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
