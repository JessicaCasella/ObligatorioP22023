using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class ActividadesController : Controller
    {
        Hostal hostal = Hostal.Instancia;
        public IActionResult listaActividades()
        {

            ViewBag.Actividades = hostal.FiltrarActividadesPorFecha(DateTime.Today);
            if (ViewBag.Actividades == null || ViewBag.Actividades.Count == 0) ViewBag.Error = "NO HAY ACTIVIDADES EL DIA DE HOY";
            return View();
        }

        [HttpPost]
        public IActionResult listaFiltradaPorFecha(DateTime fecha)
        {
            if (fecha == DateTime.MinValue)
            {
                
                return RedirectToAction("listaActividades");


            }
            else
            {
                ViewBag.Actividades = hostal.FiltrarActividadesPorFecha(fecha);
                if (ViewBag.Actividades == null || ViewBag.Actividades.Count == 0) ViewBag.Error = "NO HAY ACTIVIDADES EL " + fecha.ToShortDateString();
                return View("listaActividades");
            }


        }

       

    }
}
