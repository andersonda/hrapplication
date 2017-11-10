using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HRApplication.Models;

namespace HRApplication.Pages.Profile
{
    public class CreateModel : PageModel
    {
        private readonly HRApplication.Models.UserProfileContext _context;

        public CreateModel(HRApplication.Models.UserProfileContext context)
        {
            _context = context;
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

            _context.UserProfile.Add(UserProfile);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}