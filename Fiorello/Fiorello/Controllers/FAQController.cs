using Fiorello.DAL;
using Fiorello.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fiorello.Controllers
{
    public class FAQController : Controller
    {
        private readonly AppDbContext _db;

        public FAQController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string searchString)
        {
            var faq = from p in _db.Faqs
                      select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                faq = faq.Where(p => p.Title.Contains(searchString)
                                       || p.Description.Contains(searchString));
            }

            return View(faq.ToList());
           
        }
    }
}
