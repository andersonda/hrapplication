using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HRApplication.Models;

namespace HRApplication.Pages.Profile
{
    public class DeleteModel : PageModel
    {
        private readonly HRApplication.Models.UserProfileContext _context;

        public DeleteModel(HRApplication.Models.UserProfileContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserProfile UserProfile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserProfile = await _context.UserProfile.SingleOrDefaultAsync(m => m.ID == id);

            if (UserProfile == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserProfile = await _context.UserProfile.FindAsync(id);

            if (UserProfile != null)
            {
                _context.UserProfile.Remove(UserProfile);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
