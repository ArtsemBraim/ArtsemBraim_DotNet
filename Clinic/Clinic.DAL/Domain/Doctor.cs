namespace Clinic.DAL.Domain
{
    public class Doctor : Entity
    {
        public string Surname { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string ConsultingRoom { get; set; }

        public string Specialization { get; set; }
    }
}
