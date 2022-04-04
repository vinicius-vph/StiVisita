using System.Collections;
using Microsoft.Data.Sqlite;
using StiVisita.Interfaces;
using StiVisita.Common;

namespace StiVisita.Interfaces
{
    interface IDbContext
    {
        public void ExecuteVoidQuery(string query);
        public List<Hashtable> ExecuteCallBackQuery(string query);
        private void CreateConnection() { }
        private void RunMigrations() { }
    }
}
