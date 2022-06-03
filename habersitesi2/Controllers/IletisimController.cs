using habersitesi2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace habersitesi2.Controllers
{
    public class IletisimController : Controller
    {
        SqlConnection cn = new SqlConnection("Server=.;Database=haber;Trusted_Connection=True;");

        // GET: Iletisim
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult FormGonder(Iletisim form)
        {
            cn.Open();


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO [dbo].[t_iletisim]([mail],[konu],[mesaj])VALUES(@mail,@konu,@mesaj)";
            cmd.Connection = cn;

            cmd.Parameters.AddWithValue("@mail", form.mail);
            cmd.Parameters.AddWithValue("@konu", form.konu);
            cmd.Parameters.AddWithValue("@mesaj", form.mesaj);


            cmd.ExecuteNonQuery();

            cn.Close();
            return RedirectToAction("Index");
        }
    }
}