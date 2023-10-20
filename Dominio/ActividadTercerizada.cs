using Dominio.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ActividadTercerizada : Actividad, IValidable
    {
        public Proveedor _proveedor;
        private bool _confirmada;
        private DateTime _fechaConfirmacion;
        public ActividadTercerizada(string nombre, string descripcion, DateTime fecha, int cantidadMaxima, int edadMinima, double costo, Proveedor proveedor) : base(nombre, descripcion, fecha, cantidadMaxima, edadMinima, costo)
        {
            this._proveedor = proveedor;
            this._confirmada = false;
        }

        private void ValidaProveedor()
        {
            if (_proveedor == null) throw new Exception("DEBES TENER UN PROVEEDOR PARA LA ACTIVIDAD");
        }
        public override void Validar()
        {
            base.Validar();
            ValidaProveedor();
        }

        public override double CalcularCostoFinal(NivelDeFidelizacion n)
        {

            if (_confirmada)
            {
                double descProv = _proveedor.Descuento;
                double costoConDescuento = _costo - ((_costo * descProv) / 100);
                return costoConDescuento;
            }
            else
            {
                return _costo;
            }

        }
        public override string MostrarDato()
        {
            return _proveedor.Nombre;
        }


    }

}

