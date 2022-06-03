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
    public class HakkimizdaController : Controller
    {
        SqlConnection cn = new SqlConnection("Server=.;Database=haber;Trusted_Connection=True;");
        // GET: Hakkimizda
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
    }
}