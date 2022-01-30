using System;
using System.Collections.Generic;
using System.Text;

namespace Obligatorio
{
    public class Cerrado : Lugares
    {             
        public int CostoMant { get; set; }
        public bool AccesibilidadTotal { get; set; }
        public static int PorcAforoMaxPermitido = 50;

        public Cerrado(string nombre, int dimensionesM2, int costoMant, bool accesibilidadTotal)
        {
            ID = ultimoId;
            ultimoId++;
            Nombre = nombre;
            DimensionesM2 = dimensionesM2;
            CostoMant = costoMant;
            AccesibilidadTotal = accesibilidadTotal;
            
        }

    }
}
