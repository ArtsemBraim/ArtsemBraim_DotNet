using Clinic.DAL.Interfaces;

namespace Clinic.DAL.Domain
{
    public class Specialization : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
