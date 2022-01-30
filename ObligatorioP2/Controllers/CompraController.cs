using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Obligatorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObligatorioP2.Controllers
{
    public class CompraController : Controller
    {
        Sistema s = Sistema.GetInstancia();
        public IActionResult Index()
        {
            return View();
        }

        //Realizar Compra - Usuario logueado
        public IActionResult RealizarCompra(int id)
        {
            if (HttpContext.Session.GetString("datosUsuario") != null && HttpContext.Session.GetString("usuarioRol") == "Usuario" && id != 0)
            {
                Actividad actividadComprada = s.GetActividades(id);
                ViewBag.actividad = actividadComprada;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
          
        }


        [HttpPost]
        public IActionResult RealizarCompra(int id, int cEntradas)
        {
            Actividad actividadComprada = s.GetActividades(id);
            Usuario usu = s.UsuarioLogueado(HttpContext.Session.GetString("nombreUsuario"));
            ViewBag.actividad = actividadComprada;
            Compra compraRealizada = s.AltaCompra(actividadComprada, cEntradas, usu, DateTime.Now, true);
            if (compraRealizada != null)
            {
                ViewBag.msg = "Compra realizada.";
            }
            else
            {
                ViewBag.msg = "Compra inválida.";
            }
            
            return View();
        }

        //Ver Compras de Usuario 
        public IActionResult VerCompras()
        {
            if (HttpContext.Session.GetString("datosUsuario") != null && HttpContext.Session.GetString("usuarioRol") == "Usuario") 
            {
                List<Compra> comprasRealizadas = s.VerComprasUsuario(HttpContext.Session.GetString("nombreUsuario"));
                if (comprasRealizadas.Count>0)
                {
                    return View(comprasRealizadas);
                }
                else
                {
                    ViewBag.msg = "No tiene ninguna compra realizada para mostrar.";
                    return View();
                }
                
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Cancelar compra de Usuario
        public IActionResult CancelarCompra(int id)
        {
            if (HttpContext.Session.GetString("datosUsuario") != null && HttpContext.Session.GetString("usuarioRol") == "Usuario" && id!=0)
            {
                ViewBag.Compra = s.GetCompraBuscarCancelar(id);
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]

        public IActionResult CancelarCompra(int id, bool confirmar)
        {
            Compra comp = s.GetCompraCancelar(id);
            if (comp != null)
            {
                return RedirectToAction("VerCompras", "Compra");
            }
            else
            {
                ViewBag.msg = "Únicamente puede cancelar 24 horas antes de que se realice la actividad.";
                return View();
            }
            
        }



        //Visualizar Compras entre Dos Fechas - Admin
        public IActionResult BuscarComp()
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

        [HttpPost]
        public IActionResult BuscarComp(DateTime fechaInic, DateTime fechaFinal)
        {
            if (fechaInic <= fechaFinal && fechaFinal <= DateTime.Now)
            {
                List<Compra> comprasEncontradas = s.GetVisualizarComprasFecha(fechaInic, fechaFinal);
                if (comprasEncontradas.Count > 0)
                {
                    ViewBag.Enc = comprasEncontradas;
                }
                else
                {
                    ViewBag.Msg = "No hay resultados encontrados en esa fecha.";
                }
            }
            else
            {
                ViewBag.Enc = null;
                ViewBag.Msg = "Complete todos los campos para la búsqueda donde la fecha inicial sea menor que la fecha final y menor que la fecha actual.";
            }
            return View();
        }

        //Estadisticas - Compras de mayor valor - Admin
        public IActionResult ObtenerMayorCompra()
        {
            if (HttpContext.Session.GetString("datosUsuario") != null && HttpContext.Session.GetString("usuarioRol") == "Admin") //limito que no puedan acceder al sistema sin loguearse
            {

                return View(s.GetMayorCompra());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
