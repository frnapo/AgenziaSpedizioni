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
            string connectionString = ConfigurationManager.ConnectionStrings["Spedizioni"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<Spedizioni.Models.Spedizioni> spedizioni = new List<Spedizioni.Models.Spedizioni>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM Spedizioni WHERE FK_IdStato = 2";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@data", DateTime.Today);
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

        [Authorize(Roles = "admin")]
        public ActionResult SpedizioniInCorso()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Spedizioni"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<Spedizioni.Models.Spedizioni> spedizioni = new List<Spedizioni.Models.Spedizioni>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM Spedizioni WHERE FK_IdStato = 1 OR FK_IdStato = 2";
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
                    s.IdStatoSpedizione = Convert.ToInt32(dr["FK_IdStato"]);
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

        [Authorize(Roles = "admin")]
        public ActionResult SpedizioniPerCitta()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Spedizioni"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<Spedizioni.Models.Spedizioni> spedizioni = new List<Spedizioni.Models.Spedizioni>();
            try
            {
                conn.Open();
                string query = "SELECT cittaDestinazione, COUNT(*) as NumeroSpedizioni FROM Spedizioni GROUP BY cittaDestinazione";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Spedizioni.Models.Spedizioni s = new Spedizioni.Models.Spedizioni();
                    s.cittaDestinazione = dr["cittaDestinazione"].ToString();
                    s.IdSpedizione = Convert.ToInt32(dr["NumeroSpedizioni"]);
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
                string connectionString = ConfigurationManager.ConnectionStrings["Spedizioni"].ConnectionString.ToString();
                SqlConnection conn = new SqlConnection(connectionString);
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Spedizioni (FK_IdCliente, codTracciamento, dataSpedizione, pesoSpedizione, cittaDestinazione, indirizzoDestinazione, nominativoDestinatario, costoSpedizione, dataConsegna) VALUES (@FK_IdCliente, @codTracciamento, @dataSpedizione, @pesoSpedizione, @cittaDestinazione, @indirizzoDestinazione, @nominativoDestinatario, @costoSpedizione, @dataConsegna)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@FK_IdCliente", spedizione.IdCliente);
                    cmd.Parameters.AddWithValue("@codTracciamento", spedizione.codTracciamento);
                    cmd.Parameters.AddWithValue("@dataSpedizione", spedizione.dataSpedizione);
                    cmd.Parameters.AddWithValue("@pesoSpedizione", spedizione.pesoSpedizione);
                    cmd.Parameters.AddWithValue("@cittaDestinazione", spedizione.cittaDestinazione);
                    cmd.Parameters.AddWithValue("@indirizzoDestinazione", spedizione.indirizzoDestinazione);
                    cmd.Parameters.AddWithValue("@nominativoDestinatario", spedizione.nominativoDestinatario);
                    cmd.Parameters.AddWithValue("@costoSpedizione", spedizione.costoSpedizione);
                    cmd.Parameters.AddWithValue("@dataConsegna", spedizione.dataConsegna);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
                finally
                {
                    conn.Close();
                }
                return RedirectToAction("Index, Home");
            }
            return View(spedizione);
        }

        public ActionResult Consegnato(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Spedizioni"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "UPDATE Spedizioni SET FK_IdStato = 3 WHERE idSpedizione =" + id;
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("SpedizioniInCorso");
        }

        public ActionResult InConsegna(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Spedizioni"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "UPDATE Spedizioni SET FK_IdStato = 2 WHERE idSpedizione =" + id;
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("SpedizioniInCorso");
        }

    }
}

