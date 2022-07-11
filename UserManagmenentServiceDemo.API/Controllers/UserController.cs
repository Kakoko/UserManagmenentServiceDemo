using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagmenentServiceDemo.API.Models;
using UserManagmenentServiceDemo.API.Models.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagmenentServiceDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> Post([FromBody] RegisterAdminModelUsers adminModelUser)
        {
            //Check to see if user exists
            var userExists = await _userManager.FindByEmailAsync(adminModelUser.Email);

            if(userExists != null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
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


            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new Response { Status = "Success", Message = "Administrator created successfully!" });
        }


        // POST api/<UserController>
        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> PostUser([FromBody] RegisterAdminModelUsers adminModelUser)
        {
            //Check to see if user exists
            var userExists = await _userManager.FindByEmailAsync(adminModelUser.Email);

            if (userExists != null)
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
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


            if (await _roleManager.RoleExistsAsync(UserRoles.Normal))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Normal);
            }

            return Ok(new Response { Status = "Success", Message = $"{user.Email} created successfully!" });
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
