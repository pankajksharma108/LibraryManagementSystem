using LibraryManagementSystem.Entity;
using LibraryManagementSystem.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public UserController(IUserService userService, IConfiguration config)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _config = config;
        }

        public async Task<IActionResult> GetUserDetails()
        {
            try
            {
                {
                    IEnumerable<UserEntity> response = await _userService.GetUserDetails();
                    return View(response);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
