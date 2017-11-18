using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HRApplication.Models;
using HRApplication.Data;

namespace HRApplication.Pages.Applications
{
    public class DetailsModel : PageModel
    {
        private readonly HRApplication.Models.PositionContext _context;
        private readonly UserProfileContext _profileContext;

        public DetailsModel(HRApplication.Models.PositionContext context, UserProfileContext profileContext)
        {
            _context = context;
            _profileContext = profileContext;
        }

        public Application Application { get; set; }

        public Position Position { get; set; }

        public UserProfile Profile { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Application = await _context.Application.SingleOrDefaultAsync(m => m.ID == id);
            Position = await _context.Position.SingleOrDefaultAsync(m => m.ID == Application.PositionID);
            Profile = await _profileContext.UserProfile.SingleOrDefaultAsync(m => m.ID == Application.ApplicantID);

            if (Application == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
