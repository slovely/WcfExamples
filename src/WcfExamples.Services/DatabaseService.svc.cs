using System;
using System.Collections.Generic;
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
            return ExecuteSproc<Person>("LoadPerson", new {id = id});
        }

        public void SavePerson(Person person)
        {
            ExecuteSproc<int>("InsertPerson", new {Name = person.Name, DateOfBirth = person.DateOfBirth});
        }

        public Tuple<List<Animal>, List<Person>> LoadPeopleAndAnimals()
        {
            Tuple<List<Animal>, List<Person>> result;
            _connection.Open();
            using (var multi = _connection.QueryMultiple("ReturnAllPeopleAndAnimals", commandType: CommandType.StoredProcedure))
            {
                result = new Tuple<List<Animal>, List<Person>>(
                    multi.Read<Animal>().ToList(), multi.Read<Person>().ToList());
            }
            _connection.Close();
            return result;
        }

        private TResult ExecuteSproc<TResult>(string sql, object parameters)
        {
            _connection.Open();
            try
            {
                return _connection.Query<TResult>(sql, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}
