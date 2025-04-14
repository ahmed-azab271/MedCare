using AutoMapper;
using BLL.Interfaces;
using DAL.Models;
using Doctor_sAppointment.Helpers;
using Doctor_sAppointment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Numerics;

namespace Doctor_sAppointment.Controllers
{
    [AllowAnonymous]
    public class DoctorController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DoctorController( IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Doctor> doctors = await unitOfWork.DoctorRepo.GetDocsAllIncludedAsync();
            IEnumerable<DoctorVM> doctorVMs = mapper.Map<IEnumerable<Doctor>, IEnumerable<DoctorVM>>(doctors);
            return View(doctorVMs);
        }
        [Authorize (Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorVM doctorVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    doctorVM.ImageName = DocumentaSettings.UploadFile(doctorVM.Image, "Image");
                    Doctor doctor = mapper.Map<Doctor>(doctorVM);
                    await unitOfWork.DoctorRepo.Add(doctor);
                    await unitOfWork.Compelete();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return View(doctorVM);
        }
        [Authorize(Roles = "Admin , Doctors")]
        public async Task<IActionResult> Edit([FromRoute] int id, string ViewName = "Edit")
        {
            if (id == null)
                return BadRequest();
            Doctor doctor = await unitOfWork.DoctorRepo.GetByIdAsync(id);
            if (doctor == null)
                return NotFound();
            DoctorVM doctorVM = mapper.Map<Doctor, DoctorVM>(doctor);
            return View(ViewName, doctorVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorVM doctorVM, [FromRoute] int id)
        {
            try
            {
                if (id != doctorVM.Id)
                    return BadRequest();
                if (ModelState.IsValid)
                {
                    if (doctorVM.ImageName is not null)
                        DocumentaSettings.DeleteFile(doctorVM.ImageName, "Image");
                    doctorVM.ImageName = DocumentaSettings.UploadFile(doctorVM.Image, "Image");
                    Doctor doctor = mapper.Map<DoctorVM, Doctor>(doctorVM);
                    unitOfWork.DoctorRepo.Update(doctor);
                    await unitOfWork.Compelete();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message);}
            return View(doctorVM);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute]int id) => await Edit(id, "Delete");
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DoctorVM doctorVM, [FromRoute] int id)
        {
            try
            {
                if (id != doctorVM.Id)
                    return BadRequest();
                Doctor doctor = mapper.Map<DoctorVM, Doctor>(doctorVM);
                unitOfWork.DoctorRepo.Delete(doctor);
                int AffectedRows = await unitOfWork.Compelete();
                if (AffectedRows > 0 && doctor.ImageName is not null)
                    DocumentaSettings.DeleteFile(doctor.ImageName, "Image");
                return RedirectToAction("Index");
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return View(doctorVM);
        }
        [Authorize(Roles = "Admin , Doctors")]
        public async Task<IActionResult> Details([FromRoute] int id) => await Edit(id, "Details");
        [HttpGet]
        public async Task<IActionResult> SearchFor(string Specialty , string City = "Cairo")
        {
            var Doctor = await unitOfWork.DoctorRepo.GetDocsWithSchAsync();
            var SearchedDocs = Doctor.Where(S=>S.Specialty == Specialty 
                                            && S.City == City );
            if (!SearchedDocs.Any())
                return RedirectToAction("Index");
            var Mapped = mapper.Map< IEnumerable<Doctor> , IEnumerable<DoctorVM> >(SearchedDocs);
            return View(Mapped);
        }
    }
}
