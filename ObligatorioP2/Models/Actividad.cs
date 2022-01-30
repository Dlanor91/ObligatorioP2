using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Obligatorio
{
    public class Actividad:IComparable<Actividad>
    {
        private static int ultimoID = 1;

        public int ID { get; set; }

        public string Nombre { get; set; }

        public Categoria CategoriaTipo { get; set; }

        public DateTime FechaHora { get; set; }

        public Lugares DatosLugar { get; set; }

        public Edades EdadMinima { get ;  set; }

        public static double PrecioBase = 1000;

        public int MeGusta { get; set; }
      
        public enum Edades //enum de edades
        {
            P,
            C13,
            C16,
            C18
        }

        public Actividad(string nombre, Categoria categoriaTipo, DateTime fechaHora, Lugares datosLugar, Edades edadMinima, int meGusta)
        {
            ID = ultimoID;
            ultimoID++;
            Nombre = nombre;
            CategoriaTipo = categoriaTipo;
            FechaHora = fechaHora;
            DatosLugar = datosLugar;
            EdadMinima = edadMinima;            
            MeGusta = meGusta;            
        }

        public double PrecioFinal()
        {
            double precioCalculado = 0;

            if (DatosLugar is Abierto)
            {
                precioCalculado= PrecioBase*1.1;
              
            }

            if (DatosLugar is Cerrado)
            {
                if (Cerrado.PorcAforoMaxPermitido<50)
                {
                    precioCalculado = PrecioBase * 1.3;
                }else if (Cerrado.PorcAforoMaxPermitido >= 50 && Cerrado.PorcAforoMaxPermitido <= 75) 
                {
                    precioCalculado = PrecioBase * 1.15;
                }
               
                
            }
            return precioCalculado;
        }

        public override string ToString()
        {
            return $"Nombre: {Nombre} || Categoria: {CategoriaTipo.Nombre} || Lugar: {DatosLugar.Nombre} || Fecha y Hora {FechaHora} ||Edades: {EdadMinima} || Me gusta: {MeGusta}";
        }

        public int CompareTo([AllowNull] Actividad other)
        {
            {
                if (this.FechaHora.CompareTo(other.FechaHora) > 0)
                {
                    return 1;
                }
                else if (this.FechaHora.CompareTo(other.FechaHora) < 0)
                {
                    return -1;
                }
                else
                {
                    if (this.Nombre.CompareTo(other.Nombre) > 0)
                    {
                        return 1;
                    }
                    else if (this.Nombre.CompareTo(other.Nombre) < 0)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
    }
}
