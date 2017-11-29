using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HRApplication.Data;
using HRApplication.Models;

namespace HRApplication.Pages.Notifications
{
    public class DetailsModel : PageModel
    {
        private readonly HRApplication.Data.NotificationContext _context;

        public DetailsModel(HRApplication.Data.NotificationContext context)
        {
            _context = context;
        }

        public Notification Notification { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Notification = await _context.Notification.SingleOrDefaultAsync(m => m.ID == id);

            if (Notification == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
