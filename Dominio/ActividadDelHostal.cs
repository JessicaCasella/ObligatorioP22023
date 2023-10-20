using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{

    public class ActividadDelHostal : Actividad
    {
        private string _personaResponsable;
        private string _lugarDelHostal;
        private bool _alAireLibre;
        public ActividadDelHostal(string nombre, string descripcion, DateTime fecha, int cantidadMaxima, int edadMinima, double costo, string personaResponsable, string lugarDelHostal, bool alAireLibre) : base(nombre, descripcion, fecha, cantidadMaxima, edadMinima, costo)
        {
            this._personaResponsable = personaResponsable;
            this._lugarDelHostal = lugarDelHostal;
            this._alAireLibre = alAireLibre;

        }

        public string LugarDelHostal
        {
            get { return _lugarDelHostal; }
        }

        private void ValidarPersonaResponsable()
        {
            if (string.IsNullOrEmpty(_personaResponsable)) throw new Exception("DEBES TENER UNA PERSONA RESPONSABLE");
        }

        public override void Validar()
        {
            base.Validar();
            ValidarPersonaResponsable();
        }
        public override string MostrarDato()
        {
            return _lugarDelHostal;
        }

        public override double CalcularCostoFinal(NivelDeFidelizacion nivel)
        {
            double costofinal = 0;

            double porcentaje = PorcentajeDelNivelDeFidelizacion(nivel);

            costofinal = _costo - ((_costo * porcentaje) / 100);

            return costofinal;
        }

        private double PorcentajeDelNivelDeFidelizacion(NivelDeFidelizacion nivel)
        {
            double porcentaje = 0;

            if (nivel == NivelDeFidelizacion.NIVEL2) porcentaje = 10;
            if(nivel == NivelDeFidelizacion.NIVEL3) porcentaje = 15;
            if (nivel == NivelDeFidelizacion.NIVEL4) porcentaje = 20;

            return porcentaje;
        }

    }
}

