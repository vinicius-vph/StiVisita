using Microsoft.AspNetCore.Mvc;
using StiVisita.Services;
using StiVisita.Models;

namespace StiVisita.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdmissionApiController : ControllerBase
    {
        //implementar metodos assincronos
        private readonly AdmissionService _admissionService = new AdmissionService();
        
        [HttpGet]
        public Task<IEnumerable<Admission>> GetAdmissions()
        {
            var admissions = _admissionService.FindAll();
            return (Task<IEnumerable<Admission>>)admissions.AsEnumerable();
        }
        [HttpGet("{id}")]
        public ActionResult GetAdmissions(int? id)
        {
            var admission = _admissionService.FindById(id);
            return Ok(admission);
        }
        [HttpPost]
        public ActionResult Create([Bind("PatientId,EntryDate,Observation")] Admission admission)
        {
            _admissionService.Create(admission);
            return Ok();
        }
        [HttpPut]
        public ActionResult Edit([Bind("Id,EntryDate,Observation")] Admission admission)
        {
            _admissionService.Update(admission);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            _admissionService.Delete(id);
            return NoContent();
        }

    }
}
