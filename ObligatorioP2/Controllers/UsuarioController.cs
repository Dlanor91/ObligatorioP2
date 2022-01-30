using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Obligatorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObligatorioP2.Controllers
{
    public class UsuarioController : Controller
    {
        Sistema s = Sistema.GetInstancia();

        //Login - Usuario Anonimo
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("datosUsuario") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Login(string nombreUsuario, string contrasenna)
        {
            Usuario logueado = s.UsuarioLogin(nombreUsuario, contrasenna);
            if (logueado != null)
            {
                string datos = $"{logueado.Nombre} {logueado.Apellido}";
                /* HttpContext.Session.SetString("hola", "hola");*/
                HttpContext.Session.SetString("datosUsuario", datos);
                HttpContext.Session.SetString("usuarioRol", logueado.Rol);
                HttpContext.Session.SetString("nombreUsuario", logueado.NombreUsuario);               

                //en vez de retornar la vista misma, retornar la pagina principal
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "El usuario o contraseña ingresado no están en nuestra base de batos.";
                return View();
            }


        }

        //Registro - Usuario Anonimo
        public IActionResult Registro()
        {
            if (HttpContext.Session.GetString("datosUsuario") == null) {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Registro(string nombre, string apellido, string email, DateTime fechaNac, string nombreUsuario, string contrasenna)
        {
            Usuario usu = s.AltaUsuario(nombre, apellido, email, fechaNac, nombreUsuario, contrasenna);

            if (usu != null)
            {
                ViewBag.Msg = "Usuario registrado Correctamente.";
            }
            else
            {
                ViewBag.Msg = "Error en la validación, o el usuario o el email ya está registrado en el sistema.";
            }
            return View();
        }

        //ListarActividades - Usuario Anonimo - Registrado - Admin
        public IActionResult ListarAct()
        {

            return View(s.GetActividad());
        }


        public IActionResult MeGusta(int id)
        {
            if (HttpContext.Session.GetString("datosUsuario") != null && HttpContext.Session.GetString("usuarioRol") == "Usuario")
            {
                ViewBag.MeGusta = s.MeGustaActividad(id);
                return RedirectToAction("ListarAct", "Usuario");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        

        //ListarUsuarios - Admin
        public IActionResult ListarUsuarios()
        {
            if (HttpContext.Session.GetString("datosUsuario") != null  && HttpContext.Session.GetString("usuarioRol") == "Admin") //limito que no puedan acceder al sistema sin loguearse
            {
                s.GetUsuariosOrdenados();
                return View(s.GetUsuario());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
       

        //Estadisticas - Admin
        public IActionResult Estadisticas()
        {
            if (HttpContext.Session.GetString("datosUsuario") != null && HttpContext.Session.GetString("usuarioRol") == "Admin") //limito que no puedan acceder al sistema sin loguearse
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Estadisticas - Buscar Actividades dado un Lugar - Admin
        public IActionResult ListarActPorNombre()
        {
            if (HttpContext.Session.GetString("datosUsuario") != null && HttpContext.Session.GetString("usuarioRol") == "Admin") //limito que no puedan acceder al sistema sin loguearse
            {
                ViewBag.Lugares = s.GetLugares();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult ListarActPorNombre(string lugar)
        {
            ViewBag.Lugares = s.GetLugares();
            if (lugar != "") {
                List<Actividad> actEnc = s.GetActividadLugar(lugar);
                if (actEnc.Count>0)
                {
                    ViewBag.LugaresEnc = actEnc;
                }
                else
                {
                    ViewBag.Msg = "No hay resultados posibles para esta búsqueda.";
                }
            }
            else
            {
                ViewBag.LugaresEnc = null;
                ViewBag.Msg = "Debe seleccionar un lugar para realizar la búsqueda.";
            }
            return View();
        }

        //Estadisticas - Listar actividades que cumplan con filtro de dos fechas y una categoria - Admin

        public IActionResult ListarActPorFechaCateg()
        {
            if (HttpContext.Session.GetString("datosUsuario") != null && HttpContext.Session.GetString("usuarioRol") == "Admin") //limito que no puedan acceder al sistema sin loguearse
            {
                ViewBag.Categoria = s.GetCategoria();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult ListarActPorFechaCateg(DateTime fechaInic, DateTime fechaFin, string categoria)
        {
            ViewBag.Categoria = s.GetCategoria();
            if(fechaInic<=fechaFin && categoria != "")
            {
                List<Actividad> actEnc = s.GetListarActRangoFechas(fechaInic, fechaFin, categoria);
                if (actEnc.Count > 0)
                {
                    ViewBag.Buscar = actEnc;
                }
                else
                {
                    ViewBag.Msg = "No hay resultados posibles para esta búsqueda.";
                }
                
            }
            else
            {
                ViewBag.Buscar = null;
                ViewBag.Msg = "Debe completar todos los campos de la búsqueda.";
            }
            return View();
            
        }               

        //Cerrar Sesion
        public IActionResult CerrarSesion()
        {
            if (HttpContext.Session.GetString("datosUsuario") != null) //limito que no puedan acceder al sistema sin loguearse
            {
                HttpContext.Session.Clear();
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

    }
}
