using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using CsvHelper;
using NLog;

using laba1.Interfaces;
using laba1.Models;

namespace laba1.Services
{
    public class StudentCsvReader : IFileReader
    {
        private const int IndexOfFirstMark = 3;

        private ILogger Logger { get; }

        public StudentCsvReader()
        {
            Logger = LogManager.GetCurrentClassLogger();
        }

        public IEnumerable<Student> Read(string path)
        {
            if (!File.Exists(path))
            {
                Logger.Error($"Could not find file {(new FileInfo(path)).FullName}");
                throw new FileNotFoundException();
            }

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
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
                        Logger.Error($"Student not added due to incorrect format found in column \"{csv.Context.HeaderRecord[i]}\"");
                        throw;
                    }
                    catch (CsvHelper.MissingFieldException)
                    {
                        Logger.Error("Student not added due to invalid row");
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex.Message);
                        throw;
                    }
                }

                try
                {
                    students.Add(new Student(csv.GetField<string>(0), csv.GetField<string>(1), csv.GetField<string>(2), exams.AsReadOnly()));
                }
                catch (CsvHelper.MissingFieldException)
                {
                    Logger.Error("Students not added due to invalid header");
                    throw;
                }
                catch (Exception ex)
                {
                    Logger.Error(ex.Message);
                    throw;
                }
            }

            return students;
        }
    }
}