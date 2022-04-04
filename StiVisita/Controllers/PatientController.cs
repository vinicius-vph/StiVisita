using Microsoft.AspNetCore.Mvc;
using StiVisita.Services;
using StiVisita.Models;

namespace StiVisita.Controllers
{
    public class PatientController : Controller
    {
        //implementar metodos assincronos
        private readonly PatientService _patientService = new PatientService();

        public IActionResult Index()
        {
            var patients = _patientService.FindAll();
         
            return View(patients);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var patient = PatientExists(id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _patientService.Create(patient);

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = PatientExists(id);

            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _patientService.Update(patient);

                return RedirectToAction(nameof(Index));
            }

            return View(patient);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = PatientExists(id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _patientService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
        private Patient? PatientExists(int? id)
        {
            var patient = _patientService.FindById(id);
            if (patient.Id == 0)
            {
                return null;
            }
            return patient;
        }
    }
}
