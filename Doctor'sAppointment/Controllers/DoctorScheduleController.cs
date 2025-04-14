using AutoMapper;
using BLL.Interfaces;
using BLL.Repos;
using DAL.Models;
using Doctor_sAppointment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Doctor_sAppointment.Controllers
{
    [Authorize]
    public class DoctorScheduleController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<AccountUser> userManager;

        public DoctorScheduleController(IUnitOfWork _unitOfWork, IMapper _mapper , UserManager<AccountUser> _userManager)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
            userManager = _userManager;
        }
        [Authorize (Roles = "Admin , Doctors")]
        public async Task<IActionResult> Index(int id)
        {
            Doctor doctor = await unitOfWork.DoctorRepo.GetDocsWithSchAsyncById(id);
            if (doctor == null)
                return NotFound("Doctor not found");
            ViewBag.DoctorId = id;
            IEnumerable<DoctorScheduleVM> scheduleVM = doctor.DoctorSchedules.Select(schedule => new DoctorScheduleVM
            {
                Id = schedule.Id,
                DoctorId = doctor.Id,
                Day = schedule.Day,
                StartAt = schedule.StartAt,
            });
            return View(scheduleVM);
        }

        public async Task<IActionResult> Create(int id)
        {
            Doctor doctor = await unitOfWork.DoctorRepo.GetByIdAsync(id);
            if (doctor == null)
                return NotFound("Doctor not found");
            DoctorScheduleVM scheduleVM = new DoctorScheduleVM() { Doctor = doctor,  DoctorId = id };
            return  View(scheduleVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorScheduleVM scheduleVM , int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    scheduleVM.Id = 0;
                    Doctor doctor = await unitOfWork.DoctorRepo.GetDocsAllIncludedAsyncById(id);
                    DoctorSchedule schedule = mapper.Map<DoctorScheduleVM, DoctorSchedule>(scheduleVM);
                    string result = unitOfWork.DoctorScheduleRepo.Compare(schedule);
                    if (string.IsNullOrEmpty(result))
                    {
                        var patientUserId = userManager.GetUserId(User);
                        var patient = unitOfWork.PatientRepo.GetPatientById(patientUserId);
                        schedule.PatientId = patient.Id;
                        await unitOfWork.DoctorScheduleRepo.Add(schedule);
                        doctor.Patients.Add(patient);
                        unitOfWork.DoctorRepo.Update(doctor);
                        await unitOfWork.Compelete();
                        return RedirectToAction("Index", "Doctor");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "The Date is Taken");
                        return View(scheduleVM);
                    }
                }
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return View(scheduleVM);
        }
        [Authorize(Roles = "Admin , Doctors")]
        public async Task<IActionResult> Delete([FromRoute] int id , string ViewName = "Delete")
        {
            if (id == null)
                return BadRequest();
            DoctorSchedule schedule = await unitOfWork.DoctorScheduleRepo.GetByIdAsync(id);
            if (schedule == null)
                return NotFound();
            DoctorScheduleVM scheduleVM = mapper.Map<DoctorSchedule, DoctorScheduleVM>(schedule);
            return View(ViewName , scheduleVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DoctorScheduleVM scheduleVM,[FromRoute] int id)
        {
            try
            {
                if (id != scheduleVM.Id)
                    return BadRequest();
                DoctorSchedule doctorSchedule = mapper.Map<DoctorScheduleVM, DoctorSchedule>(scheduleVM);
                unitOfWork.DoctorScheduleRepo.Delete(doctorSchedule);
                await unitOfWork.Compelete();
                return RedirectToAction("Index" , new { id = doctorSchedule.DoctorId });
            }
            catch (Exception ex){ ModelState.AddModelError(string.Empty, ex.Message); }
            return View(scheduleVM);
        }
        [Authorize]
        public async Task<IActionResult> Edit([FromRoute] int id) => await Delete(id, "Edit");
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorScheduleVM scheduleVM, [FromRoute] int id)
        {
            try
            {
                if (id != scheduleVM.Id)
                    return BadRequest();
                if (ModelState.IsValid)
                {
                    var patientId = userManager.GetUserId(User);
                    scheduleVM.PatientId = patientId;
                    DoctorSchedule doctorSchedule = mapper.Map<DoctorScheduleVM, DoctorSchedule>(scheduleVM);
                    string result = unitOfWork.DoctorScheduleRepo.Compare(doctorSchedule, scheduleVM.Id);
                    if (string.IsNullOrEmpty(result))
                    {
                        unitOfWork.DoctorScheduleRepo.Update(doctorSchedule);
                        await unitOfWork.Compelete();
                        return RedirectToAction("Index", "Patient");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "The Date is Taken");
                        return View(scheduleVM);
                    }
                }
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return View(scheduleVM);
        }
    }
}
