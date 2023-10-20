using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class ProveedoresController : Controller
    {
        Hostal hostal = Hostal.Instancia;
        public IActionResult listaProveedores()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "administrador")
            {
                return View("NoAutorizado");
            }

            ViewBag.Proveedores = hostal.Proveedores;
            return View();
        }
       
        public IActionResult ObtenerProveedor(string nombre)
        {

            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "administrador")
            {
                return View("NoAutorizado");
            }

            if (string.IsNullOrEmpty(nombre)) throw new Exception("El nombre no puede estar vacio");
            ViewBag.Proveedor = hostal.ObtenerProveedorPorNombre(nombre);

          return View();
        }

        [HttpPost]
        public IActionResult CambiarPorcentaje(int CantDescuento, string nombreProveedor)
        {

            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "administrador")
            {
                return View("NoAutorizado");
            }


            try
            {
                if (CantDescuento > 100) throw new Exception("El porcentaje debe ser menor a 100");
                if (CantDescuento < 0) throw new Exception("El porcentaje debe ser mayor a 0");
                if (string.IsNullOrEmpty(nombreProveedor)) throw new Exception("No recibí nombre de proveedor");
                Proveedor p = hostal.ObtenerProveedorPorNombre(nombreProveedor);
                hostal.CambiarDescuentoProveedor(p,CantDescuento);
                return RedirectToAction("listaProveedores");

            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                ViewBag.Proveedor = hostal.ObtenerProveedorPorNombre(nombreProveedor);
                return View("ObtenerProveedor");
            }




        }
    }
}
