using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace habersitesi2.Models
{
    public class Kullanici
    {
        public int  id { get; set; }
        public string AdSoyad { get; set; }
        public string EPosta { get; set; }
        public string Telefon { get; set; }
        public string KullaniciAd { get; set; }
        public string Sifre { get; set; }
    }
}