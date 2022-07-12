using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWDataTracker.Helper
{
    public class GridResultGeneric<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class GridFilter
    {
        public string Operator { get; set; }
        public string Field { get; set; }
        public string Direction { get; set; }
        public string _value { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public List<GridSearchFilter> Searchs { get; set; }

    }

    public class GridSearchFilter
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
        public string AppendType { get; set; }
    }
}
