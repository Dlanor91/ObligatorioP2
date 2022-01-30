using System;
using System.Collections.Generic;
using System.Text;

namespace Obligatorio
{
     public class Abierto : Lugares
    {
        public static double PrecioButacas = 200;
        public Abierto(string nombre, int dimensionesM2)
        {
            ID = ultimoId;
            ultimoId++;
            Nombre = nombre;
            DimensionesM2 = dimensionesM2;            
        }
               
    }
}
