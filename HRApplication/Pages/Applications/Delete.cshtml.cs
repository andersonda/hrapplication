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
    public class DeleteModel : PageModel
    {
        private readonly HRApplication.Models.PositionContext _context;

        public DeleteModel(HRApplication.Models.PositionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Application Application { get; set; }

        public Position Position { get; set; }

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

            Position = await _context.Position.SingleOrDefaultAsync(m => m.ID == Application.PositionID);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Application = await _context.Application.FindAsync(id);

            if (Application != null)
            {
                _context.Application.Remove(Application);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./MyApplications");
        }
    }
}
