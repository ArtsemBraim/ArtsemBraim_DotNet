using Clinic.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.BLL.Dto
{
    public class TimeSlotDto
    {
        public int Id { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }

        public Patient Patient { get; set; }
    }
}
