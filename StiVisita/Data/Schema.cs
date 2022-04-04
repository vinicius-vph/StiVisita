using System.Collections;

namespace StiVisita.Data
{
    class Schema
    {
        private static readonly string CreatePatientTable = "CREATE TABLE IF NOT EXISTS patient (Id integer primary key autoincrement, PatientCode varchar(500), Name text)";

        private static readonly string CreateAdmissionTable = "CREATE TABLE IF NOT EXISTS admission ( Id integer primary key autoincrement, AdmissionCode varchar(500),  EntryDate text, " +
                                                              "Observation varchar(500), PatientId integer, foreign key (PatientId) references patient (Id) ON UPDATE SET DEFAULT ON DELETE CASCADE)";

        private static readonly string CreateTriggerPatientUuidGenerator = "CREATE TRIGGER IF NOT EXISTS AutoGeneratePatientGUID AFTER INSERT ON patient FOR EACH ROW WHEN(NEW.PatientCode IS NULL) " +
                                                                           "BEGIN UPDATE patient SET PatientCode = (select hex(randomblob(4)) || '-' || hex(randomblob(2)) || '-' || '4' || substr(hex(randomblob(2)), 2) "+
                                                                           "|| '-' || substr('AB89', 1 + (abs(random()) % 4), 1) || substr(hex(randomblob(2)), 2) || '-' || hex(randomblob(6)) ) WHERE rowid = NEW.rowid; END;";

        private static readonly string CreateTriggerAdmissionUuidGenerator = "CREATE TRIGGER IF NOT EXISTS AutoGenerateAdmissionGUID AFTER INSERT ON admission FOR EACH ROW WHEN(NEW.AdmissionCode IS NULL) " +
                                                                             "BEGIN UPDATE admission SET AdmissionCode = (select hex(randomblob(4)) || '-' || hex(randomblob(2)) || '-' || '4' || substr(hex(randomblob(2)), 2) "+
                                                                             "|| '-' || substr('AB89', 1 + (abs(random()) % 4), 1) || substr(hex(randomblob(2)), 2) || '-' || hex(randomblob(6)) ) WHERE rowid = NEW.rowid; END;";

        public static ArrayList Migrations()
        {
            ArrayList migrationsList = new ArrayList();

            migrationsList.Add(CreatePatientTable);
            migrationsList.Add(CreateAdmissionTable);
            migrationsList.Add(CreateTriggerPatientUuidGenerator);
            migrationsList.Add(CreateTriggerAdmissionUuidGenerator);

            return migrationsList;
        }
    }
}
