using Spedizioni.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Security;

namespace Spedizioni.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Utenti u)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Spedizioni"].ConnectionString.ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Utenti WHERE Username = @Username AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", u.NomeUtente);
                    cmd.Parameters.AddWithValue("@Password", u.Password);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        FormsAuthentication.SetAuthCookie(u.NomeUtente, false);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Username o password errati";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;

                    ;
                }
            return RedirectToAction("Index");

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }




        public ActionResult CheckCodFiscale(string CodFisc)
        {
            string pattern = @"^[A-Z]{6}\d{2}[A-Z]\d{2}[A-Z]\d{3}[A-Z]$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(CodFisc))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CheckPartitaIva(string PartitaIva)
        {
            string pattern = @"^\d{11}$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(PartitaIva))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Tracking()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Tracking(string CodiceFiscale, string codTracciamento)
        {
            ViewBag.search = codTracciamento;
            string connectionString = ConfigurationManager.ConnectionStrings["Spedizioni"].ConnectionString.ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Spedizioni s JOIN Clienti c ON s.FK_idCliente = c.idCliente WHERE (c.codFiscale = @CodiceFiscale OR c.partitaIva = @CodiceFiscale) AND s.codTracciamento = @codTracciamento";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CodiceFiscale", CodiceFiscale);
                    cmd.Parameters.AddWithValue("@codTracciamento", codTracciamento);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        Spedizioni.Models.Spedizioni s = new Spedizioni.Models.Spedizioni();
                        s.IdSpedizione = Convert.ToInt32(dr["IdSpedizione"]);
                        s.codTracciamento = dr["codTracciamento"].ToString();
                        s.dataSpedizione = Convert.ToDateTime(dr["dataSpedizione"]);
                        s.pesoSpedizione = Convert.ToDecimal(dr["pesoSpedizione"]);
                        s.cittaDestinazione = dr["cittaDestinazione"].ToString();
                        s.indirizzoDestinazione = dr["indirizzoDestinazione"].ToString();
                        s.nominativoDestinatario = dr["nominativoDestinatario"].ToString();
                        s.costoSpedizione = Convert.ToDecimal(dr["costoSpedizione"]);
                        s.dataConsegna = Convert.ToDateTime(dr["dataConsegna"]);

                        return View(s);
                    }
                    else
                    {
                        TempData["Message"] = "Nessuna spedizione trovata";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
            }
            return View();
        }

    }
}