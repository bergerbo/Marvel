using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marvel.Models
{
    public class DataWrapper<Data>
    {
        public int? code;
        public string status;
        public string copyright;
        public string attributionText;
        public string attributionHTML;
        public DataContainer<Data> data;
        public string etag;
    }
}