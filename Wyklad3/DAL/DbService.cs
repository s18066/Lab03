using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Wyklad3.Models;

namespace Wyklad3.DAL
{
    public class DbService : IDbService
    {
        private readonly string _connectionString;
        public DbService(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await PerformQuery(
                "select FirstName, LastName, IndexNumber from Student",
                null,
                reader => new Student()
                {
                    FirstName = reader["FirstName"].ToString(),
                    IndexNumber = reader["IndexNumber"].ToString(),
                    LastName = reader["LastName"].ToString(),
                });
        }

        public async Task<Student> GetStudent(string id)
        {
            var result =  await PerformQuery(
                "select FirstName, LastName, IndexNumber from Student where IndexNumber = @id ",
                new []
                {
                    new SqlParameter("id", id)
                },
                reader => new Student()
                {
                    FirstName = reader["FirstName"].ToString(),
                    IndexNumber = reader["IndexNumber"].ToString(),
                    LastName = reader["LastName"].ToString(),
                });

            return result.SingleOrDefault();
        }

        public async Task<IEnumerable<Enrollment>> GetStudentEnrollments(string studentId)
        {
            return await PerformQuery(
                @"select Enrollment.StartDate, Enrollment.Semester, Studies.Name as ClassName from Enrollment 
                join Student on Student.IdEnrollment = Enrollment.IdEnrollment
	            join Studies on Studies.IdStudy = Enrollment.IdStudy
	            where IndexNumber = 's1'",
                new []
            {
                new SqlParameter("id", studentId)
            },
            reader => new Enrollment(
                    reader["ClassName"].ToString(),
                    Convert.ToInt32(reader["Semester"]),
                        DateTime.Parse(reader["StartDate"].ToString()))
                );
        }

        private  async Task<IEnumerable<T>> PerformQuery<T>(
            string commandText,
            IEnumerable<SqlParameter> queryParameters,
            Func<SqlDataReader, T> resultMapping)
        {
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand
            {
                Connection = connection,
                CommandText = commandText
            };
            
            command.Parameters.AddRange(queryParameters?.ToArray() ?? new SqlParameter[0]);

            await connection.OpenAsync();

            var dataReader = await command.ExecuteReaderAsync();
            
            var itemList = new List<T>();
            
            while (await dataReader.ReadAsync())
            {
                itemList.Add(resultMapping(dataReader));
            }

            return itemList;
        }
    }
}
