using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalCoderHouse.Models
{
    public class Producto
    {
        public long Id { get; set; }
        public int Stock { get; set; }
        public long IdUsuario { get; set; }

        public string Descripcion { get; set; }
        public float Costo { get; set; }
        public float PrecioVenta { get; set; }

        public Producto()
        {
            Id = 0;
            Descripcion = "";
            Costo = 0;
            PrecioVenta = 0;
            Stock = 0;
            IdUsuario = 0;
        }
        public Producto(long id, string descripciones, float costo, float precioVenta, int stock, int idUsuario)
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
