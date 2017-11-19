using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HRApplication.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace HRApplication.Pages.Applications
{
    public class EditModel : PageModel
    {
        private readonly HRApplication.Models.PositionContext _context;

        public EditModel(HRApplication.Models.PositionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Application Application { get; set; }

        [BindProperty]
        public Position Position { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Application = await _context.Application.SingleOrDefaultAsync(m => m.ID == id);
            Position = await _context.Position.SingleOrDefaultAsync(m => m.ID == Application.PositionID);

            if (Application == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile resume, IFormFile coverLetter)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: delete old resume and cover letter from server
            //var oldResume = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Application.ResumePath);
            //if (System.IO.File.Exists(oldResume))
            //{
            //    System.IO.File.Delete(oldResume);
            //}
            //var oldCoverLetter = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Application.ResumePath);
            //if (System.IO.File.Exists(oldCoverLetter))
            //{
            //    System.IO.File.Delete(oldCoverLetter);
            //}

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

            // update db entity with new file names
            Application.ResumePath = resume.FileName;
            Application.CoverLetterPath = coverLetter.FileName;

            _context.Attach(Application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
            }

            return RedirectToPage("./MyApplications");
        }
    }
}
