using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
    public class Reply
    {
        [Key]
        public int Id { get; set; }

        [Display(Name=("Ticket"))]
        public int TicketId { get; set; }
        [ForeignKey("TicketId")]
        public virtual Ticket Ticket { get; set; }

        [Display(Name = ("Replay"))]
        public string ReplyDetails { get; set; }

        [Display(Name=("Username"))]
        public string UsernameReply { get; set; }
        [Display(Name=("Date"))]
        public string ReplyDate { get; set; }
        
    }
}
