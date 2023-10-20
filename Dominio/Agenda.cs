using Dominio.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{

    public class Agenda : IComparable<Agenda>, IValidable
    {
        
        private EstadoAgenda _estadoAgenda;
        private DateTime _fechaAgenda;
        private Huesped _huesped;
        private Actividad _actividad;
        private double _costoFinal;


        public Agenda(Huesped huesped, Actividad actividad, DateTime fechaAgenda)
        {
            this._fechaAgenda = fechaAgenda;
            this._huesped = huesped;
            this._actividad = actividad;
            this._estadoAgenda = EstadoAgenda.PENDIENTE_PAGO;

        }

        public EstadoAgenda EstadoDeLaAgenda
        {
            get { return _estadoAgenda; }
            set { _estadoAgenda = value; }
        }

        public DateTime FechaAgenda
        {
            get { return _fechaAgenda; }
        }
        public Huesped Huesped
        {
            get { return _huesped; }
        }

        public Actividad Actividad
        {
            get { return _actividad; }
        }

        public double CostoFinal
        {
            get { return _costoFinal; }
            set { _costoFinal = value; }
        }
        private void ValidarEdad()
        {
            DateTime fechaHoy = DateTime.Today;
            int edadHuesped = fechaHoy.Year - _huesped.FechaDeNacimiento.Year;


            if (_huesped.FechaDeNacimiento.Month > fechaHoy.Month || (_huesped.FechaDeNacimiento.Month == fechaHoy.Month && _huesped.FechaDeNacimiento.Day > fechaHoy.Day))
            {
                edadHuesped--;
            }

            if (_actividad.EdadMinima > edadHuesped) throw new Exception("No cumple con la edad miníma.");
            
        }

        private void ValidarCupo()
        {
            if ((_actividad.PersonasAgendadas + 1) > _actividad.CantidadMaxima) throw new Exception("No hay cupos disponibles para la actividad.");
        }
        private void EstadoAgendaSinCosto()
        {
            if (_actividad.Costo == 0)
            {
                this._estadoAgenda = EstadoAgenda.CONFIRMADA;
            }
        }
        
        public void Validar()
        {
            ValidarCupo();
            ValidarEdad();
            EstadoAgendaSinCosto();
        }

        public override bool Equals(object obj)
        {
            Agenda a = obj as Agenda;
            return a != null && _actividad.Equals(a._actividad) && _huesped.Equals(a._huesped);
        }

        public int CompareTo(Agenda other)
        {
            int orden = this._actividad.Fecha.CompareTo(other._actividad.Fecha);

            if (orden == 0) orden = this._actividad.Nombre.CompareTo(other._actividad.Nombre);


            return orden;
        }

    }
}
