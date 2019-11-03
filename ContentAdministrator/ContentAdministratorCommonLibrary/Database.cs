#define DBCREATE

using System;
using System.Security.Cryptography;
using System.Text;

using Microsoft.Extensions.Logging;

using ContentAdministratorCommonLibrary.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using eMeL_Common;
using System.Linq;

// Install-Package System.Security.Cryptography.ProtectedData        ---Warning! Only Windows package!

// Install-Package FirebirdSql.Data.FirebirdClient
// Install-Package FirebirdSql.EntityFrameworkCore.Firebird 
// Install-Package firebird.embedded

namespace ContentAdministratorCommonLibrary
{
    public static class Database
    {
        private static  byte[] encryptionAditionalEntropy = { 9, 18, 97, 6, 5 ,251, 56, 89, 1, 154, 253, 0, 12, 154, 88};

        public  const   string createLogsGroupName = "CREATE";

        public static async System.Threading.Tasks.Task CreateDatabaseAsync(string connectionString)
        {
            CADB_Context.Init(connectionString); 

            using (var db = new CADB_Context())
            {
                #if DEBUG && DBCREATE
                db.Database.EnsureDeleted();                    // ./SQLExpress: Nem működik!
                db.Database.EnsureCreated();                    // ./SQLExpress --> "Database 'CONTADM' already exists. Choose a different database name."
                // (localdb)   --> "A database with the same name exists, or specified file cannot be opened, or it is located on UNC share."
                #else               
                db.Database.Migrate();                          // https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/#create-a-migration
                #endif

                #if DEBUG                
                new Thread(() =>
                {
                    var sql = db.Database.GenerateCreateScript();
                    var newext = $"_databasecreate-{DateTime.Now.ToString("yyyyMMdd_hhmmss")}.sql";
                    var filename = ParameterXML.appParameterXMLFileName.Replace(".xml", newext, StringComparison.InvariantCultureIgnoreCase);
                    System.IO.File.WriteAllText(filename, sql);
                }).Start();
                #endif
               
                var logs = await db.logs.Where((log) => log.group == createLogsGroupName).ToListAsync();
                
                if (logs.Count < 1)
                {
                    var log = new Logs(createLogsGroupName, $"Created by {Environment.UserName} at {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}");

                    log.lastModTime = DateTime.Now;
                    log.lastModLogin = 0;

                    db.logs.Add(log);

                    db.SaveChanges();
                }                              
            }
        }

        public const string connectionStringNodeName = "ConnectionString";

        public static async Task SaveConnectionStringAsync(string connectionString)
        {
            byte[] connectionStringBytes = Encoding.UTF8.GetBytes(connectionString);

            var encrypted = ProtectedData.Protect(connectionStringBytes, encryptionAditionalEntropy, DataProtectionScope.CurrentUser);

            ParameterXML.SetValue(connectionStringNodeName, Environment.UserName, encrypted);

            await Task.Delay(1);
        }

        public static string ReadConnectionString()
        {
            byte[] encypted = ParameterXML.GetValueByteArray(connectionStringNodeName, Environment.UserName); 

            if (encypted == null)
            {
                throw new Exception($"The '{Environment.UserName}' user's connection string is invalid!");
            }

            var connectionStringBytes = ProtectedData.Unprotect(encypted, encryptionAditionalEntropy, DataProtectionScope.CurrentUser);

            var connectionString = Encoding.UTF8.GetString(connectionStringBytes);

            return connectionString;
        }
        private static string loginName { get => Environment.UserName; }
    }
}
