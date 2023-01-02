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
    public class StatusController : Controller
    {
        private readonly ApplicationDbContext db;

        public StatusController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public async Task <IActionResult> Index()
        {
            return View(await db.Statuses.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Status status)
        {
            if (ModelState.IsValid)
            {
                db.Statuses.Add(status);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Status = await db.Statuses.FindAsync(id);

            if (Status == null)
            {
                return NotFound();
            }

            return View(Status);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Status status)
        {
            if (ModelState.IsValid)
            {
                db.Statuses.Update(status);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(status);

        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Status = await db.Statuses.FindAsync(id);

            if (Status == null)
            {
                return NotFound();
            }

            return View(Status);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Status status)
        {
            db.Statuses.Remove(status);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Status = await db.Statuses.FindAsync(id);

            if (Status == null)
            {
                return NotFound();
            }

            return View(Status);
        }

    }
}
