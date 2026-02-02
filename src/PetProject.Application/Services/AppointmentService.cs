using AutoMapper;
using PetProject.Application.DTOs;
using PetProject.Application.Interfaces;
using PetProject.Domain.Entities;
using PetProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetProject.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateAppointmentAsync(BookingCreateDto bookingDto, string userId)
        {
            var appointment = _mapper.Map<Appointment>(bookingDto);
            appointment.PetId = bookingDto.PetId; // Ensure mapping
            // In a real app, validate Pet belongs to User

            await _unitOfWork.Repository<Appointment>().AddAsync(appointment);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<Appointment>> GetMyAppointmentsAsync(string userId)
        {
            // Include Pet navigation property to avoid N+1 queries
            // EF Core will translate this to a single SQL query with JOIN
            return await _unitOfWork.Repository<Appointment>().FindAsync(
                a => a.Pet.OwnerId == userId, 
                asNoTracking: true,
                "Pet", "Service");
        }
    }
}
