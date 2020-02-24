using CsvHelper;
using laba1.Interfaces;
using laba1.Models;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace laba1.Services
{
    public class StudentCsvReader : IFileReader
    {
        private const int IndexOfFirstMark = 3;
        private const string SurnameString = "Surname";
        private const string NameString = "Name";
        private const string MiddleNameString = "MiddleName";

        public IEnumerable<Student> Read(string path)
        {
            using (var reader = new StreamReader(path))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var students = new List<Student>();

                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var exams = new List<Exam>();

                        for (int i = IndexOfFirstMark; i < csv.Context.HeaderRecord.Length; i++)
                        {
                            exams.Add(new Exam(csv.Context.HeaderRecord[i], csv.GetField<int>(i)));
                        }
                        students.Add(new Student(csv.GetField(SurnameString), csv.GetField(NameString), csv.GetField(MiddleNameString), exams));
                    }

                    return students;
                }
            }
        }
    }
}
