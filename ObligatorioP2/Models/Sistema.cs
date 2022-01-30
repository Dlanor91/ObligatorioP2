using ObligatorioP2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static Obligatorio.Actividad;


namespace Obligatorio
{
    public class Sistema
    {

        public Sistema()
        {
            PrecargaDatos(); //llamamos a la precarga de datos
        }

        private static Sistema instancia = null;

        public static Sistema GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new Sistema();
            }

            return instancia;
        }



        #region Lugares 
        private List<Lugares> lugares = new List<Lugares>(); // para almacenar la lista de lugares abiertos y cerrados
                

        public List<Lugares> GetLugares()
        {
            return lugares;
        } 

        public Abierto AltaAbierto(string nombre, int dimensionesM2) //metodo que permite dar de alta un lugar abierto
        {
            Abierto nuevoLugarAbierto = null;
            if (nombre != "" && dimensionesM2 >0)
            {
                nuevoLugarAbierto = new Abierto(nombre,dimensionesM2);
                lugares.Add(nuevoLugarAbierto);
            }
            return nuevoLugarAbierto;
        }

        public Cerrado AltaCerrado(string nombre, int dimensionesM2, int costoMant, bool accesibilidadTotal) //metodo que permite dar de alta un lugar abierto
        {
            Cerrado nuevoLugarCerrado = null;
            if (nombre != "" && dimensionesM2 > 0 && costoMant>0)
            {
                nuevoLugarCerrado = new Cerrado(nombre, dimensionesM2, costoMant, accesibilidadTotal);
                lugares.Add(nuevoLugarCerrado);
            }

            return nuevoLugarCerrado;

        }
        #endregion

        #region Actividad

        private List<Actividad> actividades = new List<Actividad>();  //lista actividad 

        public List<Actividad> GetActividad()
        {
            actividades.Sort();
            return actividades;
        }

        public Actividad AltaActividad(string nombre,Categoria categoriaTipo, DateTime fechaHora, Lugares datosLugar, Edades edadMinima, int meGusta)
        {
            Actividad nuevaActividad = null;
            if (nombre != "" && fechaHora>DateTime.Now)
            {
                nuevaActividad = new Actividad(nombre, categoriaTipo, fechaHora, datosLugar, edadMinima, meGusta);
                actividades.Add(nuevaActividad);
            }
            return nuevaActividad;
        }

        internal Actividad GetActividades(int id)
        {
            Actividad ret = null;
            foreach (Actividad a in actividades)
            {
                if (a.ID.Equals(id))
                {
                    ret = a;
                }
            }

            return ret;
        }

        //Accion de Me Gusta - Usuario //***FALTA EN EL ASTAH**//
        internal Actividad MeGustaActividad(int id)
        {
            Actividad act = null;
            foreach (Actividad a in actividades)
            {
                if (a.ID.Equals(id))
                {
                    a.MeGusta = a.MeGusta + 1;
                    act = a;
                }
            }
            return act;
        }
        #endregion

        #region Categoria
        private List<Categoria> categorias = new List<Categoria>();

        public List<Categoria> GetCategoria()
        {
            return categorias;
        } 

        public Categoria AltaCategoria(string nombre, string descripcion)
        {
            Categoria nuevaCategoria = null;
            Categoria verificarNombre = new Categoria();

            verificarNombre.Nombre = nombre;

            if (nombre != "")
            {
                if (!categorias.Contains(verificarNombre))
                {
                    nuevaCategoria = new Categoria(nombre, descripcion);
                    categorias.Add(nuevaCategoria);
                }

            }

            return nuevaCategoria;
        }

       


        #endregion

        #region Compra

        private List<Compra> compras = new List<Compra>(); // para almacenar la lista de compra de boletos

        public List<Compra> GetCompras()
        {
            return compras;
        }

        public Compra AltaCompra(Actividad actComprada, int cantEntradas, Usuario usuarioCompra, DateTime fechaHora, bool estadocompra)
        {
            Compra nuevaCompra = null;
            DateTime fechaCompra = DateTime.Now;
            fechaCompra = fechaCompra.AddHours(1);
            if (cantEntradas > 0 && fechaHora < fechaCompra)
            {
                nuevaCompra = new Compra(actComprada, cantEntradas, usuarioCompra, fechaHora, estadocompra);
                compras.Add(nuevaCompra);
            }
            return nuevaCompra;
        }

        internal List<Compra> GetVisualizarComprasFecha(DateTime fechaInic, DateTime fechaFinal)
        {            
            List<Compra> comprasEncontradas = new List<Compra>();
            if (fechaInic<=fechaFinal && fechaFinal<=DateTime.Now) { 
                foreach (Compra c in compras)
                {
                    if (fechaInic <= c.FechaHora && c.FechaHora<=fechaFinal)
                    {
                        comprasEncontradas.Add(c);
                    }
                }
            }
            return comprasEncontradas;
        }

        //Obtener Mayor Compra
        internal List<Compra> GetMayorCompra()
        {
            List<Compra> mayorCompra = new List<Compra>();
            double max = 0;
            foreach (Compra c in compras){
                if (c.PrecioFinalCompra() > max && c.EstadoCompra)
                {
                    mayorCompra.Clear();
                    mayorCompra.Add(c);
                    max = c.PrecioFinalCompra();
                } else if (c.PrecioFinalCompra() == max)
                {
                    mayorCompra.Add(c);
                }
            }

            return mayorCompra;
        }

        internal List<Compra> VerComprasUsuario(string nombreUsuario)
        {
            List<Compra> comp = new List<Compra>();
            foreach (Compra c in compras)
            {
                if (c.UsuarioCompra.NombreUsuario.Equals(nombreUsuario))
                {
                    comp.Add(c);
                }
            }
            return comp;
        }


        internal Compra GetCompraBuscarCancelar(int id)
        {
            Compra comp = null;
            foreach (Compra c in compras)
            {
                if (c.ID.Equals(id))
                {
                    comp = c;
                }
            }
            return comp;
        }

        internal Compra GetCompraCancelar(int id)
        {
            Compra comp = null;
            foreach (Compra c in compras)
            {
                if (c.ID.Equals(id))
                {
                    comp = c;
                    if (c.EstadoCompra && c.FechaHora.AddDays(-1) <= c.FechaHora)
                    {
                        comp.EstadoCompra = false;
                    }
                }
            }
            return comp;
        }

        #endregion

        #region Usuario      

        private List<Usuario> usuarios = new List<Usuario>(); // para almacenar la lista de usuarios

        public List<Usuario> GetUsuario()
        {
            return usuarios;
        }
            

        public Usuario AltaUsuario(string nombre, string apellido, string email, DateTime fechaNac, string nombreUsuario, string contrasenna)
        {
            Usuario nuevoUsuario = null;
            Usuario verificarEmail = new Usuario();
            Usuario verificarNombreUsuario = new Usuario();
            verificarEmail.Email = email;
            verificarNombreUsuario.NombreUsuario = nombreUsuario;
            if (nombre.Length >= 2 &&  nombre != "" && apellido.Length >= 2 && apellido != "" && email!="" && nombreUsuario != "")
            {
                if (VerificarContrasenna(contrasenna))
                {
                    if (!usuarios.Contains(verificarEmail) && !usuarios.Contains(verificarNombreUsuario))
                    {
                    nuevoUsuario = new Usuario(nombre, apellido, email, fechaNac,nombreUsuario,contrasenna);
                    usuarios.Add(nuevoUsuario);
                    }
                }
                

            }
            return nuevoUsuario;
        }


     private bool VerificarContrasenna(string contrasenna)
    {
            bool ret = false;

            if (contrasenna != "" && contrasenna.Length >= 6)
            {

                //validacion mayuscula 65 al 90 ascii
                bool tieneMayuscula = false;
                foreach (char c in contrasenna)
                {
                    int n = (int)c;

                    if (n >= 65 && n <= 90)
                    {
                        tieneMayuscula = true;
                    }
                }


                //validacion numerica 48 al 57 ascii
                bool tieneNumero = false;
                foreach (char c in contrasenna)
                {
                    int n = (int)c;

                    if (n >= 48 && n <= 57)
                    {
                        tieneNumero = true;
                    }
                }


                if(tieneMayuscula && tieneNumero)
                {
                    ret = true;
                }
            }

            return ret;
        }

        //Login de Usuario - Buscar Usuario
        internal Usuario UsuarioLogin(string nombreUsuario, string contrasenna)
        {
            Usuario ret = null;
            foreach (Usuario usu in usuarios)
            {
                if (usu.NombreUsuario.Equals(nombreUsuario) && usu.Contrasenna.Equals(contrasenna))
                {
                    ret = usu;
                }
            }

            return ret;
        }

        //Buscar Usuario logueado para Compra
        internal Usuario UsuarioLogueado(string nombreUsuario)
        {
            Usuario ret = null;
            foreach (Usuario usu in usuarios)
            {
                if (usu.NombreUsuario.Equals(nombreUsuario))
                {
                    ret = usu;
                }
            }

            return ret;
        }

        //Metodo que ordena los usuarios
        public class OrdenUsuarioApellido : IComparer<Usuario>
        {
            public int Compare(Usuario x, Usuario y)
            {
                if (x.Apellido != y.Apellido)
                {
                    return x.Apellido.CompareTo(y.Apellido);
               }
               else
               {
                 return x.Nombre.CompareTo(y.Nombre);
               }
            }
        }

        public void GetUsuariosOrdenados()
        {            
            usuarios.Sort(new OrdenUsuarioApellido());
        }
        #endregion

        #region Estadisticas - Admin
        internal List<Actividad> GetActividadLugar(string lugar)
        {
            List<Actividad> actEncontradas = new List<Actividad>();
            if (lugar!="")
            {
                foreach(Actividad act in actividades)
                {
                    if (act.DatosLugar.Nombre == lugar)
                    {
                        actEncontradas.Add(act);
                    }
                }
            }
            return actEncontradas;
        }

        internal List<Actividad> GetListarActRangoFechas(DateTime fechaInic, DateTime fechaFin, string categoria)
        {
            List<Actividad> actEncontradas = new List<Actividad>();
            if (fechaInic<=fechaFin && categoria !="")
            {
                foreach (Actividad act in actividades)
                {
                    if (fechaInic<act.FechaHora && act.FechaHora < fechaFin && act.CategoriaTipo.Nombre == categoria)
                    {
                        actEncontradas.Add(act);
                    }
                }
            }
            return actEncontradas;
        }
        #endregion
        #region Instancias
        public void PrecargaDatos() //metodo que nos permitira precargar las instancias de nuestra aplicacion
        {
            //Instancias de Lugares Abiertos y Cerrados
            Abierto a1 = AltaAbierto("Teatro de Verano", 200);
            Abierto a2 = AltaAbierto("Museo Batalla de las Piedras", 250);
            Abierto a3 = AltaAbierto("Museo San Gregorio de Polanco", 150);
            Abierto a4 = AltaAbierto("Museo Mineria a Cielo Abierto", 300);
            Abierto a5 = AltaAbierto("Patio Norte del Espacio de Arte Contemporáneo", 350);

            Cerrado c1 = AltaCerrado("Movie Portones", 250, 350, false);
            Cerrado c2 = AltaCerrado("Movie Montevideo Shopping", 350, 500, true);
            Cerrado c3 = AltaCerrado("Teatro Solís", 400, 900, false);
            Cerrado c4 = AltaCerrado("Teatro El Tinglado", 350, 200, false);
            Cerrado c5 = AltaCerrado("Museo de las Migraciones", 200, 650, true);

            //Instancias para probar de categoria
            Categoria cat1 = AltaCategoria("Cine", "De Peliculas");
            Categoria cat2 = AltaCategoria("Espectáculos", "En vivo");
            Categoria cat3 = AltaCategoria("Dramaturgía", "Representar Historias");
            Categoria cat4 = AltaCategoria("Recorrida", "Visitas Guiada y Libres");

            //Instancias para  de actividad
            Actividad act1 = AltaActividad("Espectáculo Nocturno", cat2, DateTime.Now.AddDays(14), a1, Edades.C18, 32);
            Actividad act2 = AltaActividad("Visita Guiada", cat4, DateTime.Now.AddDays(9), a2, Edades.P, 40);
            Actividad act3 = AltaActividad("Show de Magia", cat2, DateTime.Now.AddDays(30), a3, Edades.P, 47);
            Actividad act4 = AltaActividad("Visita Libre", cat4, DateTime.Now.AddDays(14),c4,Edades.C18, 60);
            Actividad act5 = AltaActividad("Obra: Amor de mis amores", cat3, DateTime.Now.AddDays(1), a1, Edades.C16, 45);
            Actividad act6 = AltaActividad("Espectáculo Cultural", cat2, DateTime.Now.AddDays(22),c5,Edades.C18,72);
            Actividad act7 = AltaActividad("Obra: Valor Facial", cat3, DateTime.Now.AddDays(20),c4,Edades.C13, 45);
            Actividad act8 = AltaActividad("Teatro Infantil", cat3, DateTime.Now.AddDays(8),c3, Edades.P, 28);
            Actividad act9 = AltaActividad("La ley de los audaces", cat1, DateTime.Now.AddDays(12), c2, Edades.C18, 5);
            Actividad act10 = AltaActividad("Rápidos y furiosos 9", cat1, DateTime.Now.AddDays(10),c1, Edades.C13, 145);
            Actividad act11 = AltaActividad("Rápidos y furiosos 6", cat1, DateTime.Now.AddDays(1), c1, Edades.C13, 145);

            //Instancias de Usuario
            DateTime d1 = new DateTime(1998, 03, 23);
            Usuario u1 = AltaUsuario("Natalia", "Didac", "naty_didac@hotmail.com", d1, "natyUwU", "Uwu123");
            DateTime d2 = new DateTime(1990, 05, 25);
            Usuario u2 = AltaUsuario("Ronald", "Lima", "rl8506@gmail.com", d2,"Rony","Rl8506");
            DateTime d3 = new DateTime(1995, 07, 12);
            Usuario u3 = AltaUsuario("Mauricio", "Fernandez", "mfernandez@giberol.com.uy", d3, "Mauri", "Mfer123");
            DateTime d4 = new DateTime(1996, 06, 20);
            Usuario u4 = AltaUsuario("Sayu", "Yagami", "kira@icloud.com", d4, "sayuYaga", "Password13");
            DateTime d5 = new DateTime(1998, 01, 30);
            Usuario u5 = AltaUsuario("Gojo", "Satoru", "jjk@yahoo.com", d5, "usuGojo", "Password12");
            DateTime d6 = new DateTime(1997, 10, 25);
            Usuario u6 = AltaUsuario("Light", "Yagami", "light@icloud.com", d6, "usuLight", "Password123");
            DateTime d7 = new DateTime(1992, 01, 20);
            Usuario u7 = AltaUsuario("Pedro", "Armando", "pedrot@icloud.com", d7, "Pedro", "Pedro123");
            DateTime d8 = new DateTime(1995, 03, 17);
            Usuario u8 = AltaUsuario("Maria", "Garcia", "maria@icloud.com", d8, "Maria", "Contra123");
            DateTime d9 = new DateTime(1998, 06, 30);
            Usuario u9 = AltaUsuario("Juan", "Lay", "juan@mail.com", d9, "Juan", "Contra123");
            DateTime d10 = new DateTime(1981, 09, 28);
            Usuario u10 = AltaUsuario("Gimena", "Cruz", "gime@aol.com", d10, "Gimena", "Contra123");
            DateTime d11 = new DateTime(1985, 10, 29);
            Usuario u11 = AltaUsuario("Armando", "Morejon", "arm@yahoo.com", d11, "Armando", "Contra123");
            u1.Rol = "Admin";
            u2.Rol = "Admin";
            u3.Rol = "Admin";

            //Instacias para compra
            Compra Co1 = AltaCompra(act1, 5, u4, DateTime.Parse("2021-08-28, 12:45"), true);
            Compra Co2 = AltaCompra(act2, 2, u5, DateTime.Parse("2021-09-12, 13:50"), true);
            Compra Co3 = AltaCompra(act1, 3, u6, DateTime.Parse("2021-09-25, 11:00"), true);
            Compra Co4 = AltaCompra(act3, 1, u4, DateTime.Parse("2021-11-05, 14:15"), true);
            Compra Co5 = AltaCompra(act4, 2, u7, DateTime.Parse("2021-10-13, 14:30"), true);
            Compra Co6 = AltaCompra(act9, 1, u4, DateTime.Parse("2021-10-20, 18:30"), true);
            Compra Co7 = AltaCompra(act10, 3, u5, DateTime.Parse("2021-11-11, 19:30"), true);
            Compra Co8 = AltaCompra(act8, 4, u6, DateTime.Parse("2021-11-04, 08:30"), true);
            Compra Co9 = AltaCompra(act10, 4, u6, DateTime.Parse("2021-11-04, 08:30"), false);
            Compra Co10 = AltaCompra(act11, 4, u6, DateTime.Parse("2021-11-05, 08:30"), true);
            Compra Co11 = AltaCompra(act2, 2, u8, DateTime.Parse("2021-10-05, 09:30"), true);
            Compra C012 = AltaCompra(act6, 3, u8, DateTime.Parse("2021-05-04, 10:32"), false);
            Compra Co13 = AltaCompra(act7, 5, u8, DateTime.Parse("2021-11-12, 08:14"), true);
            Compra Co14 = AltaCompra(act10, 6, u9, DateTime.Parse("2021-11-10, 12:30"), true);
            Compra Co15 = AltaCompra(act4, 1, u9, DateTime.Parse("2021-11-03, 19:24"), false);
            Compra Co16 = AltaCompra(act3, 2, u9, DateTime.Parse("2021-11-01, 06:28"), true);
            Compra Co17 = AltaCompra(act8, 3, u10, DateTime.Parse("2021-11-16, 22:51"), true);
            Compra Co18 = AltaCompra(act1, 3, u10, DateTime.Parse("2021-11-13, 08:46"), false);
            Compra Co19 = AltaCompra(act2, 7, u10, DateTime.Parse("2021-11-13, 17:30"), true);
        }

        #endregion

        #region Metodos

        //****Mostrar Nuevo Aforo****//
        public int GetNuevoAforo()
        {
            return Cerrado.PorcAforoMaxPermitido;
        }
        //****Fin Mostrar Nuevo Aforo****//

        //****Mostrar Nuevo Precio Butaca****//
        public double GetNuevoPrecioButaca()
        {
            return Abierto.PrecioButacas;
        }
        //****Fin Mostrar Nuevo Precio Butaca****//

        //****Cambiar Precio Butaca****//
        public bool CambiarPrecioButaca(double nuevoPrecio)
        {
            bool cambioRealizado = false;

            if (nuevoPrecio > 0)
            {
                Abierto.PrecioButacas = nuevoPrecio;
                cambioRealizado = true;
            }

            return cambioRealizado;
        }

        //**** Fin Cambiar Precio Butaca****//

        //****Listar actividades para cualquier edad****//
        public List<Actividad> ObtenerEspectaculosTodoPublico()
        {
            List<Actividad> listaAptoP= new List<Actividad>();
            
            foreach(Actividad act in actividades)
            {
                if (act.EdadMinima == 0)//comparo contra p que se encuentra en la posicion 0
                {
                    listaAptoP.Add(act);
                }
            }

            return listaAptoP;
        }
        //****Fin Listar actividades para cualquier edad****//

        //****Cambiar Porciento de Aforo****//
        public bool CambiarAforo(int nuevoAforo)
        {
            bool AforoNuevo = false;
            if (nuevoAforo != Cerrado.PorcAforoMaxPermitido && nuevoAforo > 0 && nuevoAforo <= 100)
            {
                Cerrado.PorcAforoMaxPermitido = nuevoAforo;
                AforoNuevo = true;
            }

            return AforoNuevo;

        }
        //****Fin Cambiar Porciento de Aforo****//

        // ******** Listar Actividades *********** //

        public List<Actividad> ListarActividades(string categoriaBuscar, DateTime fechaInicio, DateTime fechaFinal)
        {

            List<Actividad> listaFiltrada = new List<Actividad>();

            foreach (Actividad act in actividades)
            {
                if (act.CategoriaTipo.Nombre == categoriaBuscar && fechaInicio < act.FechaHora && fechaFinal > act.FechaHora)
                {

                    listaFiltrada.Add(act);
                }
            }
            return listaFiltrada;
        }
        // ******** Fin Listar Actividades *********** //
        #endregion
    }
}
