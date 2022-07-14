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

        public async Task<List<UserInfoModel>> GetAllUsers(UserResourceParameters userResourceParameters)
        {


            var userInfo = new List<UserInfoModel>();

            var users = _context.Users.ToList();

            if(userResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(userResourceParameters));
            }


            if (string.IsNullOrWhiteSpace(userResourceParameters.UserRole))
            {
                foreach (var user in users)
                {
                    var userFromDb = await _userManager.FindByNameAsync(user.UserName);
                    var userRoles = await _userManager.GetRolesAsync(userFromDb);
                    var userRole = userRoles[0].Trim();

                    if (userRole == UserRoles.Admin
                   || userRole == UserRoles.HeadOfDepartment || userRole == UserRoles.Normal)
                    {
                        userInfo.Add(new UserInfoModel()
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            PhoneNumber = user.PhoneNumber,
                            Email = user.Email,
                            Username = user.UserName,
                            Role = userRole,
                            UserActivationStatus = user.ActivationStatus

                        });
                    }
                }

                return userInfo;
            }

            // For Admins
            if (userResourceParameters.UserRole == UserRoles.Admin)
            {
                //var userRole = userResourceParameters.UserRole.Trim();

                foreach (var user in users)
                {

                    var userz = await _userManager.FindByNameAsync(user.UserName);
                    var userRoles = await _userManager.GetRolesAsync(userz);
                    var userRole = userRoles[0].Trim();


                    if (userRole == UserRoles.Admin)
                    {
                        userInfo.Add(new UserInfoModel()
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            PhoneNumber = user.PhoneNumber,
                            Email = user.Email,
                            Username = user.UserName,
                            Role = userRole,
                            UserActivationStatus = user.ActivationStatus

                        });



                    }
                }

                return userInfo;
            }


            //For Head of Department
            if (userResourceParameters.UserRole == UserRoles.HeadOfDepartment)
            {
                //var userRole = userResourceParameters.UserRole.Trim();

                foreach (var user in users)
                {

                    var userz = await _userManager.FindByNameAsync(user.UserName);
                    var userRoles = await _userManager.GetRolesAsync(userz);
                    var userRole = userRoles[0].Trim();


                    if (userRole == UserRoles.HeadOfDepartment)
                    {
                        userInfo.Add(new UserInfoModel()
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            PhoneNumber = user.PhoneNumber,
                            Email = user.Email,
                            Username = user.UserName,
                            Role = userRole,
                            UserActivationStatus = user.ActivationStatus

                        });



                    }
                }

                return userInfo;
            }

            //For Normal User
            if (userResourceParameters.UserRole == UserRoles.Normal)
            {
                //var userRole = userResourceParameters.UserRole.Trim();

                foreach (var user in users)
                {

                    var userz = await _userManager.FindByNameAsync(user.UserName);
                    var userRoles = await _userManager.GetRolesAsync(userz);
                    var userRole = userRoles[0].Trim();


                    if (userRole == UserRoles.Normal)
                    {
                        userInfo.Add(new UserInfoModel()
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            PhoneNumber = user.PhoneNumber,
                            Email = user.Email,
                            Username = user.UserName,
                            Role = userRole,
                            UserActivationStatus = user.ActivationStatus

                        });



                    }
                }

                return userInfo;
            }

            return new List<UserInfoModel>()
            {
                new UserInfoModel()
                {
                    Username = "No Records Founds"
                }
            };



        }   

        public Task<UserInfoModel> GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
