using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using Clinic.BLL.Interfaces;
using Clinic.BLL.Dto;
using Clinic.WebUI.Models;
using Microsoft.AspNetCore.Authorization;

namespace Clinic.WebUI.Controllers
{
    [Authorize(Roles = Roles.User)]
    public class PatientsController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(IPatientService patientService, IMapper mapper, ILogger<PatientsController> logger)
        {
            _patientService = patientService;
            _mapper = mapper;
            _logger = logger;
        }

        public ActionResult Index()
        {
            var patients = _patientService.GetAll();
            return View(_mapper.Map<List<PatientViewModel>>(patients));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(PatientViewModel patient)
        {
            try
            {
                await _patientService.AddAsync(_mapper.Map<Patient>(patient));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create patient flow ended with error");
                return RedirectToAction(nameof(Create));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Edit(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            return View(_mapper.Map<PatientViewModel>(patient));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(PatientViewModel patient)
        {
            try
            {
                await _patientService.UpdateAsync(_mapper.Map<Patient>(patient));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Update patient flow ended with error");
                return RedirectToAction(nameof(Edit));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            return View(_mapper.Map<PatientViewModel>(patient));
        }

        [HttpPost]
        public async Task<ActionResult> DeletePatient(int id)
        {
            try
            {
                await _patientService.DeleteAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Delete patient flow ended with error");
                return RedirectToAction(nameof(Edit));
            }

            return RedirectToAction(nameof(Delete));
        }
    }
}