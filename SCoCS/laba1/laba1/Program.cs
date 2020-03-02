using laba1.Interfaces;
using laba1.Models;
using laba1.Services;
using McMaster.Extensions.CommandLineUtils;
using NLog;
using System;
using System.Collections.Generic;

namespace laba1
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const string ExcelFormatName = "excel";
        private const string JsonFormatName = "json";

        [Option("-i", Description = "The path of the input file")]
        public string InputFilePath { get; }

        [Option("-o", Description = "The path of the output file")]
        public string OutputFilePath { get; set; }

        [Option("-f", Description = "The file format")]
        public string FileFormat { get; set; }

        public static void Main(string[] args) 
            => CommandLineApplication.Execute<Program>(args);

        private void OnExecute()
        {
            StudentCsvReader reader = new StudentCsvReader();
            IEnumerable<Student> students = reader.Read(InputFilePath);
            IFileWriter writer = null;
            if (FileFormat.ToLower() == ExcelFormatName)
            {
                writer = new ExcelWriter();
            }
            else if (FileFormat.ToLower() == JsonFormatName)
            {
                writer = new JsonWriter();
            }
            else
            {
                Console.WriteLine($"{FileFormat} is unknown format");
            }

            if (writer != null && students != null)
            {
                writer.Write(students, OutputFilePath);
            }
            else
            {
                logger.Error("The result not recorded");
            }
        }
    }
}
