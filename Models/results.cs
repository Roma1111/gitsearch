using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitSearch.Models
{
    public class results
    {
        public int total_count { get; set; }
        public bool incomplete_results { get; set; }
        public List<item> items { get; set; }
    }
}