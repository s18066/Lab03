using System.Collections.Generic;
using Wyklad3.Models;

namespace Wyklad3.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}
