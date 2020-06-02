using Clinic.DAL.Interfaces;

namespace Clinic.DAL.Domain
{
    public class Patient : IEntity
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }
    }
}
