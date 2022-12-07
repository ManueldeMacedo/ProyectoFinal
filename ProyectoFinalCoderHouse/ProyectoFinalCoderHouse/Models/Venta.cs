using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalCoderHouse.Models
{
    public class Venta
    {
        public long Id { get; set; }
        public long IdUsuario { get; set; }
        public string Comentarios { get; set; }

        List<ProductoVendido>productosVendidos { get; set; }

        public Venta()
        {
            Id = 0;
            Comentarios = "";
            IdUsuario = 0;
        }
        public Venta(long id, string comentarios, long idUsuario)
        {
            Id = id;
            Comentarios = comentarios;
            IdUsuario = idUsuario;
        }
    }
}
