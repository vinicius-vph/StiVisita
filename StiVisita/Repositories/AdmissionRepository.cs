using StiVisita.Interfaces;
using System.Collections;
using StiVisita.Data;

namespace StiVisita.Repositories
{
    public class AdmissionRepository: IRepository
    {
        private DatabaseManager _databaseManager;

        public AdmissionRepository()
        {
            _databaseManager = new DatabaseManager();
        }
        public List<Hashtable> Get()
        {
            var command = "SELECT * FROM admission";

            var TableData = _databaseManager.ExecuteCallBackQuery(command);

            return TableData;
        }
        public Hashtable Get(int? id)
        {
            var command = $"SELECT * FROM admission WHERE id = ( {id} )";

            var TableDataRow = _databaseManager.ExecuteCallBackQuery(command);

            return TableDataRow[0];
        }
        public void Create(Hashtable admission)
        {
            var patientId = admission["PatientId"];
            var entryDate = admission["EntryDate"];
            var observation = admission["Observation"];

            var command = $"INSERT INTO admission ( PatientId, EntryDate, Observation ) VALUES ( '{patientId}', '{entryDate}','{observation}' )";

            _databaseManager.ExecuteVoidQuery(command);
        }
        public void Update(Hashtable admission)
        {
            var id = admission["Id"];
            var entryDate = admission["EntryDate"];
            var observation = admission["Observation"];

            var command = $"UPDATE admission SET EntryDate = ( '{entryDate}' ), Observation  = ( '{observation}' ) WHERE id = ( {id} )";

            _databaseManager.ExecuteVoidQuery(command);
        }
        public void Destroy(int? id)
        {
            var command = $"DELETE FROM admission WHERE id = ( '{id}' )";

            _databaseManager.ExecuteVoidQuery(command);
        }
    }
}
