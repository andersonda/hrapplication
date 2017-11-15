using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HRApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HRApplication.Pages.Positions
{
    public class IndexModel : PageModel
    {
        private readonly HRApplication.Models.PositionContext _context;

        public IndexModel(HRApplication.Models.PositionContext context)
        {
            _context = context;
        }

        public IList<Position> Position { get;set; }

        public IList<SelectListItem> SearchCriteria { get; set; }

        [BindProperty]
        public string SearchText { get; set; }

        [BindProperty]
        public string SearchBy { get; set; }

        public async Task OnGetAsync()
        {
            if(Position == null)
            {
                Position = await _context.Position.ToListAsync();
            }
            if(SearchCriteria == null)
            {
                SearchCriteria = (from t in typeof(Position).GetProperties()
                                  select new SelectListItem { Text = t.Name, Value = t.Name }).ToList();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(Position == null)
            {
                Position = await _context.Position.ToListAsync();
            }
            if(SearchCriteria == null)
            {
                SearchCriteria = (from t in typeof(Position).GetProperties()
                                  select new SelectListItem { Text = t.Name, Value = t.Name }).ToList();
            }


            if (SearchBy == null)
            {
                ModelState.AddModelError("", "Choose Search By");
            }
            else if (SearchText == null)
            {
                ModelState.AddModelError("", "Enter Search Criteria");
            }
            else
            {
                Position = _context.getPositionsByCriteria(SearchBy, SearchText);
            }

            return Page();
        }
    }
}
