namespace laba1.Models
{
    public class Exam
    {
        public string Subject { get; set; }

        public int Mark { get; set; }

        public Exam()
        {

        }

        public Exam(string subject, int mark)
        {
            Subject = subject;
            Mark = mark;
        }
    }
}
