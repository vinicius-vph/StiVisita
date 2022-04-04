using Microsoft.AspNetCore.Mvc;
using StiVisita.Services;
using StiVisita.Models;

namespace StiVisita.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PatientApiController: ControllerBase
    {
        private readonly PatientService _patientService = new PatientService();

        [HttpGet]
        public Task<IEnumerable<Patient>> GetPatients()
        {
            var patients = _patientService.FindAll();
            return (Task<IEnumerable<Patient>>)patients.AsEnumerable();
        }
        [HttpGet("{id}")]
        public ActionResult GetPatients(int? id)
        {
            var patient = _patientService.FindById(id);
            return Ok(patient);
        }

        [HttpPost]
        public ActionResult Create([Bind("Name")] Patient patient)
        {
            _patientService.Create(patient);
            return Ok();
        }
        [HttpPut]
        public ActionResult Edit(int id, [Bind("Id,Name")] Patient patient)
        {
            _patientService.Update(patient);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _patientService.Delete(id);
            return NoContent();
        }
    }
}
