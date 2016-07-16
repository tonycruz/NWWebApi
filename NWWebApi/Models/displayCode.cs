using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWWebApi.Models
{
    public class displayCode
    {
        public int id { get; set; }
        public string accordionName { get; set; }
        public string title { get; set; }
        public string hrefName { get; set; }
        public string href { get; set; }
        public string fileLocation { get; set; }
        public string fileContent { get; set; }
        public string group { get; set; }
    }
}
