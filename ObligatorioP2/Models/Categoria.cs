using System;
using System.Collections.Generic;
using System.Text;

namespace Obligatorio
{
    public class Categoria
    {
        private static int ultimoID = 1;//ID autoincremental

        public int ID { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get;  set; }

        public Categoria(string nombre, string descripcion)
        {
            ID = ultimoID;
            ultimoID++;
            Nombre = nombre;
            Descripcion = descripcion;
        }

        public Categoria()//Constructor vacío para poder comprobar que el nombre de la categoría sea único
        {
        }

        public override bool Equals(object nombreCat)
        {
            return nombreCat is Categoria categoria &&
                   Nombre == categoria.Nombre;
        }
    }
}
