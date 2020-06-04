using Clinic.BLL.Dto;
using Clinic.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Clinic.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(IPatientService patientService, ILogger<PatientsController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var patients = _patientService.GetAll();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            return Ok(patient);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]Patient patient)
        {
            try
            {
                await _patientService.AddAsync(patient);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create patient flow ended with error");
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromBody]Patient patient)
        {
            try
            {
                await _patientService.UpdateAsync(patient);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Update patient flow ended with error");
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _patientService.DeleteAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Delete patient flow ended with error");
                return BadRequest();
            }

            return Ok();
        }
    }
}