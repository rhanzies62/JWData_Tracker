using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace JWDataTracker.Domain.Grid
{

    public class PublisherRecentPartGrid
    {
        public string ScheduledDate { get; set; }
        public DateTime ScheduledDateDT
        {
            get
            {
                return JsonConvert.DeserializeObject<DateTime>(ScheduledDate);
            }
        }
        public string Category { get; set; }
        public string part { get; set; }
        public long IsPartner { get; set; }
    }
}
