using laba1.Interfaces;
using laba1.Models;
using laba1.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace laba1
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            string path = @"..\..\..\TestFiles\6_TestFile.csv";
            StudentCsvReader reader = new StudentCsvReader();
            IEnumerable<Student> students = reader.Read(path);
            IFileWriter excelWriter = new ExcelWriter();
            IFileWriter jsonWriter = new JsonWriter();
            if (students != null)
            {
                excelWriter.Write(students, @"..\..\..\output.xlsx");
                jsonWriter.Write(students, @"..\..\..\output.json");
            }
            else
            {
                logger.Error("The result not recorded");
            }
        }
    }
}
