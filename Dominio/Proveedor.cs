using Dominio.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
    public class Proveedor : IComparable<Proveedor>, IValidable
    {
        private string _nombre;
        private string _telefono;
        private string _direccion;
        private double _descuento;

        public Proveedor(string nombre, string telefono, string direccion, double descuento)
        {
            this._nombre = nombre;
            this._telefono = telefono;
            this._direccion = direccion;
            this._descuento = descuento;
        }

        public string Nombre
        {
            get { return _nombre; }
        }

        public string Telefono
        {
            get { return _telefono; }
        }

        public string Direccion
        {
            get { return _direccion; }
        }
        public double Descuento
        {
            get { return _descuento; }
        }

        public override bool Equals(object obj)
        {
            Proveedor p = obj as Proveedor;
            return p != null && _nombre.Equals(p._nombre);
        }


        private void ValidarNombreVacio()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("EL NOMBRE NO PUEDE ESTAR VACÍO.");
        }

        private void ValidarTelefonoVacio()
        {
            if (string.IsNullOrEmpty(_telefono)) throw new Exception("EL TELÉFONO NO PUEDE ESTAR VACÍO.");
        }

        private void ValidarDireccionVacia()
        {
            if (string.IsNullOrEmpty(_direccion)) throw new Exception("LA DIRECCIÓN NO PUEDE ESTAR VACIA.");
        }

        private void ValidarDescuento()
        {
            if (_descuento > 100 || _descuento < 0) throw new Exception("EL PORCENTAJE DE DESCUENTO NO ES VÁLIDO.");
        }

        public void Validar()
        {
            ValidarNombreVacio();
            ValidarTelefonoVacio();
            ValidarDireccionVacia();
            ValidarDescuento();
        }


        public void CambiarDescuento(double valorPromo)
        {
            if (valorPromo < 0 || valorPromo > 100) throw new Exception("El valor de promoción debe estar entre 0 y 100.");
            _descuento = valorPromo;
        }


        public int CompareTo(Proveedor other)
        {
            return this._nombre.CompareTo(other._nombre);
        }







    }
}

