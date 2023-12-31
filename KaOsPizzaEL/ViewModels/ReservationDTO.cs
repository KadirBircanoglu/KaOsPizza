﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KaOsPizzaEL.IdentityModels;
using KaOsPizzaEL.Entities;

namespace KaOsPizzaEL.ViewModels
{
    public class ReservationDTO
    {
        public long Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string UserId { get; set; } //FK
        public bool? Confirmation { get; set; }
        public DateTime DateTime { get; set; }
        public long ReservationSystemId { get; set; }
        public int NumberofPeople { get; set; }
        public AppUser? AppUser { get; set; }
        public ReservationSystem? ReservationSystem { get; set; }
    }
}
