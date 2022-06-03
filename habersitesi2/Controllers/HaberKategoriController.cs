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
    public class HaberKategoriController : Controller
    {
        // GET: HaberKategori
        SqlConnection cn = new SqlConnection("Server=.;Database=haber;Trusted_Connection=True;");

        public ActionResult Index(int kid)
        {
            cn.Open();
            SqlDataAdapter da_kategori = new SqlDataAdapter("select  I.*,K.KategoriAd from t_icerik I, t_kategori K where K.id = I.kategoriId and I.kategoriId=" + kid, cn);
            DataTable dtkat = new DataTable();
            da_kategori.Fill(dtkat);

            cn.Close();
            List<Icerik> lstkategori = new List<Icerik>();
            foreach (DataRow rw in dtkat.Rows)
            {
                Icerik kat = new Icerik();
                kat.id = Convert.ToInt32(rw["id"]);
                kat.baslik = rw["baslik"].ToString();
                kat.altbaslik = rw["altbaslik"].ToString();
                kat.icerik = rw["icerik"].ToString();
                //ic.fotograf = rw["fotograf"].ToString();
                kat.fotografAdi = rw["fotograf"].ToString();
                kat.kategoriId = Convert.ToInt32(rw["kategoriId"]);
                kat.kategoriAd = rw["kategoriAd"].ToString();
                kat.mansetId = Convert.ToInt32(rw["mansetId"]);

                if (rw["tarih"] != null && rw["tarih"].ToString() != "")
                {
                    kat.tarih = Convert.ToDateTime( rw["tarih"]);
                }
                lstkategori.Add(kat);
            }
            ViewBag.kategori = lstkategori;

            return View();
        }
    }
}