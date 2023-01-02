using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystem.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        [Display(Name =("Customer ID"))]
        public int CustomerId { get; set; }

        [Display(Name=("Customer Name"))]
        public string CustomerName { get; set; }
        
        [Required]

        [Display(Name = ("Phone Number"))]
        public string PhoneNumber { get; set; }
        public string Description { get; set; }


        [Display(Name=("Status"))]
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }




    }
}
