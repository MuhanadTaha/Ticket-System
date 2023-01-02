using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name=("Status"))]
        public string Name { get; set; }
    }
}
