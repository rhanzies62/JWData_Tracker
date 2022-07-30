using System;
using System.Collections.Generic;

namespace JWDataTracker.Infrastructure
{
    public partial class LookUp
    {
        public long LookUpId { get; set; }
        public string Text { get; set; }
        public string Code { get; set; }
        public long? SortOrder { get; set; }
    }
}
