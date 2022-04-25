using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCmasr.Models;
using System.Threading.Tasks;

namespace MVCmasr.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Index(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            return View(user);
        }
    }
}
