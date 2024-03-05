using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;


namespace Spedizioni.Controllers
{
    [Authorize]
    public class SpedizioniController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult SpedizioniOggi()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult SpedizioniInCorso()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult SpedizioniPerCitta()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Spedizioni"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<Spedizioni.Models.Spedizioni> spedizioni = new List<Spedizioni.Models.Spedizioni>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM Spedizioni";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Spedizioni.Models.Spedizioni s = new Spedizioni.Models.Spedizioni();
                    s.IdSpedizione = Convert.ToInt32(dr["IdSpedizione"]);
                    s.IdCliente = Convert.ToInt32(dr["FK_IdCliente"]);
                    s.codTracciamento = dr["codTracciamento"].ToString();
                    s.dataSpedizione = Convert.ToDateTime(dr["dataSpedizione"]);
                    s.pesoSpedizione = Convert.ToDecimal(dr["pesoSpedizione"]);
                    s.cittaDestinazione = dr["cittaDestinazione"].ToString();
                    s.indirizzoDestinazione = dr["indirizzoDestinazione"].ToString();
                    s.nominativoDestinatario = dr["nominativoDestinatario"].ToString();
                    s.costoSpedizione = Convert.ToDecimal(dr["costoSpedizione"]);
                    s.dataConsegna = Convert.ToDateTime(dr["dataConsegna"]);
                    spedizioni.Add(s);
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
            return View(spedizioni);
        }

        public ActionResult InserisciSpedizione()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InserisciSpedizione(Spedizioni.Models.Spedizioni spedizione)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View(spedizione);
        }
    }
}