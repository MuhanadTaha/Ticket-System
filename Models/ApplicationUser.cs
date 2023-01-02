using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
    public class ApplicationUser:IdentityUser //هذا الكلاس المسؤول عن ال ايه اس بي رول بالداتا بيس
    {
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }


    }
}
