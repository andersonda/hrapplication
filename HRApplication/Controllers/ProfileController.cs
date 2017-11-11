using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using HRApplication.Data;
using HRApplication.Models;

namespace HRApplication.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly UserProfileContext _context;

        public ProfileController(UserManager<ApplicationUser> userManager,UserProfileContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Route("Profile")]
        public async Task<IActionResult> IndexAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (_context.UserProfile.Find(user.Id.ToString()) == null)
            {
                return Redirect("Profile/Create");
            }
            else
            {
                return Redirect("Profile/Edit");
            }
        }
    }
}