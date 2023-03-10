using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TicketSystem.Data;
using TicketSystem.Models;
using TicketSystem.Models.ViewModels;

namespace TicketSystem.Controllers
{
    [Area("Agent")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;

        [BindProperty] //لما اعمل سابميت هاي رح تستقبل الداتا اللي رح تيجي من الفورم
        public TicketViewModel TicketVM { get; set; }

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
            TicketVM = new TicketViewModel() // بس بعمللها إنيشيالايز، يعني بعمل سيت للموديل اللي جواها
            {
                Ticket = new Ticket(), // بس إينيشيال بالدبداية
                CategoriesList = db.Categories.ToList(), // عشان بالبداية لما أفوت على الصفحة تكون الداتا معبية بالدروب داون ليست
                StatusList = db.Statuses.ToList()
            };
        }

        public async Task<IActionResult> Index()
        {
            var Tickets = await db.Tickets.Include(m => m.Category).Include(m => m.Status).ToListAsync();
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
        public async Task<IActionResult> EditPost() //ما بقد اسميها كريت لإني ما رح أمرر فيها داتا بالتالي سميتها كريت بوست
        {
            var claim = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Name);
            var username = claim.Value;


            if (ModelState.IsValid)
            {
                db.Tickets.Update(TicketVM.Ticket);
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
