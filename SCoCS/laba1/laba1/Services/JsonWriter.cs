using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

using laba1.Interfaces;
using laba1.Models;

namespace laba1.Services
{
    public class JsonWriter : IFileWriter
    {
        public void Write(IEnumerable<Student> students, string path)
        {
            using var writer = new StreamWriter(path);
            var output = new OutputModel
            {
                StudentsRating = students.GetAverageMarkByStudents(),
                AverageGroupRating = students.GetAvarageMarkByGroup(),
                SubjectsRating = students.GetAvarageMarkBySubjects()
            };

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string jsonString = JsonSerializer.Serialize(output, options);
            writer.Write(jsonString);
        }
    }
}
