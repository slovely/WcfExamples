using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Linq;
using WcfExamples.Contracts;
using Dapper;

namespace WcfExamples.Services
{
    public class DatabaseService : IDatabaseService
    {
        private SqlConnection _connection;

        public DatabaseService()
        {
            //Connection and connection string should abstracted and injected in
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SampleDBConnStr"].ConnectionString);
        }

        public Person GetObjectFromDatabase(int id)
        {
            //TODO: all the connection, try/catch, etc needs to be abstracted
            try
            {
                _connection.Open();
                return _connection.Query<Person>("LoadPerson", new {id = id}, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
            catch
            {
                int a = 123;
                throw;
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
