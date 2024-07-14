using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegistrationWizard.DAL.Repository;
using RegistrationWizard.DTO;
using RegistrationWizard.Models;
using RegistrationWizard.Services;

namespace RegistrationWizard.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO request)
        {
            var errorMessage = await _userService.CreateNewUser(request);
            return string.IsNullOrEmpty(errorMessage) ? Ok() : BadRequest(errorMessage);
        }
    }
}