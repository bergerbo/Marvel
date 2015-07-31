using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Marvel.Models;

namespace Marvel.Models
{
    public class ComicDataContainer
    {
        public int? offset;
        public int? limit;
        public int? total;
        public int? count;
        public Comic[] results;
    }
}