using System;
using System.Collections.Generic;
using Wyklad3.Models;

namespace Wyklad3.DAL
{
    public class OracleDbService : IDbService
    {
        public IEnumerable<Student> GetStudents()
        {
            //real db connection
            throw new NotImplementedException();
        }
    }
}
