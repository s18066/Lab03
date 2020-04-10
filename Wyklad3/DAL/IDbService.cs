using System.Collections.Generic;
using System.Threading.Tasks;
using Wyklad3.Models;

namespace Wyklad3.DAL
{
    public interface IDbService
    {
        public Task<IEnumerable<Student>> GetStudents();

        public Task<Student> GetStudent(string id);

        Task<IEnumerable<Enrollment>> GetStudentEnrollments(string studentId);
    }
}
