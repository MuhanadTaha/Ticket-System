using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Data;
using TicketSystem.Models;
using TicketSystem.Utility;

namespace TicketSystem.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.ManagerUser)]
    [Area("Admin")]

    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext db;

        public CategoriesController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
            return View(await db.Categories.ToListAsync());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); 
            }
            return View(category);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var Category = await db.Categories.FindAsync(id);

            if(Category == null)
            {
                return NotFound();
            }

            return View(Category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Update(category);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);

        }



        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Category = await db.Categories.FindAsync(id);

            if (Category == null)
            {
                return NotFound();
            }

            return View(Category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category category)
        {
            db.Categories.Remove(category);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Category = await db.Categories.FindAsync(id);

            if (Category == null)
            {
                return NotFound();
            }

            return View(Category);
        }



    }
}
