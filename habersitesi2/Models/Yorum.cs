using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace habersitesi2.Models
{
    public class Yorum
    {
        public int id { get; set; }
        public string isim { get; set; }
        public string yorum { get; set; }
        public int haberId { get; set; }
        public bool durum { get; set; }
        public string tarih { get; set; }
    }
}