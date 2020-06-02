using Clinic.DAL.Domain;
using System.Collections.Generic;

namespace Clinic.BLL.Dto
{
    public class ScheduleDto
    {
        public int Id { get; set; }

        public List<TimeSlot> TimeSlots { get; set; }

        public Doctor Doctor { get; set; }
    }
}
