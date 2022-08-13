using System;
namespace JWDataTracker.Application.LookUp
{
    public class LookUpDto
    {
        public LookUpDto()
        {
        }

        public long LookUpId { get; set; }
        public string Text { get; set; }
        public string Code { get; set; }
        public long? SortOrder { get; set; }
    }
}

