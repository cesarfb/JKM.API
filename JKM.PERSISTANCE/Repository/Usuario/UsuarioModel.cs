using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.PERSISTENCE.Repository.Usuario
{
    public class UsuarioModel : DetalleUsuarioModel
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdRol { get; set; }

        public void RegisterUsuario(string nombre = "", string apellido = "", string email = "", DateTime? fechaNacimiento = null, string username = "", string password = "", int idRol = 0)
        {
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            FechaNacimiento = fechaNacimiento;
            Username = username;
            Password = password;
            IdRol = idRol;
        }

        public void UpdateUsuario(int idUsuario = 0, string nombre = "", string apellido = "", string email = "", DateTime? fechaNacimiento = null, string username = "", string password = "", int idRol = 0)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            FechaNacimiento = fechaNacimiento;
            Username = username;
            Password = password;
            IdRol = idRol;
        }
    }

    public class DetalleUsuarioModel
    {
        public int IdDetalleUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public DateTime? FechaNacimiento { get; set; }
    }
}
