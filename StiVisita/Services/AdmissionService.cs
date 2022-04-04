using Microsoft.AspNetCore.Mvc.Rendering;
using StiVisita.Repositories;
using StiVisita.Interfaces;
using System.Collections;
using StiVisita.Models;

namespace StiVisita.Services
{
    public class AdmissionService : IService<Admission>
    {
        private AdmissionRepository _admissionRepository;
        private readonly PatientService _patientService = new PatientService();

        public AdmissionService()
        {
            _admissionRepository = new AdmissionRepository();
        }
        public List<Admission> FindAll()
        {
            List<Admission> _admissionList = new List<Admission>();

            var admissions = _admissionRepository.Get();
            
            foreach (var _admission in admissions)
            {
                var admission = HashToObject(_admission);

                _admissionList.Add(admission);
            }

            return _admissionList;
        }
        public Admission FindById(int? id)
        {
            var findedAdmission = _admissionRepository.Get(id);
            var _admission = HashToObject(findedAdmission);

            return _admission;
        }
        public void Create(Admission admission)
        {
            var _admission = ObjectToHash(admission);

            _admissionRepository.Create(_admission);
        }
        public void Update(Admission admission)
        {
            var _admission = ObjectToHash(admission);

            _admissionRepository.Update(_admission);
        }
        public void Delete(int? id)
        {
            _admissionRepository.Destroy(id);
        }
        public List<SelectListItem> FechPatients()
        {
            var patientsAll = _patientService.FindAll();
          
            var _patientsList = (from patient in patientsAll
                                select new SelectListItem()
                                {
                                    Text = patient.Name.ToString(),
                                    Value = patient.Id.ToString(),

                                }).ToList();

            _patientsList.Insert(0, new SelectListItem()
            {
                Text = "-- Selecione o Doente --",
                Value = String.Empty
            });

            return _patientsList;
        }
        private static Hashtable ObjectToHash(Admission admission)
        {
            Hashtable hash = new Hashtable();
            hash.Add("Id", admission.Id);
            hash.Add("PatientId", admission.PatientId);
            hash.Add("EntryDate", admission.EntryDate);
            hash.Add("Observation", admission.Observation);
            hash.Add("AdmissionCode", admission.AdmissionCode);
            return hash;
        }
        private static Admission HashToObject(Hashtable admission)
        {
            var _admission = new Admission();
            var _patient = FindPatient(Convert.ToInt32(admission["PatientId"]));

            _admission.PatientId = _patient.Id;
            _admission.PatientName = _patient.Name;
            _admission.Id = Convert.ToInt32(admission["Id"]);
            _admission.AdmissionCode = admission["AdmissionCode"].ToString();
            _admission.EntryDate = Convert.ToDateTime(admission["EntryDate"]);
            _admission.Observation = admission["Observation"].ToString();
            return _admission;
        }
        private static Patient FindPatient(int? id)
        {
            var _patientService = new PatientService();
            var _patient = _patientService.FindById(id);

            return _patient;
        }
    }
}
