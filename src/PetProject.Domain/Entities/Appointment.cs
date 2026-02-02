using PetProject.Domain.Enums;
using System;

namespace PetProject.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeSlot { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
        public string? Notes { get; set; }

        public int PetId { get; set; }
        public Pet Pet { get; set; } = null!;

        public int ServiceId { get; set; }
        public Service Service { get; set; } = null!;

        public string? VetId { get; set; }
        public AppUser? Vet { get; set; }
    }
}
