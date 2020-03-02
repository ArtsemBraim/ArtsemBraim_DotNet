using laba1.Interfaces;
using laba1.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace laba1.Services
{
    public class JsonWriter : IFileWriter
    {
        public void Write(IEnumerable<Student> students, string path)
        {
            var output = new OutputModel
            {
                StudentsRating = students.GetAverageMarkByStudents(),
                AverageGroupRating = students.GetAvarageMarkByGroup(),
                SubjectsRating = students.GetAvarageMarkBySubjects()
            };

            string jsonString = JsonSerializer.Serialize(output);
            File.WriteAllText(path, jsonString);
        }
    }
}
