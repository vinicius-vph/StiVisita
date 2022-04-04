using StiVisita.Interfaces;
using System.Collections;
using StiVisita.Data;

namespace StiVisita.Repositories
{
    public class PatientRepository : IRepository
    {
        private DatabaseManager _databaseManager;

        public PatientRepository()
        {
            _databaseManager = new DatabaseManager();
        }
        public List<Hashtable> Get()
        {
            var command = "SELECT * FROM patient";

            var TableData = _databaseManager.ExecuteCallBackQuery(command);

            return TableData;
        }
        public Hashtable Get(int? id)
        {
            var command = $"SELECT * FROM patient WHERE id = ( {id} )";

            var TableDataRow = _databaseManager.ExecuteCallBackQuery(command);

            return TableDataRow[0];
        }
        public void Create(Hashtable patient)
        {
            var name = patient["Name"];

            var command = $"INSERT INTO patient ( Name ) VALUES ( '{name}' )";


            _databaseManager.ExecuteVoidQuery(command);
        }
        public void Update(Hashtable patient)
        {
            var name = patient["Name"];
            var id = patient["Id"];

            var command = $"UPDATE patient SET name = ( '{name}' ) WHERE id = ( {id} )";

            _databaseManager.ExecuteVoidQuery(command);
        }
        public void Destroy(int? id)
        {
            var command = $"DELETE FROM patient WHERE id = ( '{id}' )";

            _databaseManager.ExecuteVoidQuery(command);
        }
    }
}
