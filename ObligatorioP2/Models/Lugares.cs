using System;
using System.Collections.Generic;
using System.Text;

namespace Obligatorio
{
   public abstract class Lugares //es una clase asbtract porque es la clase padre
    {
        protected static int ultimoId = 1;//ID autoincremental protected para que lo hereden los hijos
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int DimensionesM2 { get; set; }
            
        //aqui no se crea el constructor porque no se generan instancias de la misma, sino solo de las clases hijas
        
    }
}
