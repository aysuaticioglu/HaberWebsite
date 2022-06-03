using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace habersitesi2.Models
{
 
    public class Icerik
    {
        public int id { get; set; }
        public string baslik { get; set; }
        public string altbaslik { get; set; }
        [AllowHtml]
        public string icerik { get; set; }
        public HttpPostedFileBase fotograf { get; set; }
        public string fotografAdi { get; set; }
        public int kategoriId { get; set; }
        public string kategoriAd { get; set; }
        public string mansetAd { get; set; }
        public int mansetId { get; set; }
        public DateTime tarih { get; set; }
     



    }
}