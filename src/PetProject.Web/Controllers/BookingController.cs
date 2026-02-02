using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetProject.Application.DTOs;
using PetProject.Application.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PetProject.Web.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public BookingController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookingCreateDto bookingDto)
        {
            if (!ModelState.IsValid) return View("Index", bookingDto);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return RedirectToAction("Login", "Account");

            var result = await _appointmentService.CreateAppointmentAsync(bookingDto, userId);
            if (result)
            {
                return RedirectToAction("Confirmation");
            }

            ModelState.AddModelError(string.Empty, "Failed to book appointment.");
            return View("Index", bookingDto);
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
