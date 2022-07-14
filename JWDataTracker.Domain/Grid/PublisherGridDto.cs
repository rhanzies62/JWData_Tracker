using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace JWDataTracker.Domain.Grid
{
    public class PublisherGridDto
    {
        public long PublisherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CreatedDate { get; set; }
        public DateTime CreatedDateDt
        {
            get
            {
                return JsonConvert.DeserializeObject<DateTime>(CreatedDate);
            }
        }


        public long? CongregationId { get; set; }
        public long IsElder { get; set; }
        public bool IsElderBool { get { return IsElder == 1; } }

        public long IsMs { get; set; }
        public bool IsMsBool { get { return IsMs == 1; } }

        public long IsRp { get; set; }
        public bool IsRpBool { get { return IsRp == 1; } }

        public long IsUnBaptized { get; set; }
        public bool IsUnBaptizedBool { get { return IsUnBaptized == 1; } }

        public long GroupNumber { get; set; }
    }
    public class Count
    {
        public int TotalCount { get; set; }
    }
}
