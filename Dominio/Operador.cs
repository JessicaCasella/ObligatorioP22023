using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Operador : Usuario
    {
        DateTime _fechaInicio = DateTime.Today;

        public Operador(string email, string password, string nombre, string apellido) : base(email, password, nombre, apellido)
        {

        }
        public DateTime FechaInicio
        {
            get { return _fechaInicio; }
        }
        public override void Validar()
        {
            base.Validar();
        }
         public override bool Equals(object obj)
        {
            Operador a = obj as Operador;
            return base.Equals(obj);

        }

        public override string GetRol()
        {
            return "administrador";
        }


    }

}
