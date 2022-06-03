using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace habersitesi2.Models
{
    public class Kategori
    {
        internal string baslik;
        internal string altbaslik;
        internal string icerik;
        internal string fotograf;
        internal int kategoriId;
        internal int mansetId;
        internal DateTime tarih;

        public int id { get; set; }
        public string KategoriAd { get; set; }

        public int Sira { get; set; }
    }
}