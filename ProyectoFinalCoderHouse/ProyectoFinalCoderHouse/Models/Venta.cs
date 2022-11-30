using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalCoderHouse.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Comentarios { get; set; }

        public Venta()
        {
            Id = 0;
            Comentarios = "";
            IdUsuario = 0;
        }
        public Venta(int id, string comentarios, int idUsuario)
        {
            Id = id;
            Comentarios = comentarios;
            IdUsuario = idUsuario;
        }
    }
}
