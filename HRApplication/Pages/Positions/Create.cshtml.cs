using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HRApplication.Models;

namespace HRApplication.Pages.Positions
{
    public class CreateModel : PageModel
    {
        private readonly HRApplication.Models.PositionContext _context;

        public CreateModel(HRApplication.Models.PositionContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Position Position { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Position.Add(Position);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}