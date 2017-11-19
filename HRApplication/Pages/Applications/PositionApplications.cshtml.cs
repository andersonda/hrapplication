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
    public class PositionApplicationsModel : PageModel
    {
        private readonly HRApplication.Models.PositionContext _context;

        public PositionApplicationsModel(HRApplication.Models.PositionContext context)
        {
            _context = context;
        }

        public List<Application> Application { get;set; }

        public List<UserProfile> Applicants { get; set; }

        public async Task OnGetAsync(string id)
        {
            Application = _context.GetApplicationsForPosition(id);
            Applicants = _context.GetUserProfilesForApplications(Application);
        }
    }
}
