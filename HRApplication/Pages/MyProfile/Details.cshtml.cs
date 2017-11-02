using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HRApplication.Models;

namespace HRApplication.Pages.MyProfile
{
    public class DetailsModel : PageModel
    {
        private readonly HRApplication.Models.ProfileDbContext _context;

        public DetailsModel(HRApplication.Models.ProfileDbContext context)
        {
            _context = context;
        }

        public Profile Profile { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Profile = await _context.Profile.SingleOrDefaultAsync(m => m.Email == id);

            if (Profile == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
