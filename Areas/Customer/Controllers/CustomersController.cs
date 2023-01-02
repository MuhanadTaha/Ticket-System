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

namespace TicketSystem.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext db;

        //[BindProperty] //لما اعمل سابميت هاي رح تستقبل الداتا اللي رح تيجي من الفورم
        //public TicketViewModel TicketVM { get; set; }

        public CustomersController(ApplicationDbContext db)
        {
            this.db = db;
            //TicketVM = new TicketViewModel() // بس بعمللها إنيشيالايز، يعني بعمل سيت للموديل اللي جواها
            //{
            //    Ticket = new Ticket(), // بس إينيشيال بالدبداية
            //    CategoriesList = db.Categories.ToList(), // عشان بالبداية لما أفوت على الصفحة تكون الداتا معبية بالدروب داون ليست
            //    StatusList = db.Statuses.ToList()
            //};
        }

        public IActionResult Index()
        {


            // return View(TicketVM);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        //[HttpPost]
        //[ActionName("Create")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreatePost(TicketViewModel model) //ما بقد اسميها كريت لإني ما رح أمرر فيها داتا بالتالي سميتها كريت بوست
        //{

        
            
        //    if (ModelState.IsValid)
        //    {


        //        db.Tickets.Add(model.Ticket);
        //        await db.SaveChangesAsync();

             
        //        Reply r = new Reply
        //        {
        //            ReplyDate = Convert.ToString(DateTime.Now),
        //            UsernameReply = "muhanad.taha",
        //            ReplyDetails = model.ReplyUser,
        //            TicketId = model.Ticket.Id,

        //        };
        //        db.Replies.Add(r);
        //        await db.SaveChangesAsync();




        //        return RedirectToAction(nameof(Index));
        //    }

        //    TicketViewModel modelVM = new TicketViewModel()
        //    {
        //        CategoriesList = await db.Categories.ToListAsync(),
        //        StatusList = await db.Statuses.ToListAsync()
        //    };

        //    return View(modelVM);

        //}
    }
}
