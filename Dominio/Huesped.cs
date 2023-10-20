using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Huesped : Usuario
    {

        private TipoDocumento _tipoDeDocumento;
        private string _numeroDocumento;
        private string _habitacion;
        private DateTime _fechaDeNacimiento;
        private NivelDeFidelizacion _nivel;
        private List<Agenda> _misAgendas = new List<Agenda>();

        public Huesped(string email, string password, string nombre, string apellido, TipoDocumento tipoDeDocumento, string numeroDocumento, string habitacion, DateTime fechaDeNacimiento) : base(email, password, nombre, apellido)
        {
            this._tipoDeDocumento = tipoDeDocumento;
            this._numeroDocumento = numeroDocumento;
            this._habitacion = habitacion;
            this._fechaDeNacimiento = fechaDeNacimiento;
            this._nivel = NivelDeFidelizacion.NIVEL1;
        }
         public string NumeroDocumento
        {
            get { return _numeroDocumento; }
        }
        public TipoDocumento TipoDocumento
        {
            get { return _tipoDeDocumento; }
        }
        public List<Agenda> MisAgendas
        {
            get { return _misAgendas; }
        }
        public DateTime FechaDeNacimiento
        {
            get{  return _fechaDeNacimiento; }
        }

        public NivelDeFidelizacion NivelDeFidelizacion
        {
            get { return _nivel; }
        }
      
        public string Habitacion
        {
            get { return _habitacion; }
        }


        public string TipoYNumeroDocumento
        {
            get { return $"{_tipoDeDocumento}: {_numeroDocumento}"; }
        }

        private void ValidarHabitacion()
        {
            if (string.IsNullOrEmpty(_habitacion)) throw new Exception("DEBE INGRESAR UNA HABITACIÓN.");
        }

        private void ValidarDocumento()
        {
            if (_tipoDeDocumento == TipoDocumento.CI)
            {
                try
                {
                    string numeroDoc = _numeroDocumento.Trim();
                    int numDoc;

                    bool exito = int.TryParse(_numeroDocumento, out numDoc);
                    if (string.IsNullOrEmpty(numeroDoc) || numeroDoc.Length != 8 || !exito) throw new Exception("El número de documento no es valido.");

                    int[] factores = { 2, 9, 8, 7, 6, 3, 4 };

                    int suma = 0;

                    for (int i = 0; i < factores.Length; i++)
                    {
                        suma += (factores[i] * int.Parse(numeroDoc[i].ToString()));
                    }

                    int numeroParaVerificar = suma;

                    while (numeroParaVerificar % 10 != 0)
                    {
                        numeroParaVerificar++;
                    }

                    int NumeroVerificador = numeroParaVerificar - suma;
                    if (NumeroVerificador != int.Parse(_numeroDocumento[7].ToString())) throw new Exception("Número de documento mal ingresado");

                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }
        public override void Validar()
        {
            base.Validar();
            ValidarHabitacion();
            ValidarDocumento();
        }
         public override bool Equals(object obj)
        {
            Huesped a = obj as Huesped;
            return (base.Equals(obj)) || a != null && (_tipoDeDocumento.Equals(a._tipoDeDocumento) && _numeroDocumento.Equals(a._numeroDocumento));
        }

        public override string GetRol()
        {
            return "huesped";
        }

        public void AgregarAgenda(Agenda A)
        {
            if (A == null) throw new Exception("la agenda no puede ser nula");
            _misAgendas.Add(A);
            A.Actividad.AgregarPersonaEnActividad();
        }
        public List<Agenda> ObtenerAgendasAPartirDeUnaFecha(DateTime fecha)
        {
            List<Agenda> aux = new List<Agenda>();

            foreach (Agenda a in _misAgendas)
            {
                if(a.FechaAgenda >= fecha)
                {
                    aux.Add(a);
                }
            }
            aux.Sort();
            
            return aux;
        }

        public Agenda ObtenerAgenda(int idAct)
        {
            Agenda agenda = null;
            int i = 0;

            while (agenda == null && i < _misAgendas.Count)
            {
                if (_misAgendas[i].Actividad.Id == idAct) agenda = _misAgendas[i];
                i++;
            }
            return agenda;
        }

    }
}


