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
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(IDoctorService doctorService, ILogger<DoctorsController> logger)
        {
            _doctorService = doctorService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var doctors = _doctorService.GetAll();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            return Ok(doctor);
        }

        [HttpGet("/detailed/{id}")]
        public async Task<ActionResult> GetDetailed(int id)
        {
            var doctor = await _doctorService.GetByIdWithPatients(id);
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]Doctor doctor)
        {
            try
            {
                await _doctorService.AddAsync(doctor);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create doctor flow ended with error");
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("/receptions")]
        public async Task<ActionResult> AddReception([FromBody]Reception reception)
        {
            try
            {
                await _doctorService.AddReception(reception);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create reception flow ended with error");
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromBody]Doctor doctor)
        {
            try
            {
                await _doctorService.UpdateAsync(doctor);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Update doctor flow ended with error");
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _doctorService.DeleteAsync(id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Delete doctor flow ended with error");
                return BadRequest();
            }

            return Ok();
        }
    }
}