using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace habersitesi2.Models
{
    public class Gizlilik
    {
        public int id { get; set; }

        [AllowHtml]
        public string icerik { get; set; }
    }
}