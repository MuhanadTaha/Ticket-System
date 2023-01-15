using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketSystem.Data;
using TicketSystem.Models;
using TicketSystem.Models.ViewModels;
using TicketSystem.Utility;

namespace TicketSystem.Areas.Admin.Controllers
{
    [Authorize(Roles =SD.ManagerUser)]
    [Area("Admin")]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext db;


        [BindProperty] //لما اعمل سابميت هاي رح تستقبل الداتا اللي رح تيجي من الفورم
        public TicketViewModel TicketVM { get; set; }

        public TicketsController(ApplicationDbContext db)
        {
            this.db = db;

            TicketVM = new TicketViewModel() // بس بعمللها إنيشيالايز، يعني بعمل سيت للموديل اللي جواها
            {
                Ticket = new Ticket(), // بس إينيشيال بالدبداية
                CategoriesList = db.Categories.ToList(), // عشان بالبداية لما أفوت على الصفحة تكون الداتا معبية بالدروب داون ليست
                StatusList = db.Statuses.ToList(),


            };

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Tickets = await db.Tickets.Include(m => m.Category).Include(m=>m.Status).ToListAsync();

            return View(Tickets);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(TicketVM); 
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(TicketViewModel model) //ما بقد اسميها كريت لإني ما رح أمرر فيها داتا بالتالي سميتها كريت بوست
        {
            var claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Name);
            var username = claim.Value;


            if (ModelState.IsValid)
            {


                db.Tickets.Add(model.Ticket);
                await db.SaveChangesAsync();


                Reply r = new Reply
                {
                    ReplyDate = Convert.ToString(DateTime.Now),
                    UsernameReply = username,
                    ReplyDetails = model.ReplyUser,
                    TicketId = model.Ticket.Id,

                };
                db.Replies.Add(r);
                await db.SaveChangesAsync();




                return RedirectToAction(nameof(Index));
            }

            TicketViewModel modelVM = new TicketViewModel()
            {
                CategoriesList = await db.Categories.ToListAsync(),
                StatusList = await db.Statuses.ToListAsync(),
            };

            return View(modelVM);

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var ticket = db.Tickets.Include(m => m.Category).Include(m => m.Status).SingleOrDefault(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            var reply = db.Replies.Where(m => m.TicketId == id).ToList();
            
            TicketVM.Ticket = ticket;
            TicketVM.RepliesList = reply;

            return View(TicketVM);
        }


        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(UpdateTicketViewModel updateTicketVM)
        {
            var claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Name);
            var username = claim.Value;




            if (ModelState.IsValid)
            {
                var doesexistTicket = await db.Tickets.FindAsync(TicketVM.Ticket.Id);

                doesexistTicket.Id = TicketVM.Ticket.Id;
                doesexistTicket.CustomerId = TicketVM.Ticket.CustomerId;
               // doesexistTicket.CustomerName = TicketVM.Ticket.CustomerName;
                doesexistTicket.StatusId = TicketVM.Ticket.StatusId;
                doesexistTicket.CategoryId = TicketVM.Ticket.CategoryId;

                db.Tickets.Update(doesexistTicket);
                await db.SaveChangesAsync();

                Reply reply = new Reply
                {
                    ReplyDate = Convert.ToString(DateTime.Now),
                    UsernameReply = username,
                    ReplyDetails = TicketVM.ReplyUser,
                    TicketId = TicketVM.Ticket.Id,

                };

                if (!string.IsNullOrEmpty(reply.ReplyDetails))
                {
                    db.Replies.Add(reply);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Edit));

                }

                return RedirectToAction(nameof(Index));


            }

            TicketViewModel modelVM = new TicketViewModel()
            {
                CategoriesList = await db.Categories.ToListAsync(),
                StatusList = await db.Statuses.ToListAsync(),
                

            };

            return View(modelVM);

        }



        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ticket = db.Tickets.Include(m => m.Category).Include(m => m.Status).SingleOrDefault(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }


            TicketVM.Ticket = ticket;
            return View(TicketVM);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost()
        {
            

            db.Tickets.Remove(TicketVM.Ticket);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Details(int? id)

        {
            if (id == null)
            {
                return NotFound();
            }
            var ticket = db.Tickets.Include(m => m.Category).Include(m => m.Status).SingleOrDefault(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }


            TicketVM.Ticket = ticket; // الداتا كلها رح تتخزن بالموديل منيو ايتيم
            TicketVM.CategoriesList = await db.Categories.ToListAsync();
            return View(TicketVM);
        }

    }
}
