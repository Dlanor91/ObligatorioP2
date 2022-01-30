using System;
using System.Collections.Generic;
using System.Text;

namespace Obligatorio
{
   public class Usuario 
    {
        private static int ultimoID = 1;//ID autoincremental

        public int ID { get; set; }
                
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public DateTime FechaNac { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenna { get; set; }
        public string Rol { get; set; }
        


        public Usuario(string nombre, string apellido, string email, DateTime fechaNac, string nombreUsuario, string contrasenna)
        {
            ID = ultimoID;
            ultimoID++;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            FechaNac = fechaNac;
            NombreUsuario = nombreUsuario;
            Contrasenna = contrasenna;
            Rol = "Usuario";
        }

        public Usuario() //Constructor vacío para poder comprobar que el email sea único
        {
        }       

        //Validacion de correo del usuario
        public override bool Equals(object correo)
        {
            return correo is Usuario usuario &&
            Email == usuario.Email;
        }

      
    }

    
}
