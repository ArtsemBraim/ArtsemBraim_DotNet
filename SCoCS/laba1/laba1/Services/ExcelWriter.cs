using laba1.Interfaces;
using laba1.Models;
using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;

namespace laba1.Services
{
    public class ExcelWriter : IFileWriter
    {
        public void Write(IEnumerable<Student> students, string path)
        {
            using (var package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Student performance");
                var averageMarks = students.GetAverageMarkByStudents();
                var studentEnumerator = students.GetEnumerator();
                var markEnumerator = averageMarks.GetEnumerator();
                int row = 1;

                while (studentEnumerator.MoveNext() && markEnumerator.MoveNext())
                {
                    worksheet.Cells[row, 1].Value = studentEnumerator.Current.Surname;
                    worksheet.Cells[row, 2].Value = studentEnumerator.Current.Name;
                    worksheet.Cells[row, 3].Value = studentEnumerator.Current.MiddleName;
                    worksheet.Cells[row, 4].Value = markEnumerator.Current;
                    row++;
                }

                var subjects = students.GetAvarageMarkBySubjects();
                var subjectEnumerator = subjects.GetEnumerator();

                while (subjectEnumerator.MoveNext())
                {
                    worksheet.Cells[row, 1].Value = subjectEnumerator.Current.subject;
                    worksheet.Cells[row, 2].Value = subjectEnumerator.Current.mark;
                    row++;
                }

                worksheet.Cells[row, 1].Value = "Average group rating";
                worksheet.Cells[row, 2].Value = students.GetAvarageMarkByGroup();
                row++;

                var xlFile = new FileInfo(path);
                package.SaveAs(xlFile);
            }
        }
    }
}