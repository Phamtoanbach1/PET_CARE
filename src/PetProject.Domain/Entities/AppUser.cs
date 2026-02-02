using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace PetProject.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Pet> Pets { get; set; } = new List<Pet>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
