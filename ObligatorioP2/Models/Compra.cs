using System;
using System.Collections.Generic;
using System.Text;

namespace Obligatorio
{
   public class Compra
    {
        private static int ultimoID = 1;//ID autoincremental

        public int ID { get;  set; }

        public Actividad ActComprada { get; set; }

        public int CantEntradas { get; set; }

        public Usuario UsuarioCompra { get; set; }

        public DateTime FechaHora { get; set; }

        public bool EstadoCompra { get; set; }
        
    public Compra(Actividad actComprada, int cantEntradas, Usuario usuarioCompra, DateTime fechaHora, bool estadoCompra)
        {
            ID = ultimoID;
            ultimoID++;
            ActComprada = actComprada;
            CantEntradas = cantEntradas;
            UsuarioCompra = usuarioCompra;
            FechaHora = fechaHora;
            EstadoCompra = estadoCompra;
        }

    public double PrecioFinalCompra()
        {
            double precioFinal = 0;

            if(CantEntradas >= 5)
            {
                precioFinal = (ActComprada.PrecioFinal() * CantEntradas) * 0.95;
            } else
            {
                precioFinal = ActComprada.PrecioFinal() * CantEntradas;
            }

            return precioFinal;
        }    
    }
}
