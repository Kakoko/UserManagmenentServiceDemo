using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagmenentServiceDemo.API.Models;
using UserManagmenentServiceDemo.API.Models.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagmenentServiceDemo.API.Controllers
{

    //[Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {



            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user != null && user.ActivationStatus && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);



                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    userId = user.Id,
                    role = userRoles[0]
                });

            }
            return Unauthorized();
        }

        
        // POST api/<UserController>
        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> PostUser([FromBody] RegisterModelUsers adminModelUser)
        {
            if(!(adminModelUser.Role == UserRoles.Normal || adminModelUser.Role == UserRoles.Admin || adminModelUser.Role == UserRoles.HeadOfDepartment))
            {
                return  StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Role does not exist!" });
            }
            //Check to see if user exists
            var userExists = await _userManager.FindByEmailAsync(adminModelUser.Email);

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User already exists!" });
            }

            var user = new ApplicationUser()
            {
                Email = adminModelUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = adminModelUser.Email,
                FirstName = adminModelUser.FirstName,
                LastName = adminModelUser.LastName,
                PhoneNumber = adminModelUser.Phone,
            };

            var result = await _userManager.CreateAsync(user, adminModelUser.Password);

            if (!result.Succeeded)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Normal))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Normal));
            if (!await _roleManager.RoleExistsAsync(UserRoles.HeadOfDepartment))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.HeadOfDepartment));


           
           await _userManager.AddToRoleAsync(user, adminModelUser.Role);
            

            return Ok(new Response { Status = "Success", Message = $"{user.Email} created successfully!" });
        }


        [HttpPut]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordResetModel resetPasswordModel)
        {

            var userExists = await _userManager.FindByNameAsync(resetPasswordModel.Username);

            if (userExists == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = $"User does not exists!" });
            }


            var passwordValidator = new PasswordValidator<ApplicationUser>();
            var passwordValidatorResult = await _userManager.CheckPasswordAsync(userExists, resetPasswordModel.CurrentPassword);

            if (passwordValidatorResult)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(userExists);

                var result = await _userManager.ResetPasswordAsync(userExists, token, resetPasswordModel.NewPassword);

                if (result.Succeeded)
                {
                    return Ok(new Response { Status = "Success", Message = $"Password changed for {userExists.UserName}, successfully!" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Failed to change password" });
                }
            }

            return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Message = "Current password not correct" });


        }

        [HttpPut]
        [Route("change-status")]
        public async Task<IActionResult> ActivateUser([FromBody] UserStatusModel userStatus)
        {

            var userExists = await _userManager.FindByNameAsync(userStatus.Username);
            if (userExists == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User does not exists!" });
            }

            var userActivationStatus = userExists.ActivationStatus;


            if (userActivationStatus && userStatus.Flag == false)
            {
                userExists.ActivationStatus = false;

                var result = await _userManager.UpdateAsync(userExists);

                if (result.Succeeded)
                {
                    return Ok(new Response { Status = "Success", Message = "User de-activated successfully!" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Failed to activate user" });
                }
            }
            else if(!userActivationStatus && userStatus.Flag)
            {
                userExists.ActivationStatus = true;

                var result = await _userManager.UpdateAsync(userExists);

                if (result.Succeeded)
                {
                    return Ok(new Response { Status = "Success", Message = "User activated successfully!" });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Failed to activate user" });
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already in requested state" });
            }
        }
    }
}
