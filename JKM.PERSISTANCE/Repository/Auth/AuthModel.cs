using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.PERSISTENCE.Repository.Auth
{
    public class AuthModel
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdDetalleUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FechaNacimiento { get; set; }
        public void Login(string username, string password)
        {
            Username = username;
            Password = password;
        }
        public void Register(string username, string password, string nombre, string apellido, string fechaNacimiento)
        {
            Username = username;
            Password = password;
            Nombre = nombre;
            Apellido = apellido;
            FechaNacimiento = fechaNacimiento;
        }
    }
}
