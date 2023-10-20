using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class AgendasController : Controller
    {
        Hostal hostal = Hostal.Instancia;

       
        public IActionResult CrearAgenda(int idAct)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "huesped")
            {
                return View("NoAutorizado");

            }

            try
            {
                if (idAct == null) throw new Exception("El id de la actividad no puede ser nulo");

                Actividad a = hostal.ObtenerActividadPorID(idAct);
                if (a == null) throw new Exception("Se necesita una actividad para crear la agenda");
                Usuario u = hostal.ObtenerUsuarioPorMail(HttpContext.Session.GetString("email"));
                if (u == null) throw new Exception("Se necesita un usuario para crear la agenda");
                Huesped huesped = u as Huesped;


                Agenda agenda = new Agenda(huesped, a, DateTime.Today);

             
                hostal.AltaAgenda(agenda);
                ViewBag.Exito = "Agenda creada con exito";

                ViewBag.NombreHuesped = agenda.Huesped.NombreYApellido;
                ViewBag.NombreActividad = agenda.Actividad.Nombre;
                ViewBag.FechaActividad = agenda.Actividad.Fecha;
                ViewBag.CostoActividad = agenda.Actividad.Costo;
                ViewBag.EstadoAgenda = agenda.EstadoDeLaAgenda;
                ViewBag.fechaAgenda = agenda.FechaAgenda;




                return View();

                
               

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
               return RedirectToAction("listaActividades", "Actividades");

            }

        }
      
        public IActionResult listadoDeMisAgendas()
        {
          
                if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "huesped")
                {
                    return View("NoAutorizado");
                }

                Usuario u = hostal.ObtenerUsuarioPorMail(HttpContext.Session.GetString("email"));
                Huesped h = u as Huesped;
               
                ViewBag.Agendas = hostal.ObtenerAgendas(h, DateTime.Today);
                if (ViewBag.Agendas == null || ViewBag.Agendas.Count == 0) ViewBag.Error = "NO TIENES AGENDAS";

          
               
            

            return View();
        }

        
        public IActionResult listaPordDocumento()
        {

            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "administrador")
            {
                return View("NoAutorizado");
            }

            return View("listadoDeMisAgendas");
        }


        [HttpPost]
        public IActionResult AgendasDeUnSoloHuesped(string numero, TipoDocumento tipoDoc)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "administrador")
            {
                return View("NoAutorizado");
            }

            try
            {

                ViewBag.PermitoPorFecha = false;

                if (string.IsNullOrEmpty(numero)) throw new Exception("el numero no puede estar vacio");

                Huesped h = hostal.ObtenerHuespedPorCedula(numero, tipoDoc);
                if (h == null) throw new Exception("el huesped no existe");

                List<Agenda> agendas = hostal.ObtenerAgendas(h);
                if (agendas == null) throw new Exception("el numero no puede estar vacio");

                ViewBag.Agendas = agendas;
                if (ViewBag.Agendas == null || ViewBag.Agendas.Count == 0) ViewBag.Error = "EL HUESPED NO CONTIENE AGENDAS";

                return View("listadoDeMisAgendas");


            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("listadoDeMisAgendas");
            }
        }

        public IActionResult AgendasPorFecha()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "administrador")
            {
                return View("NoAutorizado");
            }

            ViewBag.PermitoPorFecha = true;

            ViewBag.Agendas = hostal.ObtenerAgendasDeUnaFecha(DateTime.Today);
            if (ViewBag.Agendas == null || ViewBag.Agendas.Count == 0) ViewBag.Error = "No hay agendas pendientes para el dia de hoy";
            
            

            return View("listadoDeMisAgendas");
        }


        [HttpPost]
        public IActionResult FiltrarAgendasPorFecha(DateTime fecha)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "administrador")
            {
                return View("NoAutorizado");
            }

            ViewBag.PermitoPorFecha = true;

            try
            {
                if (fecha == DateTime.MinValue) throw new Exception("la fecha no puede ser nula");

                List<Agenda> aux = hostal.ObtenerAgendasDeUnaFecha(fecha);
                ViewBag.Agendas = aux;
                if (ViewBag.Agendas == null || ViewBag.Agendas.Count == 0) ViewBag.Error = "NO TIENES AGENDAS PARA EL DIA " + fecha.ToShortDateString();

            }
            catch(Exception ex)
            {

                ViewBag.Error = ex.Message;
            }

            return View("listadoDeMisAgendas");
        }

        public IActionResult ConfirmarAgenda(int idAct, TipoDocumento tipo, string doc)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "administrador")
            {
                return View("NoAutorizado");
            }

            Huesped huesped = hostal.ObtenerHuespedPorCedula(doc, tipo);
            Agenda agenda = hostal.ObtenerAgendaPorHuespedYActividad(huesped, idAct);
            hostal.CambiarEstadoAgendaAConfirmada(agenda);
            ViewBag.Exito = "Agenda confirmada con exito";

            return View("listadoDeMisAgendas");
        }

        public IActionResult ConfirmarAgenda2(int idAct, TipoDocumento tipo, string doc)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "administrador")
            {
                return View("NoAutorizado");
            }

            Huesped huesped = hostal.ObtenerHuespedPorCedula(doc, tipo);
            Agenda agenda = hostal.ObtenerAgendaPorHuespedYActividad(huesped, idAct);
            hostal.CambiarEstadoAgendaAConfirmada(agenda);
            ViewBag.Exito = "Agenda confirmada con exito";

            ViewBag.PermitoPorFecha = true;

            ViewBag.Agendas = hostal.ObtenerAgendasDeUnaFecha(DateTime.Today);
            if (ViewBag.Agendas == null || ViewBag.Agendas.Count == 0) ViewBag.Error = "No hay agendas pendientes para el dia de hoy";

            return View("listadoDeMisAgendas");
        }
    }

}
