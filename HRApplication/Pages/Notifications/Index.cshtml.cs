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
    public class IndexModel : PageModel
    {
        private readonly HRApplication.Data.NotificationContext _context;

        public IndexModel(HRApplication.Data.NotificationContext context)
        {
            _context = context;
        }

        public IList<Notification> Notification { get;set; }

        public async Task OnGetAsync()
        {
            Notification = await _context.Notification.ToListAsync();
        }
    }
}
