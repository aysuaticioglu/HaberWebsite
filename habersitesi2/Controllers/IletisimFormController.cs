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
    public class IletisimFormController : Controller
    {
        SqlConnection cn = new SqlConnection("Server=.;Database=haber;Trusted_Connection=True;");

        // GET: IletisimForm
        public ActionResult Index()
        {
            cn.Open();

            SqlDataAdapter da = new SqlDataAdapter("select * from t_iletisim", cn);
            DataTable dtilet = new DataTable();
            da.Fill(dtilet);

            cn.Close();
            List<Iletisim> lstilet = new List<Iletisim>();
            foreach (DataRow rw in dtilet.Rows)
            {
                Iletisim ilet = new Iletisim();
                ilet.id = Convert.ToInt32(rw["id"]);
                ilet.mail = rw["mail"].ToString();
                ilet.konu = rw["konu"].ToString();
                ilet.mesaj = rw["mesaj"].ToString();
                lstilet.Add(ilet);

            }
            ViewBag.iletisim = lstilet;
            return View();
        }
        public ActionResult Sil(int iletid)
        {
            cn.Open();
            SqlDataAdapter da_sil = new SqlDataAdapter("delete from t_iletisim where id=" + iletid, cn);
            DataTable dtSil = new DataTable();
            da_sil.Fill(dtSil);
            cn.Close();
            return RedirectToAction("Index");
        }
    }
}