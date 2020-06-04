using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using Clinic.BLL.Interfaces;
using Clinic.BLL.Dto;
using Clinic.WebUI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Clinic.WebUI.Controllers
{
    [Authorize(Roles = Roles.User)]
    public class DoctorsController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(IDoctorService doctorService, IPatientService patientService, IMapper mapper, ILogger<DoctorsController> logger)
        {
            _doctorService = doctorService;
            _patientService = patientService;
            _mapper = mapper;
            _logger = logger;
        }

        public ActionResult Index()
        {
            var doctors = _doctorService.GetAll();
            return View(_mapper.Map<List<DoctorViewModel>>(doctors));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(DoctorViewModel doctor)
        {
            try
            {
                await _doctorService.AddAsync(_mapper.Map<Doctor>(doctor));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create doctor flow ended with error");
                return RedirectToAction(nameof(Create));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int id)
        {
            var patient = await _doctorService.GetByIdAsync(id);
            return View(_mapper.Map<DoctorViewModel>(patient));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(DoctorViewModel doctor)
        {
            try
            {
                await _doctorService.UpdateAsync(_mapper.Map<Doctor>(doctor));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Update doctor flow ended with error");
                return RedirectToAction(nameof(Edit));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            return View(_mapper.Map<DoctorViewModel>(doctor));
        }

        [HttpPost]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
            try
            {
                await _doctorService.DeleteAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Delete doctor flow ended with error");
                return RedirectToAction(nameof(Edit));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> GetReceptions(int id)
        {
            var doctor = await _doctorService.GetByIdWithPatients(id);
            return View("Receptions", _mapper.Map<DoctorViewModel>(doctor));
        }

        public ActionResult AddReception(int id)
        {
            var patients = _patientService.GetAll();
            var patientsList = new List<SelectListItem>();
            var reception = new ReceptionViewModel();
            reception.DoctorId = id;

            foreach (var patient in patients)
            {
                patientsList.Add(new SelectListItem
                {
                    Text = patient.Surname,
                    Value = patient.Id.ToString(),
                });
            }

            ViewBag.Items = patientsList;
            return View(reception);
        }

        [HttpPost]
        public async Task<ActionResult> AddReception(ReceptionViewModel reception)
        {
            try
            {
                await _doctorService.AddReception(_mapper.Map<Reception>(reception));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Add reception flow ended with error");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
