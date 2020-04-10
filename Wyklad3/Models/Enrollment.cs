using System;

namespace Wyklad3.Models
{
    public class Enrollment
    {
        public Enrollment(string className, int semester, DateTime startDate)
        {
            ClassName = className;
            Semester = semester;
            StartDate = startDate;
        }
        
        public string ClassName { get; }

        public int Semester { get; }
        
        public DateTime StartDate { get; }
    }
}