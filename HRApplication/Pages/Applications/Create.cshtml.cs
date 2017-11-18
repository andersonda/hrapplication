using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HRApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HRApplication.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace HRApplication.Pages.Applications
{
    public class CreateModel : PageModel
    {
        private readonly HRApplication.Models.PositionContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateModel(HRApplication.Models.PositionContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Application Application { get; set; }

        [BindProperty]
        public Position Position { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return Redirect("/Positions");
            }
            else
            {
                Position = await _context.Position.SingleOrDefaultAsync(m => m.ID == id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile resume, IFormFile coverLetter)
        {
            if (resume == null || resume.Length == 0)
            {
                ModelState.AddModelError("", "Resume file not selected");
                return Page();
            }
            if (coverLetter == null || coverLetter.Length == 0)
            {
                ModelState.AddModelError("", "Cover Letter file not selected");
                return Page();
            }

            // save resume on server
            var resumePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", resume.FileName);
            using (var stream = new FileStream(resumePath, FileMode.Create))
            {
                await resume.CopyToAsync(stream);
            }

            // save cover letter on server
            var coverLetterPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", coverLetter.FileName);
            using (var stream = new FileStream(coverLetterPath, FileMode.Create))
            {
                await coverLetter.CopyToAsync(stream);
            }

            // create new application and save it in db
            Application.ApplicantID = (await _userManager.GetUserAsync(HttpContext.User)).Id.ToString();
            Application.PositionID = Position.ID;
            Application.ResumePath = resume.FileName;
            Application.CoverLetterPath = coverLetter.FileName;
            Application.Status = ApplicationStatus.Submitted;

            _context.Application.Add(Application);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}