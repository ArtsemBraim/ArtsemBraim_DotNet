using System;
using System.Collections.Generic;
using System.Text;

namespace laba1.Models
{
    public class OutputModel
    {
        public IReadOnlyCollection<StudentRating> StudentsRating { get; set; }

        public double AverageGroupRating { get; set; }

        public IReadOnlyCollection<SubjectRating> SubjectsRating { get; set; }
    }
}
