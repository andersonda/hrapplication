using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HRApplication.Models;
using Microsoft.AspNetCore.Identity;
using HRApplication.Data;
using System.IO;

namespace HRApplication.Pages.Applications
{
    public class MyApplicationsModel : PageModel
    {
        private readonly HRApplication.Models.PositionContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyApplicationsModel(HRApplication.Models.PositionContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Application> Application { get;set; }

        public List<Position> Position { get; set; }

        public async Task OnGetAsync()
        {
            string userID = (await _userManager.GetUserAsync(HttpContext.User)).Id.ToString();
            Application = _context.GetApplicationsForUser(userID);
            Position = _context.GetPositionsForApplications(Application);
        }
    }
}
