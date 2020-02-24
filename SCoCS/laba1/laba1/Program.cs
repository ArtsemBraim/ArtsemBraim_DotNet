using laba1.Interfaces;
using laba1.Models;
using laba1.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace laba1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"..\..\..\input.csv";
            StudentCsvReader reader = new StudentCsvReader();
            List<Student> students = reader.Read(path).ToList();
            IFileWriter fileWriter = new ExcelWriter();
            fileWriter.Write(students, @"..\..\..\output.xlsx");
        }
    }
}
