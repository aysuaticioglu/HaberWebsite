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
    public class GizlilikController : Controller
    {
        // GET: Gizlilik
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
    }
}