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
    public class IndexModel : PageModel
    {
        private readonly HRApplication.Models.UserProfileContext _context;

        public IndexModel(HRApplication.Models.UserProfileContext context)
        {
            _context = context;
        }

        public IList<UserProfile> UserProfile { get;set; }

        public async Task OnGetAsync()
        {
            UserProfile = await _context.UserProfile.ToListAsync();
        }
    }
}
