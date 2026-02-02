using PetProject.Application.DTOs;
using PetProject.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetProject.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginDto loginDto);
        Task<bool> RegisterAsync(RegisterDto registerDto);
        Task LogoutAsync();
    }

    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetMyAppointmentsAsync(string userId);
        Task<bool> CreateAppointmentAsync(BookingCreateDto bookingDto, string userId);
    }

    public interface IShopService
    {
        Task<IEnumerable<ProductListDto>> GetProductsAsync();
        Task<IEnumerable<ServiceListDto>> GetServicesAsync();
        Task<ProductListDto?> GetProductByIdAsync(int id);
    }
}
