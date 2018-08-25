using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitSearch.Models
{
    public class license
    {
        public string key { get; set; }
        public string name { get; set; }
        public string spdx_id { get; set; }
        public string url { get; set; }
        public string node_id { get; set; }
    }
}