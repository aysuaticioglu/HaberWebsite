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

    public class HaberEkleController : Controller
    {
        SqlConnection cn = new SqlConnection("Server=.;Database=haber;Trusted_Connection=True;");

        // GET: HaberEkle
        public ActionResult Index()
        {
            cn.Open();

            SqlDataAdapter da = new SqlDataAdapter("select * from t_kategori", cn);
            DataTable dtKategori = new DataTable();
            da.Fill(dtKategori);

            SqlDataAdapter da_manset = new SqlDataAdapter("select * from t_manset", cn);
            DataTable dtManset = new DataTable();
            da_manset.Fill(dtManset);

            SqlDataAdapter da_icerik = new SqlDataAdapter("select I.*,K.KategoriAd,M.MansetAd from t_icerik I, t_kategori K, "+
                "t_Manset M where K.id = I.kategoriId and M.id = I.mansetId order by I.id desc", cn);
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
            //--------------------------------------------//
            List<Icerik> lsticerik = new List<Icerik>();
            foreach (DataRow rw in dticerik.Rows)
            {
                Icerik ic = new Icerik();
                ic.id = Convert.ToInt32(rw["id"]);
                ic.baslik = rw["baslik"].ToString();
                ic.altbaslik = rw["altbaslik"].ToString();
                ic.icerik = rw["icerik"].ToString();
                //ic.fotograf = rw["fotograf"].ToString();
                ic.kategoriId = Convert.ToInt32(rw["kategoriId"]);
                ic.kategoriAd = rw["kategoriAd"].ToString();
                ic.mansetId = Convert.ToInt32(rw["mansetId"]);
                ic.mansetAd = rw["mansetAd"].ToString();
                if (rw["tarih"] != null && rw["tarih"].ToString() != "")
                {
                    ic.tarih = Convert.ToDateTime(rw["tarih"]);
                }
                lsticerik.Add(ic);
            }
            ViewBag.icerik = lsticerik;
            ViewBag.haberler = "";
            return View();
        }
        [HttpPost]
        public ActionResult HaberKaydet(Icerik haber)
        {

            cn.Open();
            var path = Path.Combine(Server.MapPath("~/Images"), haber.fotograf.FileName);
            haber.fotograf.SaveAs(path);
            var fotoname = haber.fotograf.FileName;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO [dbo].[t_icerik]([baslik],[altbaslik],[icerik],[fotograf],[kategoriId],[mansetId],[tarih]) VALUES (@baslik,@altbaslik,@icerik,@fotograf,@kategoriId,@mansetId,@tarih)";
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@baslik", haber.baslik);
            cmd.Parameters.AddWithValue("@altbaslik", haber.altbaslik);
            cmd.Parameters.AddWithValue("@icerik", haber.icerik);
            cmd.Parameters.AddWithValue("@fotograf", fotoname);
            cmd.Parameters.AddWithValue("@kategoriId", haber.kategoriId);
            cmd.Parameters.AddWithValue("@mansetId", haber.mansetId);
            cmd.Parameters.AddWithValue("@tarih", haber.tarih);

            cmd.ExecuteNonQuery();

            cn.Close();
            return RedirectToAction("Index");
        }





        public ActionResult HaberSil(int haberId)
        {

            cn.Open();
            SqlDataAdapter da_sil = new SqlDataAdapter("delete from t_icerik where id=" + haberId, cn);
            DataTable dtSil = new DataTable();
            da_sil.Fill(dtSil);
            cn.Close();
            return RedirectToAction("Index");
        }

        public ActionResult YorumEkle(string isim, string yorum, int haberId)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO [dbo].[t_yorum]([isim],[yorum],[tarih],[haberId],[durum])VALUES(@isim,@yorum,@tarih,@haberId,@durum)";
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@isim", isim);
            cmd.Parameters.AddWithValue("@yorum", yorum);
            cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
            cmd.Parameters.AddWithValue("@haberId", haberId);
            cmd.Parameters.AddWithValue("@durum", false);
           
            cmd.ExecuteNonQuery();
            cn.Close();
            return Json(new { Success = true });
        }
        public ActionResult YorumOku(int haberId)
        {
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from t_yorum where durum=1 and haberId=" + haberId + "order by tarih desc", cn);
            DataTable dtYorum = new DataTable();
            da.Fill(dtYorum);
            List<Yorum> lstyorum = new List<Yorum>();
            foreach (DataRow rw in dtYorum.Rows)
            {
                Yorum yorum = new Yorum();
                yorum.isim = rw["isim"].ToString();
                yorum.yorum = rw["yorum"].ToString();
                yorum.tarih = Convert.ToDateTime(rw["tarih"]).ToString("dd.MM.yyyy HH:mm");
                lstyorum.Add(yorum);

            }
            cn.Close();
            return Json(new { Success = true, Yorumlar = lstyorum });

        }
    }
}