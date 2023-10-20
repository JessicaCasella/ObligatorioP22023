using Dominio.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Actividad : IComparable<Actividad>, IValidable

    {
        protected int _id;
        protected static int s_ultimoNumero = 1;
        protected string _nombre;
        protected string _descripcion;
        protected DateTime _fecha;
        protected int _cantidadMaximaDePersonas;
        protected int _edadMinima;
        protected double _costo;
        protected int _personasAgendadas;

        public Actividad(string nombre, string descripcion, DateTime fecha, int cantidadMaxima, int edadMinima, double costo)
        {
            this._id = s_ultimoNumero;
            s_ultimoNumero++;
            this._nombre = nombre;
            this._descripcion = descripcion;
            this._fecha = fecha;
            this._cantidadMaximaDePersonas = cantidadMaxima;
            this._edadMinima = edadMinima;
            this._costo = costo;
            this._personasAgendadas = 0;
        }

        public int Id
        {
            get { return _id; }
        }

        public int PersonasAgendadas
        {
            get { return _personasAgendadas; }
        }

        public DateTime Fecha
        {
            get { return _fecha; }
        }

        public double Costo
        {
            get { return _costo; }
        }

        public string Nombre
        {
            get { return _nombre; }
        }

        public int EdadMinima
        {
            get { return _edadMinima; }
        }

        public int CantidadMaxima
        {
            get { return _cantidadMaximaDePersonas; }
        }
        public override bool Equals(object obj)
        {
            Actividad a = obj as Actividad;
            return a != null && this._id.Equals(a._id);
        }

        private void ValidarNombre()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("EL NOMBRE NO PUEDE ESTAR VACIO");
            if (_nombre.Length > 25) throw new Exception("EL NOMBRE NO PUEDE TENER MAS DE 25 CARACTERES");
        }

        private void ValidarDescripcion()
        {
            if (string.IsNullOrEmpty(_descripcion)) throw new Exception("LA DESCRIPCION NO PUEDE ESTAR VACIA");
        }

        public virtual void Validar()
        {
            ValidarNombre();
            ValidarDescripcion();
            
        }
        public abstract string MostrarDato();
        public abstract double CalcularCostoFinal(NivelDeFidelizacion nivel);
        

        public int CompareTo(Actividad other)
        {
            return this._costo.CompareTo(other._costo) * -1;
        }

        public void AgregarPersonaEnActividad()
        {
            _personasAgendadas++;
        }
    }
}

