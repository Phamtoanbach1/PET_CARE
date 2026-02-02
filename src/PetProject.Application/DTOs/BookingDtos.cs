using System;
using System.ComponentModel.DataAnnotations;

namespace PetProject.Application.DTOs
{
    public class BookingCreateDto
    {
        [Required]
        public int PetId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string TimeSlot { get; set; } = string.Empty; // "09:00 AM"

        public string? Notes { get; set; }
    }
}
