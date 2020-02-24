using laba1.Models;
using System.Collections.Generic;
using System.Linq;

namespace laba1.Services
{
    public static class StudentExtensions
    {
        public static IReadOnlyCollection<double> GetAverageMarkByStudents(this IEnumerable<Student> students)
        {
            return students.Select(s => s.Exams.Average(e => e.Mark)).ToList().AsReadOnly();
        }

        public static double GetAvarageMarkByGroup(this IEnumerable<Student> students)
        {
            return students.GetAverageMarkByStudents().Average();
        }

        public static IReadOnlyCollection<string> GetSubjects(this Student student)
        {
            return student.Exams.Select(e => e.Subject).ToList().AsReadOnly();
        }

        public static IReadOnlyCollection<(string subject, double mark)> GetAvarageMarkBySubjects(this IEnumerable<Student> students)
        {
            var averageMarks = new List<(string subject, double mark)>();

            foreach (string subject in students.First().GetSubjects())
            {
                var averageMark = (subject, students.SelectMany(e => e.Exams, (s, e) => new { Student = s, Exam = e })
                                         .Where(x => x.Exam.Subject == subject)
                                         .Select(x => x.Exam.Mark)
                                         .Average());

                averageMarks.Add(averageMark);
            }

            return averageMarks.AsReadOnly();
        }
    }
}
