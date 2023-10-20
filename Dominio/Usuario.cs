using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Dominio.Interfases;

namespace Dominio
{
    public abstract class Usuario : IValidable
    {

        protected string _email;
        protected string _password;
        protected string _nombre;
        protected string _apellido;

        public Usuario(string email, string password, string nombre, string apellido)
        {
            this._email = email;
            this._password = password;
            this._nombre = nombre;
            this._apellido = apellido;

        }

        public string NombreYApellido
        {
            get { return $"{_nombre} {_apellido}"; }
        }

        public string Password
        {
            get { return _password; }
        }
        public string Nombre
        {
            get { return _nombre; }
        }

        public string Email
        {
            get { return _email; }
        }

        private void ValidarPassword()
        {
            if (_password.Length < 8) throw new Exception("LA CONTRASEÑA DEBE CONTENER UN MINIMO DE 8 CARACTERES");
        }

        private void ValidarEmail()
        {
            int arrobaIndex = _email.IndexOf("@");
            int arrobaLastIndex = _email.LastIndexOf("@");


            if (!_email.Contains("@"))
            {
                throw new Exception("El mail no es valido.");
            }
            else
            {
                if (arrobaLastIndex != arrobaIndex) throw new Exception("El mail no es valido.");
                if (arrobaIndex == 0 || arrobaIndex == _email.Length - 1) throw new Exception("El mail no es valido. ");

            }
        }

        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("EL NOMBRE NO PUEDE ESTAR VACÍO.");
            if (string.IsNullOrEmpty(_apellido)) throw new Exception("EL APELLIDO NO PUEDE ESTAR VACÍO.");
        }
        public virtual void Validar()
        {
            ValidarPassword();
            ValidarEmail();
            ValidarNombre();
        }

        public abstract string GetRol();

        public override bool Equals(object obj)
        {
            Usuario a = obj as Usuario;
            return a != null && _email.ToLower().Equals(a._email.ToLower());
        }

    }
}
