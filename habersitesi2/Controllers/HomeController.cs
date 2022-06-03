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
    public class HomeController : Controller
    {
        SqlConnection cn = new SqlConnection("Server=.;Database=haber;Trusted_Connection=True;");
        public ActionResult Index()
        {
            
            cn.Open();
            SqlDataAdapter da_home = new SqlDataAdapter("SELECT t_icerik.id, t_icerik.baslik, t_icerik.altbaslik, t_icerik.fotograf, t_icerik.kategoriId,t_icerik.mansetId, t_icerik.tarih,t_kategori.kategoriAd, t_kategori.sira FROM   t_icerik INNER JOIN t_kategori ON t_icerik.kategoriId = t_kategori.id where  t_icerik.mansetId = 1 order by t_icerik.tarih desc", cn);
            DataTable dtHome = new DataTable();
            da_home.Fill(dtHome);

            cn.Close();

            List<Icerik> lstHome = new List<Icerik>();
            foreach (DataRow rw in dtHome.Rows)
            {
                Icerik home = new Icerik();
                home.id = Convert.ToInt32(rw["id"]);
                home.baslik = rw["baslik"].ToString();
                //home.altbaslik = rw["altbaslik"].ToString();
                //home.icerik = rw["icerik"].ToString();
                home.fotografAdi = rw["fotograf"].ToString();
                home.kategoriId = Convert.ToInt32(rw["kategoriId"]);
                home.mansetId = Convert.ToInt32(rw["mansetId"]);
                home.tarih = Convert.ToDateTime( rw["tarih"]);
                home.kategoriAd= rw["kategoriAd"].ToString();

                lstHome.Add(home);

            }
            ViewBag.Haberler = lstHome;
            

            //-------------Guncel Haberler------------------------//
            cn.Open();
            SqlDataAdapter da_guncel = new SqlDataAdapter("SELECT top 4 t_icerik.id, t_icerik.baslik, t_icerik.altbaslik, t_icerik.fotograf, t_icerik.kategoriId,t_icerik.mansetId, t_icerik.tarih,t_kategori.kategoriAd, t_kategori.sira FROM   t_icerik INNER JOIN t_kategori ON t_icerik.kategoriId = t_kategori.id where  t_icerik.mansetId = 2 order by t_icerik.tarih desc", cn);
            DataTable dtguncel = new DataTable();
            da_guncel.Fill(dtguncel);
            cn.Close();

            List<Icerik> lstGuncel = new List<Icerik>();
            foreach (DataRow rw in dtguncel.Rows)
            {
                Icerik guncel = new Icerik();
                guncel.id = Convert.ToInt32(rw["id"]);
                guncel.baslik = rw["baslik"].ToString();
                //home.altbaslik = rw["altbaslik"].ToString();
                //home.icerik = rw["icerik"].ToString();
                guncel.fotografAdi = rw["fotograf"].ToString();
                guncel.kategoriId = Convert.ToInt32(rw["kategoriId"]);
                guncel.mansetId = Convert.ToInt32(rw["mansetId"]);
                guncel.tarih = Convert.ToDateTime( rw["tarih"]);
                guncel.kategoriAd = rw["kategoriAd"].ToString();
                lstGuncel.Add(guncel);

            }
            ViewBag.Guncel = lstGuncel;

            //-------------Ekonomi---------------------------------//
            cn.Open();
            SqlDataAdapter da_ekonomi = new SqlDataAdapter("select top 4 * from t_icerik where kategoriId=2 and mansetId=3 order by tarih desc", cn);
            DataTable dtekonomi = new DataTable();
            da_ekonomi.Fill(dtekonomi);
            cn.Close();

            List<Icerik> lstEkonomi = new List<Icerik>();
            foreach (DataRow rw in dtekonomi.Rows)
            {
                Icerik ekonomi = new Icerik();
                ekonomi.id = Convert.ToInt32(rw["id"]);
                ekonomi.baslik = rw["baslik"].ToString();
                //home.altbaslik = rw["altbaslik"].ToString();
                //home.icerik = rw["icerik"].ToString();
                ekonomi.fotografAdi = rw["fotograf"].ToString();
                ekonomi.kategoriId = Convert.ToInt32(rw["kategoriId"]);
                ekonomi.mansetId = Convert.ToInt32(rw["mansetId"]);
                ekonomi.tarih = Convert.ToDateTime(rw["tarih"]);

                lstEkonomi.Add(ekonomi);

            }
            ViewBag.Ekonomi = lstEkonomi;
            //-------------Spor---------------------------------//
            cn.Open();
            SqlDataAdapter da_spor = new SqlDataAdapter("select top 4 * from t_icerik where kategoriId=3 and mansetId=3 order by tarih desc", cn);
            DataTable dtspor = new DataTable();
            da_spor.Fill(dtspor);
            cn.Close();
            List<Icerik> lstSpor = new List<Icerik>();
            foreach (DataRow rw in dtspor.Rows)
            {
                Icerik spor = new Icerik();
                spor.id = Convert.ToInt32(rw["id"]);
                spor.baslik = rw["baslik"].ToString();
                //home.altbaslik = rw["altbaslik"].ToString();
                //home.icerik = rw["icerik"].ToString();
                spor.fotografAdi = rw["fotograf"].ToString();
                spor.kategoriId = Convert.ToInt32(rw["kategoriId"]);
                spor.mansetId = Convert.ToInt32(rw["mansetId"]);
                spor.tarih = Convert.ToDateTime( rw["tarih"]);

                lstSpor.Add(spor);

            }
            ViewBag.Spor = lstSpor;
            //-------------Kültür Sanat---------------------------------//
            cn.Open();
            SqlDataAdapter da_kultur = new SqlDataAdapter("select top 4 * from t_icerik where kategoriId=4 and mansetId=3 order by tarih desc", cn);
            DataTable dtkultur = new DataTable();
            da_kultur.Fill(dtkultur);
            cn.Close();
            List<Icerik> lstKultur = new List<Icerik>();
            foreach (DataRow rw in dtkultur.Rows)
            {
                Icerik kultur = new Icerik();
                kultur.id = Convert.ToInt32(rw["id"]);
                kultur.baslik = rw["baslik"].ToString();
                //home.altbaslik = rw["altbaslik"].ToString();
                //home.icerik = rw["icerik"].ToString();
                kultur.fotografAdi = rw["fotograf"].ToString();
                kultur.kategoriId = Convert.ToInt32(rw["kategoriId"]);
                kultur.mansetId = Convert.ToInt32(rw["mansetId"]);
                kultur.tarih = Convert.ToDateTime( rw["tarih"]);

                lstKultur.Add(kultur);

            }
            ViewBag.Kultur = lstKultur;
            //------------Dünya---------------------------------//

            cn.Open();
            SqlDataAdapter da_dunya = new SqlDataAdapter("select top 4 * from t_icerik where kategoriId=1 and mansetId=3 order by tarih desc", cn);
            DataTable dtdunya = new DataTable();
            da_dunya.Fill(dtdunya);
            cn.Close();
            List<Icerik> lstDunya = new List<Icerik>();
            foreach (DataRow rw in dtdunya.Rows)
            {
                Icerik dunya = new Icerik();
                dunya.id = Convert.ToInt32(rw["id"]);
                dunya.baslik = rw["baslik"].ToString();
                //home.altbaslik = rw["altbaslik"].ToString();
                //home.icerik = rw["icerik"].ToString();
                dunya.fotografAdi = rw["fotograf"].ToString();
                dunya.kategoriId = Convert.ToInt32(rw["kategoriId"]);
                dunya.mansetId = Convert.ToInt32(rw["mansetId"]);
                dunya.tarih = Convert.ToDateTime(rw["tarih"]);

                lstDunya.Add(dunya);

            }
            ViewBag.Dunya = lstDunya;


            return View();
        }

    }
}