using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafio_uno_clases
{
    public class Producto
    {
        int Id { get; set; }
        int Stock { get; set; }
        int IdUsuario { get; set; }

        string Descripcion { get; set; }
        double Costo { get; set; }
        double PrecioVenta { get; set; }
    }
}
