using System.Diagnostics;
using AutoMapper;
using BLL.Interfaces;
using BLL.Repos;
using DAL.Models;
using Doctor_sAppointment.Models;
using Doctor_sAppointment.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Doctor_sAppointment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public HomeController(ILogger<HomeController> logger , IUnitOfWork _unitOfWork , IMapper _mapper)
        {
            _logger = logger;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }

        public async Task<IActionResult> Index()
        {
            var Doctors =   await unitOfWork.DoctorRepo.GetAllAsync();
            IEnumerable<DoctorVM> doctorVMs = mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorVM>>(Doctors);
            return View(doctorVMs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
