using Clinic.DAL.Interfaces;
using System.Collections.Generic;

namespace Clinic.DAL.Domain
{
    public class Schedule : IEntity
    {
        public int Id { get; set; }

        public List<TimeSlot> TimeSlots { get; set; }

        public Doctor Doctor { get; set; }
    }
}
