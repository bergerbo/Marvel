using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marvel.Models
{
    public class DataContainer<Data>
    {
        public int? offset;
        public int? limit;
        public int? total;
        public int? count;
        public Data[] results;
    }
}