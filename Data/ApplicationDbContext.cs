using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TicketSystem.Models;

namespace TicketSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet <Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


    }
}
