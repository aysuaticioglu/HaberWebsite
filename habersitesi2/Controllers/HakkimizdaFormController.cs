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
    public class HakkimizdaFormController : Controller
    {
        SqlConnection cn = new SqlConnection("Server=.;Database=haber;Trusted_Connection=True;");
        public ActionResult Index()
        {
            cn.Open();

            SqlDataAdapter da = new SqlDataAdapter("select * from t_hakkimizda", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            List<Hakkimizda> lst = new List<Hakkimizda>();
            foreach (DataRow rw in dt.Rows)
            {
                Hakkimizda hak = new Hakkimizda();
                hak.id = Convert.ToInt32(rw["id"]);
                hak.icerik = rw["icerik"].ToString();
                lst.Add(hak);

            }
            ViewBag.hak = lst;
            return View();
        }


        public ActionResult Guncelle(Hakkimizda yazi)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update t_hakkimizda set icerik=@icerik where id=1";
            cmd.Connection = cn;


            cmd.Parameters.AddWithValue("@icerik", yazi.icerik);
            cmd.ExecuteNonQuery();
            cn.Close();
            return RedirectToAction("Index", "HakkimizdaForm");
        }
    }
}