using Microsoft.AspNetCore.Mvc;
using StiVisita.Services;
using StiVisita.Models;

namespace StiVisita.Controllers
{
    public class AdmissionController : Controller
    {
        //implementar metodos assincronos
        private readonly AdmissionService _admissionService = new AdmissionService();

        public ActionResult Index()
        {
            var admissions = _admissionService.FindAll();

            return View(admissions);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admission = AdmissionExists(id);

            if (admission == null)
            {
                return NotFound();
            }

            return View(admission);
        }
        public IActionResult Create()
        {
            ViewBag.ListOfPatients = _admissionService.FechPatients();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PatientId,EntryDate,Observation")] Admission admission)
        {
            if (ModelState.IsValid)
            {
                _admissionService.Create(admission);

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

            var admission = AdmissionExists(id);

            if (admission == null)
            {
                return NotFound();
            }
            return View(admission);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,EntryDate,Observation")] Admission admission)
        {
            if (id != admission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _admissionService.Update(admission);

                return RedirectToAction(nameof(Index));
            }

            return View(admission);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admission = AdmissionExists(id);

            if (admission == null)
            {
                return NotFound();
            }

            return View(admission);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _admissionService.Delete(id);

            return RedirectToAction(nameof(Index));
        }
        private Admission? AdmissionExists(int? id)
        {
            var admission = _admissionService.FindById(id);
            if (admission.Id == 0)
            {
                return null;
            }

            return admission;
        }
    }
}
