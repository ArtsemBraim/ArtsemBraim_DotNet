using Clinic.DAL.Interfaces;
using System;

namespace Clinic.DAL.Domain
{
    public class TimeSlot : IEntity
    {
        public int Id { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }

        public Patient Patient { get; set; }
    }
}
