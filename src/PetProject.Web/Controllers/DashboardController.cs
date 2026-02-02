using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetProject.Application.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetProject.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public DashboardController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                return View("Admin");
            }
            else if (User.IsInRole("Vet"))
            {
                return View("Vet");
            }
            else if (User.IsInRole("Staff"))
            {
                return View("Staff");
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var appointments = await _appointmentService.GetMyAppointmentsAsync(userId!);
                return View("Customer", appointments);
            }
        }
    }
}
