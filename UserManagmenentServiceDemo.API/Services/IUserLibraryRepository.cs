using UserManagmenentServiceDemo.API.Models;
using UserManagmenentServiceDemo.API.Models.User;

namespace UserManagmenentServiceDemo.API.Services
{
    public interface IUserLibraryRepository
    {
        Task<List<UserInfoModel>> GetAllUsers(UserResourceParameters userResourceParameters);
        Task<UserInfoModel> GetUserByUsername(string username);
        Task<UserInfoModel> EditUser(UserInfoModel user);


    }
}
