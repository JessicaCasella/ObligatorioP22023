using Microsoft.AspNetCore.Mvc;
using Dominio;
using Microsoft.AspNetCore.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MVC.Controllers
{
    public class UsuariosController : Controller
    {
        Hostal hostal = Hostal.Instancia;

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult procesarlogin(string usuario, string pass)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario) && string.IsNullOrEmpty(pass)) throw new Exception("Usuario o contraseña vacios");
                Usuario u = hostal.Login(usuario, pass);

                HttpContext.Session.SetString("email", u.Email);
                HttpContext.Session.SetString("rol", u.GetRol());

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Login");
            }

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }


        public IActionResult AltaHuesped()
        {
            return View();
        }


        [HttpPost]
        /*public IActionResult registroHuesped(string dirEmail, string pass, TipoDocumento tipo, string numDoc, string nombre, string apellido, string numHabitacion, DateTime fechaNacHuesped)
        {
            try
            {
                if (string.IsNullOrEmpty(dirEmail)) throw new Exception("Debes ingresar el email");
                if (string.IsNullOrEmpty(pass)) throw new Exception("Debes ingresar la contraseña");
                if (string.IsNullOrEmpty(numDoc)) throw new Exception("Debes ingresar el numero de documento");
                if (string.IsNullOrEmpty(nombre)) throw new Exception("Debes ingresar el nombre");
                if (string.IsNullOrEmpty(apellido)) throw new Exception("Debes ingresar el apellido");
                if (string.IsNullOrEmpty(numHabitacion)) throw new Exception("Debes ingresar el numero de habtacion");

                if (fechaNacHuesped == DateTime.MinValue) throw new Exception("Debes ingresar tu fecha de nacimiento");

                Usuario u = new Huesped(dirEmail, pass, nombre, apellido, tipo, numDoc, numHabitacion, fechaNacHuesped);
                hostal.AltaUsuario(u);
                ViewBag.Exito = "Huesped registrado con exito";
                return View("AltaHuesped");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("AltaHuesped");
            }
        }
        */

        public IActionResult registroHuesped(string dirEmail, string pass, TipoDocumento tipo, string numDoc, string nombre, string apellido, string numHabitacion, DateTime fechaNacHuesped)
        {
            try
            {
                if (string.IsNullOrEmpty(dirEmail)) throw new Exception("Debes ingresar el email");
                if (string.IsNullOrEmpty(pass)) throw new Exception("Debes ingresar la contraseña");
                if (string.IsNullOrEmpty(numDoc)) throw new Exception("Debes ingresar el numero de documento");
                if (string.IsNullOrEmpty(nombre)) throw new Exception("Debes ingresar el nombre");
                if (string.IsNullOrEmpty(apellido)) throw new Exception("Debes ingresar el apellido");
                if (string.IsNullOrEmpty(numHabitacion)) throw new Exception("Debes ingresar el numero de habtacion");

                if (fechaNacHuesped == DateTime.MinValue) throw new Exception("Debes ingresar tu fecha de nacimiento");

                Usuario u = new Huesped(dirEmail, pass, nombre, apellido, tipo, numDoc, numHabitacion, fechaNacHuesped);
                hostal.AltaUsuario(u);
                ViewBag.Exito = "Huesped registrado con exito";
                procesarlogin(dirEmail, pass);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("AltaHuesped");
            }
        }
        public IActionResult MostrarDatosUsuario()
        {

            if (HttpContext.Session.GetString("rol") == null )
            {
                return View("NoAutorizado");

            }

            Usuario U = hostal.ObtenerUsuarioPorMail(HttpContext.Session.GetString("email"));

            ViewBag.Usuario = U;



          

               
            return View();
            
        }




    }
}
