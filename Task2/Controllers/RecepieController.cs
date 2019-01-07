using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task2.Data;
using Task2.Models;
using Task2.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Task2.Controllers
{
    public class RecepieController : Controller
    {

        ApplicationDbContext db;
        public RecepieController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult CreateNewRecepie() //показать
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewRecepie(Recepie recepie)// не видно
        {
            recepie.DateOfCreation = DateTime.Now;
            db.RecepieCollection.Add(recepie);
            db.SaveChanges();
            return RedirectToAction("RecepieCollection");
        }

        [HttpGet]
        public IActionResult ViewRecepie(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditRecepie(int id)
        {
            return View();
        }

        public async Task<IActionResult> RecepieCollection(int page = 1)
        {
            IQueryable<Recepie> recepie = db.RecepieCollection;
            recepie = recepie.OrderByDescending(s => s.DateOfCreation);
            int pageSize = 5;
            var count = await recepie.CountAsync();
            var items = await recepie.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PageIndexViewModel viewModel = new PageIndexViewModel
            {
                PageViewModel = pageViewModel,
                EnumRecepie = items
            };
            return View(viewModel);
        }

        public IActionResult RecepieManagement()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteRecepie(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult ApplyEditing()
        {
            return View();
        }
    }
}