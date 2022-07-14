using Microsoft.AspNetCore.Identity;
using UserManagmenentServiceDemo.API.Context;
using UserManagmenentServiceDemo.API.Models;
using UserManagmenentServiceDemo.API.Models.User;

namespace UserManagmenentServiceDemo.API.Services
{
    public class UserLibraryRepository : IUserLibraryRepository
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserLibraryRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager;
            _roleManager = roleManager;
        }
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
