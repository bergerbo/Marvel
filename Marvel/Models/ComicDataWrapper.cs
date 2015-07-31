using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Marvel.Models
{
    public class ComicDataWrapper
    {
        public int? code;
        public string status;
        public string copyright;
        public string attributionText;
        public string attributionHTML;
        public ComicDataContainer data;
        public string etag;
    }
}