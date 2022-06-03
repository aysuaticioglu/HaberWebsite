using habersitesi2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace habersitesi2.Controllers
{
    public class HaberDuzenleController : Controller
    {
        // GET: HaberDuzenle

        SqlConnection cn = new SqlConnection("Server=.;Database=haber;Trusted_Connection=True;");


        public ActionResult Index(int haberId)
        {

            cn.Open();

            SqlDataAdapter da = new SqlDataAdapter("select * from t_kategori", cn);
            DataTable dtKategori = new DataTable();
            da.Fill(dtKategori);

            SqlDataAdapter da_manset = new SqlDataAdapter("select * from t_manset", cn);
            DataTable dtManset = new DataTable();
            da_manset.Fill(dtManset);

            SqlDataAdapter da_icerik = new SqlDataAdapter("select * from t_icerik order by id desc", cn);
            DataTable dticerik = new DataTable();
            da_icerik.Fill(dticerik);

            cn.Close();

            List<Kategori> lstkategori = new List<Kategori>();
            foreach (DataRow rw in dtKategori.Rows)
            {
                Kategori kat = new Kategori();
                kat.id = Convert.ToInt32(rw["id"]);
                kat.KategoriAd = rw["kategoriAd"].ToString();
                kat.Sira = Convert.ToInt32(rw["sira"]);
                lstkategori.Add(kat);

            }
            ViewBag.kategori = lstkategori;
            //--------------------------------------------//
            List<Manset> lstmanset = new List<Manset>();
            foreach (DataRow rw in dtManset.Rows)
            {
                Manset man = new Manset();
                man.id = Convert.ToInt32(rw["id"]);
                man.MansetAd = rw["MansetAd"].ToString();
                lstmanset.Add(man);

            }
            ViewBag.manset = lstmanset;


            SqlDataAdapter da_haber = new SqlDataAdapter("select * from t_icerik where id=" + haberId, cn);
            DataTable dtHaberler = new DataTable();
            da_haber.Fill(dtHaberler);

            cn.Close();

            List<Icerik> lstHaberler = new List<Icerik>();
            foreach (DataRow rw in dtHaberler.Rows)
            {
                Icerik haber = new Icerik();
                haber.id = Convert.ToInt32(rw["id"]);
                haber.baslik = rw["baslik"].ToString();
                haber.altbaslik = rw["altbaslik"].ToString();
                haber.icerik = rw["icerik"].ToString();
                haber.fotografAdi = rw["fotograf"].ToString();
                haber.kategoriId = Convert.ToInt32(rw["kategoriId"]);
                haber.mansetId = Convert.ToInt32(rw["mansetId"]);
                haber.tarih = Convert.ToDateTime(rw["tarih"]);

                lstHaberler.Add(haber);

            }
            ViewBag.haberler = lstHaberler;



            return View();

        }


        public ActionResult Duzenle(Icerik haber)
        {
            cn.Open();
            string updateFoto;
            if (haber.fotograf != null )
            {
                var path = Path.Combine(Server.MapPath("~/Images"), haber.fotograf.FileName);
                haber.fotograf.SaveAs(path);
                updateFoto = haber.fotograf.FileName;
            }
            else
            {
                updateFoto = haber.fotografAdi;
            }


            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "UPDATE [dbo].[t_icerik] SET baslik=@baslik,altbaslik=@altbaslik,icerik=@icerik,fotograf=@fotograf,kategoriId=@kategoriId,mansetId=@mansetId,tarih=@tarih WHERE id="+ haber.id;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@baslik", haber.baslik);
            cmd.Parameters.AddWithValue("@altbaslik", haber.altbaslik);
            cmd.Parameters.AddWithValue("@icerik", haber.icerik);
            cmd.Parameters.AddWithValue("@fotograf", updateFoto);
            cmd.Parameters.AddWithValue("@kategoriId", haber.kategoriId);
            cmd.Parameters.AddWithValue("@mansetId", haber.mansetId);
            cmd.Parameters.AddWithValue("@tarih", haber.tarih);

            cmd.ExecuteNonQuery();
            cn.Close();
            return RedirectToAction("Index", "HaberEkle");
        }


    }
}   