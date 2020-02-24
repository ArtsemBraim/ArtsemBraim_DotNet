using laba1.Models;
using System.Collections.Generic;

namespace laba1.Interfaces
{
    public interface IFileWriter
    {
        void Write(IEnumerable<Student> students, string path);
    }
}
