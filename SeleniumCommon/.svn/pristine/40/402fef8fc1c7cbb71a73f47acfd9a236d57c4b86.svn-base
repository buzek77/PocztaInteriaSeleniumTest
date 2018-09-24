using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Vulcan.Common2015.SeleniumLib.Helpers
{
    public class DBHelper
    {
        public static bool PierwszyTest = true;
        ///<summary>
        /// Zmienna oznaczająca, czy pierwszy test został wykonany
        /// </summary>
        /// <summary>
        /// Uruchamia proces przywracania bazy danych z pliku .bak
        /// </summary>
        /// <param name="snapshot">opcjonalny parametr czy przywracac baze danych ze snapshota (jesli tak to true)</param>
        /// <param name="everyClass">opcjonalny parametr, czy przywracać bazę przed każdą klasą testową. Domyślnie - nie.</param>
        public static void Setup(bool snapshot = false, bool everyClass = false)
        {
            if (everyClass || PierwszyTest)
            //Sprawdza czy baza ma być przywracana przed każdą klasą, lub test jest pierwszy - jeśli nie, przywracanie jest pomijane.
            {
                {
                    RestoreDB(ConfigurationManager.AppSettings["serverAdress"],
                              ConfigurationManager.AppSettings["dbName"],
                              ConfigurationManager.AppSettings["fileAdress"],
                              ConfigurationManager.AppSettings["logicalFileNameInBackup"], snapshot,everyClass);
                    //, ConfigurationManager.AppSettings["logicalFileNameInBackupLog"]);
                }
            }
        }

        /// <summary>
        /// Przywraca baze danych
        /// </summary>
        /// <param name="serverAdress">adres serwera na ktorym ma byc przywrocona baza</param>
        /// <param name="dbName">nazwa przywracanej bazy danych</param>
        /// <param name="fileAdress">sciezka do backupa</param>
        /// <param name="logicalFileName">nazwa pliku log w przywracanej bazie</param>
        /// <param name="snapshot">opcjonalny parametr czy przywracac baze danych ze snapshota</param>
        /// <param name="everyClass">opcjonalny parametr, czy przywracać bazę przed każdą klasą</param>
        public static void RestoreDB(string serverAdress, string dbName, string fileAdress, string logicalFileName, bool snapshot = false, bool everyClass = false) //, string logicalFileNameLog)
        {
            if (everyClass || PierwszyTest)
            //Sprawdza czy baza ma być przywracana przed każdą klasą, lub test jest pierwszy - jeśli nie, przywracanie jest pomijane.
            {
                string logicalFileNameLog = ConfigurationManager.AppSettings["logicalFileNameInBackupLog"];
                if (File.Exists(fileAdress))
                {
                    string connectionString = String.Format("Data Source={0};Initial Catalog= master;Integrated Security=SSPI;Connection Timeout=120;", serverAdress);
                    string sqlRestoreCommand = snapshot ?
                        String.Format(@"RESTORE DATABASE {0} FROM DATABASE_SNAPSHOT = '{0}_Snapshot';", dbName)
                        :
                        String.Format(
                                    @"declare @sciezka nvarchar(300)
                                declare @mdf nvarchar(300)
                                declare @ldf nvarchar(300)

                                SELECT @sciezka = SUBSTRING(physical_name, 1,
                                CHARINDEX(N'master.mdf',
                                LOWER(physical_name)) - 1) 
                                FROM master.sys.master_files
                                WHERE database_id = 1 AND FILE_ID = 1
                                set @mdf = @sciezka+'{0}.mdf'
                                set @ldf = @sciezka+'{0}_1.ldf'
                                RESTORE DATABASE [{0}] FROM  DISK = N'{1}' WITH  FILE = 1,  MOVE N'{2}' TO @mdf,  MOVE N'{3}' TO @ldf, REPLACE,  STATS = 10",

                                    dbName, fileAdress, logicalFileName, logicalFileNameLog);

                    string sqlDrobDb = String.Format(@"if exists ( select name from master.dbo.sysdatabases where name= '{0}' ) begin DROP DATABASE {0} end", dbName);
                    string sqlRemoveSnapshotCommand = String.Format(@"if exists ( select name from master.dbo.sysdatabases where name= '{0}_Snapshot' ) begin DROP DATABASE {0}_Snapshot end", dbName);
                    string sqlSingleUserCommand = String.Format(@"if exists ( select name from master.dbo.sysdatabases where name = '{0}') begin  alter database {0} SET SINGLE_USER WITH ROLLBACK IMMEDIATE end", dbName);
                    string sqlMultiUserCommand = String.Format(@"if exists ( select name from master.dbo.sysdatabases where name = '{0}') begin alter database {0} SET multi_user end ", dbName);


                    SqlConnection conn = new SqlConnection(connectionString);
                    conn.Open();

                    SqlCommand comm;
                    try
                    {
                        // ustawienie single usera na bazie danych
                        try
                        {
                            Console.WriteLine("Ustawienie trybu single user");
                            comm = new SqlCommand(sqlSingleUserCommand, conn);
                            comm.CommandTimeout = 120;
                            comm.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        if (!snapshot)
                        {
                            Console.WriteLine("Usuniecie snapshotow");
                            comm = new SqlCommand(sqlRemoveSnapshotCommand, conn);
                            comm.ExecuteNonQuery();
                            Console.WriteLine("Usunieto snapshot");

                            Console.WriteLine("Usuwanie bazy danych");
                            comm = new SqlCommand(sqlDrobDb, conn);
                            comm.CommandTimeout = 120;
                            comm.ExecuteNonQuery();
                        }

                        // restore bazy danych ;
                        Console.WriteLine("Przywracanie bazy danych snapshot={0}", snapshot);
                        comm = new SqlCommand(sqlRestoreCommand, conn);
                        comm.CommandTimeout = 120;
                        comm.ExecuteNonQuery();
                        Console.WriteLine("Przywrócono bazę danych : " + serverAdress + ":" + dbName);
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine(e.ToString());
                        throw e;
                    }
                    finally
                    {
                        // ustawienie multi usera 
                        comm = new SqlCommand(sqlMultiUserCommand, conn);
                        comm.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                else
                {
                    throw new FileNotFoundException(fileAdress);
                }
                PierwszyTest = false;
            }
        }

        /// <summary>
        /// Dodaje uzytkownika do bazy
        /// </summary>
        /// <param name="name">nazwa uzytkownika</param>
        /// <param name="password">haslo uzytkownika</param>
        public static void AddNewDBUser(string name, string password)
        {
            SqlConnection.ClearAllPools();
            string connectionString =
                   String.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI;Connection Timeout=120",
                                 ConfigurationManager.AppSettings["serverAdress"],
                                 ConfigurationManager.AppSettings["dbName"]);
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {


                string sqlCommand =
                    String.Format(
                        @"
                        IF NOT EXISTS 
                            (SELECT name  
                            FROM master.sys.server_principals
                            WHERE name = '{0}')
                        BEGIN
                            USE [{2}] 
                            CREATE LOGIN {0}
                            WITH PASSWORD = '{1}'
                        END
                            USE [{2}] 
                            CREATE USER {0} FOR LOGIN {0}
                            USE [{2}]
                            EXEC sp_addrolemember N'db_datareader', N'{0}'
                            EXEC sp_addrolemember N'db_datawriter', N'{0}'
                            EXEC sp_addrolemember N'db_owner', N'{0}'
                 ", name, password, ConfigurationManager.AppSettings["dbName"]);

                conn.Open();
                SqlCommand comm = new SqlCommand(sqlCommand, conn);
                comm.CommandTimeout = 120;
                comm.ExecuteNonQuery();


                Console.WriteLine("Stworzono użytkownika " + name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }

        /// <summary>
        /// dodaje prawa read i write do bazy dla danego uzytkownika
        /// </summary>
        /// <param name="name">nazwa uzytkownika</param>
        /// <param name="password">haslo uzytkownika</param>
        public static void AddNewDBUserDataReaderDataWriter(string name, string password)
        {
            SqlConnection.ClearAllPools();
            string connectionString =
                   String.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI;Connection Timeout=60",
                                 ConfigurationManager.AppSettings["serverAdress"],
                                 ConfigurationManager.AppSettings["dbName"]);
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {


                string sqlCommand =
                    String.Format(
                        @"
                        IF NOT EXISTS 
                            (SELECT name  
                            FROM master.sys.server_principals
                            WHERE name = '{0}')
                        BEGIN
                            USE [{2}] 
                            CREATE LOGIN {0}
                            WITH PASSWORD = '{1}'
                        END
                            USE [{2}] 
                            CREATE USER {0} FOR LOGIN {0}
                            USE [{2}]
                            EXEC sp_addrolemember N'db_datareader', N'{0}'
                            EXEC sp_addrolemember N'db_datawriter', N'{0}'
                            
                 ", name, password, ConfigurationManager.AppSettings["dbName"]);

                conn.Open();
                SqlCommand comm = new SqlCommand(sqlCommand, conn);
                comm.CommandTimeout = 120;
                comm.ExecuteNonQuery();


                Console.WriteLine("Stworzono użytkownika " + name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }


        }

        /// <summary>
        /// nadaje uprawnienia owner dla uzytkownika bazy
        /// </summary>
        private static void SetOwnerRights()
        {
            try
            {
                string connectionString =
                    String.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI;Connection Timeout=60",
                                  ConfigurationManager.AppSettings["serverAdress"],
                                  ConfigurationManager.AppSettings["dbName"]);
                string sqlCommand =
                    String.Format(
                        @"USE [{1}] 
                CREATE USER [{0}] FOR LOGIN [{0}]
                 USE [{0}]
                 EXEC sp_addrolemember N'db_owner', N'{0}'
                 ", ConfigurationManager.AppSettings["poolName"], ConfigurationManager.AppSettings["dbName"]);

                SqlConnection conn = new SqlConnection(connectionString);

                conn.Open();

                SqlCommand comm = new SqlCommand(sqlCommand, conn);
                comm.ExecuteNonQuery();

                conn.Close();
                Console.WriteLine("Ustawiono prawa dla " + ConfigurationManager.AppSettings["poolName"]);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
    }
}
