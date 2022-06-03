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
    public class YorumlarController : Controller
    {
        // GET: Yorumlar
        SqlConnection cn = new SqlConnection("Server=.;Database=haber;Trusted_Connection=True;");


        public ActionResult Index()
        {
            return View();
        }
        public ActionResult YorumListele()
        {
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from t_yorum order by tarih desc", cn);
            DataTable dtYorum = new DataTable();
            da.Fill(dtYorum);
            List<Yorum> lstyorum = new List<Yorum>();
            foreach (DataRow rw in dtYorum.Rows)
            {
                Yorum yorum = new Yorum();
                yorum.id = Convert.ToInt32(rw["id"]);

                yorum.isim = rw["isim"].ToString();
                yorum.yorum = rw["yorum"].ToString();
                yorum.tarih = Convert.ToDateTime(rw["tarih"]).ToString("dd.MM.yyyy HH:mm");
                yorum.haberId = Convert.ToInt32(rw["haberId"]);
                yorum.durum = Convert.ToBoolean(rw["durum"]);

                lstyorum.Add(yorum);

            }
            cn.Close();
            return Json(new { Success = true, Yorumlar = lstyorum });
        }
      
        public ActionResult YorumSil(int id)
        {
            cn.Open();
            SqlDataAdapter da_sil = new SqlDataAdapter("delete from t_yorum where id="+id, cn);
            DataTable dtSil = new DataTable();
            da_sil.Fill(dtSil);
            cn.Close();
            return Json(new { Success = true });
        }
        public ActionResult YorumAktif(int id)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "UPDATE [dbo].[t_yorum] SET durum=@durum WHERE id="+id;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@durum",true);
    
            cmd.ExecuteNonQuery();
            cn.Close();
            return Json(new { Success = true });

        }
        public ActionResult YorumPasif(int id)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "UPDATE [dbo].[t_yorum] SET durum=@durum WHERE id=" + id;
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@durum", false);

            cmd.ExecuteNonQuery();
            cn.Close();
            return Json(new { Success = true });

        }
    }
}