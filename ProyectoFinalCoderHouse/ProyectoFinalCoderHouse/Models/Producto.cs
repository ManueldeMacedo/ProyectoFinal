using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalCoderHouse.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }

        public string Descripcion { get; set; }
        public decimal Costo { get; set; }
        public decimal PrecioVenta { get; set; }

        public Producto()
        {
            Id = 0;
            Descripcion = "";
            Costo = 0;
            PrecioVenta = 0;
            Stock = 0;
            IdUsuario = 0;
        }
        public Producto(int id, string descripciones, decimal costo, decimal precioVenta, int stock, int idUsuario)
        {
            Id = id;
            Descripcion = descripciones;
            Costo = costo;
            PrecioVenta = precioVenta;
            Stock = stock;
            IdUsuario = idUsuario;
        }
    }
}
