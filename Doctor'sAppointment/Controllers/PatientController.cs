using AutoMapper;
using BLL.Interfaces;
using DAL.Models;
using Doctor_sAppointment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.CodeDom;
using static DAL.Models.Patient;

namespace Doctor_sAppointment.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AccountUser> userManager;
        private readonly IMapper mapper;

        public PatientController(IUnitOfWork _unitOfWork, UserManager<AccountUser> _userManager, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;
            mapper = _mapper;
        }
        public async Task<IActionResult> Index(string id)
        {
            if (id == null)
            {
                var userId = userManager.GetUserId(User);
                var patient = unitOfWork.PatientRepo.GetPatientById(userId);
                var patientWithScheduales = unitOfWork.PatientRepo.GetPatientAllIncludedById(userId);
                var patientVM = mapper.Map<Patient, PatientVM>(patientWithScheduales);
                return View(patientVM);
            }
            else
            {
                var patient = unitOfWork.PatientRepo.GetPatientById(id);
                var patientWithScheduales = unitOfWork.PatientRepo.GetPatientAllIncludedById(id);
                var patientVM = mapper.Map<Patient, PatientVM>(patientWithScheduales);
                return View(patientVM);
            }
        }
        public IActionResult Symptoms(string id, string ViewName = "Symptoms")
        {
            if (id == null)
                return BadRequest();
            var patient = unitOfWork.PatientRepo.GetPatientById(id);
            if (patient == null)
                return NotFound();
            var patientVM = mapper.Map<Patient, PatientVM>(patient);
            return View(ViewName, patientVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Symptoms(PatientVM patientVM, string id)
        {
            try
            {
                if (id != patientVM.Id)
                    return BadRequest();
                if (ModelState.IsValid)
                {
                    var patient = unitOfWork.PatientRepo.GetPatientById(id);
                    patient.Description = patientVM.Description;
                    patient.Duration = patientVM.Duration;
                    patient.AdditionalNotes = patientVM.AdditionalNotes;
                    patient.Severity = patientVM.Severity;
                    unitOfWork.PatientRepo.Update(patient);
                    await unitOfWork.Compelete();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return View(patientVM);
        }
        public IActionResult EditSymptoms(string id) => Symptoms(id, "EditSymptoms");
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSymptoms(PatientVM patientVM, string id)
            => await Symptoms(patientVM, id);
        [HttpGet]
        public async Task<IActionResult> MedicalRecords(string id) => Symptoms(id, "MedicalRecords");
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MedicalRecords(PatientVM patientVM,string id)
        {
            try
            {
                    if (id != patientVM.Id)
                        return BadRequest();
                    var patient = unitOfWork.PatientRepo.GetPatientById(id);
                    patient.Doctor = patientVM.Doctor;
                    patient.Notes = patientVM.Notes;
                    patient.Treatment = patientVM.Treatment;
                    patient.Diagnosis = patientVM.Diagnosis;
                    unitOfWork.PatientRepo.Update(patient);
                    await unitOfWork.Compelete();
                return RedirectToAction("Index");
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return View(patientVM);
        }
        [Authorize(Roles = "Admin , Patients")]
        public IActionResult EditMedicalRecords(string id) => Symptoms(id, "EditMedicalRecords");
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMedicalRecords(PatientVM  patientVM,string id) 
            =>await  MedicalRecords(patientVM,id);
    }
}

