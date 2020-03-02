using laba1.Models;
using System.Collections.Generic;
using System.Linq;

namespace laba1.Services
{
    public static class StudentExtensions
    {
        public static IReadOnlyCollection<StudentRating> GetAverageMarkByStudents(this IEnumerable<Student> students)
        {
            return students.Select(s => new StudentRating
                                        {
                                            StudentName = s.Name,
                                            StudentSurname = s.Surname,
                                            StudentMiddleName = s.MiddleName,
                                            AverageMark = s.Exams.Average(e => e.Mark)
                                        })
                           .ToList().AsReadOnly();
        }

        public static double GetAvarageMarkByGroup(this IEnumerable<Student> students)
        {
            return students.GetAverageMarkByStudents().Select(s => s.AverageMark).Average();
        }

        public static IReadOnlyCollection<string> GetSubjects(this Student student)
        {
            return student.Exams.Select(e => e.Subject).ToList().AsReadOnly();
        }

        public static IReadOnlyCollection<SubjectRating> GetAvarageMarkBySubjects(this IEnumerable<Student> students)
        {
            var averageMarks = new List<SubjectRating> ();

            foreach (string subject in students.First().GetSubjects())
            {
                var averageMark = (subject, students.SelectMany(s => s.Exams, (s, e) => new { Student = s, Exam = e })
                                         .Where(x => x.Exam.Subject == subject)
                                         .Select(x => x.Exam.Mark)
                                         .Average());

                var subjectRating = new SubjectRating { Mark = averageMark.Item2, Subject = averageMark.subject };

                averageMarks.Add(subjectRating);
            }

            return averageMarks.AsReadOnly();
        }
    }
}
