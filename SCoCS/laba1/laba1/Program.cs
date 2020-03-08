using System;
using System.Collections.Generic;

using McMaster.Extensions.CommandLineUtils;
using NLog;

using laba1.Interfaces;
using laba1.Models;
using laba1.Services;

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
            try
            {
                StudentCsvReader reader = new StudentCsvReader();
                IEnumerable<Student> students = reader.Read(InputFilePath);
                IFileWriter writer = null;
                switch (FileFormat.ToLower())
                {
                    case ExcelFormatName:
                        writer = new ExcelWriter();
                        break;
                    case JsonFormatName:
                        writer = new JsonWriter();
                        break;
                    default:
                        logger.Error($"{FileFormat} is unknown format");
                        break;
                }
                writer?.Write(students, OutputFilePath);
            }
            catch (Exception)
            {
                logger.Error("The result not recorded");
            }
        }
    }
}
