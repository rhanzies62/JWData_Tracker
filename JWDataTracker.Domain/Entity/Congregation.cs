using System;
using System.Collections.Generic;

namespace JWDataTracker.Infrastructure
{
    public partial class Congregation
    {
        public Congregation()
        {
            Publishers = new HashSet<Publisher>();
        }

        public long CongregationId { get; set; }
        public string Name { get; set; }
        public string CreatedDate { get; set; }

        public virtual ICollection<Publisher> Publishers { get; set; }
    }
}
