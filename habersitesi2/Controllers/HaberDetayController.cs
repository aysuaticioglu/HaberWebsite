using habersitesi2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace habersitesi2.Controllers
{
    public class HaberDetayController : Controller
    {
        SqlConnection cn = new SqlConnection("Server=.;Database=haber;Trusted_Connection=True;");

        // GET: HaberDetay
        public ActionResult Index(int haberId, int kategoriId)
        {
            cn.Open();
            SqlDataAdapter da_icerik = new SqlDataAdapter("select  I.*,K.KategoriAd from t_icerik I, t_kategori K where K.id = I.kategoriId and I.id="+haberId, cn);
            DataTable dticerik = new DataTable();
            da_icerik.Fill(dticerik);

          
            cn.Close();
            List<Icerik> lsticerik = new List<Icerik>();
            foreach (DataRow rw in dticerik.Rows)
            {
                Icerik ic = new Icerik();
                ic.id = Convert.ToInt32(rw["id"]);
                ic.baslik = rw["baslik"].ToString();
                ic.altbaslik = rw["altbaslik"].ToString();
                ic.icerik = rw["icerik"].ToString();
                ic.fotografAdi = rw["fotograf"].ToString();
                ic.kategoriId = Convert.ToInt32(rw["kategoriId"]);
                ic.kategoriAd = rw["kategoriAd"].ToString();
                ic.mansetId = Convert.ToInt32(rw["mansetId"]);
               
                if (rw["tarih"] != null && rw["tarih"].ToString() != "")
                {
                    ic.tarih = Convert.ToDateTime(rw["tarih"]);
                }
                lsticerik.Add(ic);
            }
            ViewBag.icerik = lsticerik;


       

            cn.Open();
            SqlDataAdapter da_kategori = new SqlDataAdapter("select top 4  I.*,K.KategoriAd from t_icerik I, t_kategori K where( K.id = I.kategoriId and I.kategoriId=" + kategoriId+") and not I.id="+haberId, cn);
            DataTable dtkat = new DataTable();
            da_kategori.Fill(dtkat);

            cn.Close();
            List<Icerik> lstkat = new List<Icerik>();
            foreach (DataRow rw in dtkat.Rows)
            {
                Icerik kat = new Icerik();
                kat.id = Convert.ToInt32(rw["id"]);
                kat.baslik = rw["baslik"].ToString();
                kat.altbaslik = rw["altbaslik"].ToString();
                kat.icerik = rw["icerik"].ToString();
                kat.fotografAdi = rw["fotograf"].ToString();
                kat.kategoriId = Convert.ToInt32(rw["kategoriId"]);
                kat.kategoriAd = rw["kategoriAd"].ToString();
                kat.mansetId = Convert.ToInt32(rw["mansetId"]);

                if (rw["tarih"] != null && rw["tarih"].ToString() != "")
                {
                    kat.tarih = Convert.ToDateTime(rw["tarih"]);
                }
                lstkat.Add(kat);
            }
            ViewBag.kategori = lstkat;

            return View();
        }
   

        }
}