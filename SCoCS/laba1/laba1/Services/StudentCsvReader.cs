using CsvHelper;
using laba1.Interfaces;
using laba1.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace laba1.Services
{
    public class StudentCsvReader : IFileReader
    {
        private const int IndexOfFirstMark = 3;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public IEnumerable<Student> Read(string path)
        {
            if (File.Exists(path))
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
                                try
                                {
                                    exams.Add(new Exam(csv.Context.HeaderRecord[i], csv.GetField<int>(i)));
                                }
                                catch (CsvHelper.TypeConversion.TypeConverterException)
                                {
                                    logger.Error($"Student not added due to incorrect format found in column \"{csv.Context.HeaderRecord[i]}\"");
                                    return null;
                                }
                                catch (CsvHelper.MissingFieldException)
                                {
                                    logger.Error("Student not added due to invalid row");
                                    return null;
                                }
                                catch (InvalidOperationException ex)
                                {
                                    logger.Error($"Exam not added because {ex.Message.ToLower()}");
                                    return null;
                                }
                            }

                            try
                            {
                                students.Add(new Student(csv.GetField<string>(0), csv.GetField<string>(1), csv.GetField<string>(2), exams));
                            }
                            catch (CsvHelper.MissingFieldException)
                            {
                                logger.Error("Students not added due to invalid header");
                                return null;
                            }
                        }

                        return students;
                    }
                }
            }
            else
            {
                logger.Error($"Could not find file {(new FileInfo(path)).FullName}");
                return null;
            }
        }
    }
}