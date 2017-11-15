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
    public class IndexModel : PageModel
    {
        private readonly HRApplication.Models.PositionContext _context;

        public IndexModel(HRApplication.Models.PositionContext context)
        {
            _context = context;
        }

        public IList<Application> Application { get;set; }

        public async Task OnGetAsync()
        {
            Application = await _context.Application.ToListAsync();
        }
    }
}
