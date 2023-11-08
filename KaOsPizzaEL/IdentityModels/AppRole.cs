using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaOsPizzaEL.IdentityModels
{
    public class AppRole : IdentityRole
    {
        [StringLength(500)]
        public string? Description { get; set; }
        public DateTime InsertedDate { get; set; }
        public bool IsDeleted { get; set; }

    }
}
