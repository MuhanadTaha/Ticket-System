using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models.ViewModels
{
    public class TicketViewModel
    {
        public Ticket Ticket { get; set; }
        public IEnumerable<Status> StatusList { get; set; }
        public IEnumerable<Category> CategoriesList { get; set; }
        public string ReplyUser { get; set; }
        public IEnumerable<Reply> RepliesList { get; set; }
    }

   
}
