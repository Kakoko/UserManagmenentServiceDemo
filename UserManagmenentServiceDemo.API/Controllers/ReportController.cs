using Microsoft.AspNetCore.Mvc;
using UserManagmenentServiceDemo.API.Models.User;
using UserManagmenentServiceDemo.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserManagmenentServiceDemo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {


        private readonly IUserLibraryRepository _userLibraryRepository;

        public ReportController(IUserLibraryRepository userLibraryRepository)
        {   
            _userLibraryRepository = userLibraryRepository ??
                throw new ArgumentNullException(nameof(userLibraryRepository));
        }
        // GET: api/<ReportController>
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetInternalUsersAsync([FromQuery] UserResourceParameters userResourceParameters)
        {
            var users = await _userLibraryRepository.GetAllUsers(userResourceParameters);
            return Ok(users);
        }

        // GET api/<ReportController>/5
        [HttpGet]
        [Route("getusers-by-id")]
        public async Task<IActionResult> GetUsersById([FromQuery] string userName)
        {

            if (userName == null)
            {
                return BadRequest();
            }

            var usersFromRepo = await _userLibraryRepository.GetUserByUsername(userName);


            return Ok(usersFromRepo);
        }

        // PUT api/<ReportController>/5
        [HttpPut]
        [Route("edit-user")]
        public async Task<ActionResult<EditUserModel>> EditInternalUser(EditUserModel userModel)
        {
            if (userModel == null)
            {
                return BadRequest();
            }

            return await _userLibraryRepository.EditUser(userModel);
        }

    }
}
