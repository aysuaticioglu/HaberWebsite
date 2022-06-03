using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace habersitesi2.Models
{
    public class Iletisim
    {
        public int id { get; set; }
        public string mail { get; set; }
        public string konu { get; set; }
        public string mesaj { get; set; }
    }
}