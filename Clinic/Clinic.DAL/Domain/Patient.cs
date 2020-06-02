namespace Clinic.DAL.Domain
{
    public class Patient : Entity
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }
    }
}
