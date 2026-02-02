using PetProject.Domain.Enums;
using System;

namespace PetProject.Domain.Entities
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public PetSpecies Species { get; set; }
        public string Breed { get; set; } = string.Empty;
        public int Age { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        
        public string OwnerId { get; set; } = string.Empty;
        public AppUser Owner { get; set; } = null!;
    }
}
