using laba1.Interfaces;
using laba1.Models;
using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;
using NLog;

namespace laba1.Services
{
    public class ExcelWriter : IFileWriter
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private const int StartRow = 1;
        private const string WorksheetName = "Student performance";
        private const string HeaderForGroupRating = "Average group rating";


        public void Write(IEnumerable<Student> students, string path)
        {
            if (File.Exists(path))
            {
                using (var package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(WorksheetName);

                    var studentsRating = students.GetAverageMarkByStudents();
                    int row = StartRow;

                    foreach (var rating in studentsRating)
                    {
                        worksheet.Cells[row, 1].Value = rating.StudentSurname;
                        worksheet.Cells[row, 2].Value = rating.StudentName;
                        worksheet.Cells[row, 3].Value = rating.StudentMiddleName;
                        worksheet.Cells[row, 4].Value = rating.AverageMark;
                        row++;
                    }

                    var subjectsRating = students.GetAvarageMarkBySubjects();

                    foreach (var rating in subjectsRating)
                    {
                        worksheet.Cells[row, 1].Value = rating.Subject;
                        worksheet.Cells[row, 2].Value = rating.Mark;
                        row++;
                    }

                    worksheet.Cells[row, 1].Value = HeaderForGroupRating;
                    worksheet.Cells[row, 2].Value = students.GetAvarageMarkByGroup();

                    package.SaveAs(new FileInfo(path));
                }
            }
            else
            {
                logger.Error("The output path is does not exist");
            }
            
        }
    }
}