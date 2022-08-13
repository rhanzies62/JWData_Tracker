using JWDataTracker.Application.LookUp;
using JWDataTracker.Application.MidWeekMeetingSchedule;
using JWDataTracker.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWDataTracker.Application.Publisher
{
    public class PublisherDto
    {
        public long PublisherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? CongregationId { get; set; }
        public bool IsRp { get; set; }
        public long GroupNumber { get; set; }
        public long? Privilege { get; set; }
        public long? Status { get; set; }
        public long? Gender { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public string UserPrivilege { get; set; }
        public string UserStatus { get; set; }
        public string UserGender { get; set; }

        public LookUpDto PrivilegeLookUp { get; set; }
        public LookUpDto StatusLookUp { get; set; }
        public LookUpDto GenderLookUp { get; set; }
    }
}
