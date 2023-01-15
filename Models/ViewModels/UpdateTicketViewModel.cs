using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TicketSystem.Models.ViewModels
{
    public class UpdateTicketViewModel
    {
        public Ticket2 Ticket1 { get; set; }

        public IEnumerable<Status> StatusList { get; set; }
        public IEnumerable<Category> CategoriesList { get; set; }
        public string ReplyUser { get; set; }
        public IEnumerable<Reply> RepliesList { get; set; }
    }



    public class Ticket2
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = ("Customer ID"))]
        public int CustomerId { get; set; }

        [Display(Name = ("Customer Name"))]
        public string CustomerName { get; set; }


        [Display(Name = ("Status"))]
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }

}
