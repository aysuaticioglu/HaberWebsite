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
    public class GizlilikFormController : Controller
    {
        // GET: GizlilikForm
        SqlConnection cn = new SqlConnection("Server=.;Database=haber;Trusted_Connection=True;");

        public ActionResult Index()
        {
            cn.Open();

            SqlDataAdapter da = new SqlDataAdapter("select * from t_gizlilik", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cn.Close();
            List<Gizlilik> lst = new List<Gizlilik>();
            foreach (DataRow rw in dt.Rows)
            {
                Gizlilik giz = new Gizlilik();
                giz.id = Convert.ToInt32(rw["id"]);
                giz.icerik = rw["icerik"].ToString();
                lst.Add(giz);

            }
            ViewBag.giz = lst;
            return View();
        }
        public ActionResult Guncelle(Gizlilik yazi)
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update t_gizlilik set icerik=@icerik where id=1";
            cmd.Connection = cn;
            cmd.Parameters.AddWithValue("@icerik", yazi.icerik);
            cmd.ExecuteNonQuery();
            cn.Close();
            return RedirectToAction("Index", "GizlilikForm");
        }
    }
}