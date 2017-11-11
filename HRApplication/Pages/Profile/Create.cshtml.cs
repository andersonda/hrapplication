using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HRApplication.Models;
using Microsoft.AspNetCore.Identity;
using HRApplication.Data;

namespace HRApplication.Pages.Profile
{
    public class CreateModel : PageModel
    {
        private readonly HRApplication.Models.UserProfileContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(HRApplication.Models.UserProfileContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserProfile UserProfile { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);
            // link the user profile to the user account
            UserProfile.ID = user.Id;

            _context.UserProfile.Add(UserProfile);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}