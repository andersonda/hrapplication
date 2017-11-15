using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HRApplication.Models;

namespace HRApplication.Pages.Applications
{
    public class DetailsModel : PageModel
    {
        private readonly HRApplication.Models.PositionContext _context;

        public DetailsModel(HRApplication.Models.PositionContext context)
        {
            _context = context;
        }

        public Application Application { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Application = await _context.Application.SingleOrDefaultAsync(m => m.ID == id);

            if (Application == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
