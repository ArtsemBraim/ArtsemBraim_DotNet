using laba1.Models;
using System.Collections.Generic;

namespace laba1.Interfaces
{
    public interface IFileReader
    {
        IEnumerable<Student> Read(string path);
    }
}
