using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRApplication.Models;
using Microsoft.AspNetCore.Identity;
using HRApplication.Data;

namespace HRApplication.Pages.Profile
{
    public class EditModel : PageModel
    {
        private readonly HRApplication.Models.UserProfileContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditModel(HRApplication.Models.UserProfileContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public UserProfile UserProfile { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            UserProfile = _context.UserProfile.Find(user.Id.ToString());

            if (UserProfile == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserProfile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return RedirectToPage("/Index");
        }
    }
}
