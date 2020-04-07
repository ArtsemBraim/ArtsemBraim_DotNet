using System;

namespace laba1.Models
{
    public class Exam
    {
        private const int MinMark = 1;
        private const int MaxMark = 10;

        private int _mark;

        public string Subject { get; set; }

        public int Mark
        {
            get
            {
                return _mark;
            }
            set
            {
                ValidateMark(value);
                _mark = value;
            }
        }

        public Exam(string subject, int mark)
        {
            ValidateMark(mark);
            Subject = subject;
            _mark = mark;
        }

        private static void ValidateMark(int mark)
        {
            if (mark < MinMark)
            {
                throw new InvalidOperationException($"Mark can not be less than {MinMark}");
            }

            if (mark > MaxMark)
            {
                throw new InvalidOperationException($"Mark can not be great than {MaxMark}");
            }
        }
    }
}
