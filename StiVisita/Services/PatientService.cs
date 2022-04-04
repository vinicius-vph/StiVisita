using StiVisita.Repositories;
using StiVisita.Interfaces;
using System.Collections;
using StiVisita.Models;

namespace StiVisita.Services
{
    public class PatientService : IService<Patient>
    {
        private PatientRepository _patientRepository;
        
        public PatientService()
        {
            _patientRepository = new PatientRepository();
        }
        public List<Patient> FindAll()
        {
            List<Patient> _patientList = new List<Patient>();

            var patients = _patientRepository.Get();

            foreach (var patient in patients)
            {
                var _patient = HashToObject(patient);

                _patientList.Add(_patient);
            }

            return _patientList;
        }
        public Patient FindById(int? id)
        {
            var findedPatient = _patientRepository.Get(id);
            var _patient = HashToObject(findedPatient);

            return _patient;
        }
        public void Create(Patient patient)
        {
            var _patient = ObjectToHash(patient);

            _patientRepository.Create(_patient);
        }
        public void Update(Patient patient)
        {
            var _patient = ObjectToHash(patient);

            _patientRepository.Update(_patient);
        }
        public void Delete(int? id)
        {
            _patientRepository.Destroy(id);
        }
        private static Hashtable ObjectToHash(Patient patient)
        {
            Hashtable hash = new Hashtable();
            hash.Add("Id", patient.Id);
            hash.Add("PatientCode", patient.PatientCode);
            hash.Add("Name", patient.Name);
            return hash;
        }
        private static Patient HashToObject(Hashtable patient)
        {
            var _patient = new Patient();

            _patient.Id = Convert.ToInt32(patient["Id"]);
            _patient.PatientCode = patient["PatientCode"].ToString();
            _patient.Name = patient["Name"].ToString();
            return _patient;
        }
    }
}
