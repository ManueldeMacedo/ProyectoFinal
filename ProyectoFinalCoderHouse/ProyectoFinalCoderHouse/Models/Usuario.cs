using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalCoderHouse.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Mail { get; set; }

        public Usuario()
        {
            Id = 0;
            Nombre = "";
            Apellido = "";
            NombreUsuario = "";
            Contrasena = "";
            Mail = "";
        }
        public Usuario(int id, string nombre, string apellido, string nombreUsuario, string contrasena, string mail)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            NombreUsuario = nombreUsuario;
            Contrasena = contrasena;
            Mail = mail;
        }

    }
}
