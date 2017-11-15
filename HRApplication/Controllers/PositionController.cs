using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HRApplication.Models;

namespace HRApplication.Controllers
{
    public class PositionController : Controller
    {
        private readonly PositionContext _context;

        public PositionController(PositionContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<String> names = (from t in typeof(Position).GetProperties()
                         select t.Name).ToList();

            ViewBag.ListOfSearchCriteria = names;
            return View();
        }
    }
}