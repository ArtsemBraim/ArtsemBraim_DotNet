using System.Collections.Generic;

namespace laba1.Models
{
    public class Student
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }
        
        public IReadOnlyCollection<Exam> Exams { get; set; }

        public Student(string surname, string name, string middleName, IReadOnlyCollection<Exam> exams)
        {
            Surname = surname;
            Name = name;
            MiddleName = middleName;
            Exams = exams;
        }
    }
}
