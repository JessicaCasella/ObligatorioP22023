using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    public class Hostal
    {
        #region Singleton
        private static Hostal _instancia;

        private Hostal()
        {
            PrecargaProveedores();
            PrecargaHuesped();
            PrecargaOperador();
            PrecargaActividades();
            PrecargaAgendas();
        }

        public static Hostal Instancia
        {
            get
            {
                if (_instancia == null) _instancia = new Hostal();
                return _instancia;
            }
        }
        #endregion

        List<Usuario> _usuarios = new List<Usuario>();
        List<Proveedor> _proveedores = new List<Proveedor>();
        List<Actividad> _actividades = new List<Actividad>();

        public List<Proveedor> Proveedores
        {
            get { return _proveedores; }
        }

        public List<Actividad> Actividades
        {
            get { return _actividades; }
        }


        #region ALTAS

        public void AltaProveedor(Proveedor proveedor)
        {
            try
            {
                if (proveedor == null) throw new Exception("EL PROVEEDOR NO PUEDE SER NULO");
                proveedor.Validar();
                if (_proveedores.Contains(proveedor)) throw new Exception("EL PROVEEDOR YA EXISTE");
                _proveedores.Add(proveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void AltaActividad(Actividad actividad)
        {
            try
            {
                if (actividad == null) throw new Exception("LA ACTIVIDAD NO PUEDE SER NULA.");
                actividad.Validar();
                if (_actividades.Contains(actividad)) throw new Exception("LA ACTIVIDAD YA EXISTE.");
                _actividades.Add(actividad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AltaUsuario(Usuario usuario)
        {
            try
            {
                if (usuario == null) throw new Exception("EL USUARIO NO PUEDE SER NULO.");
                usuario.Validar();
                if (_usuarios.Contains(usuario)) throw new Exception("EL USUARIO YA EXISTE");
                _usuarios.Add(usuario);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public void AltaAgenda(Agenda agenda)
        {
            try
            {
                if (agenda == null) throw new Exception("La agenda no puede ser nulo");
                agenda.Validar();
                if (agenda.Huesped.MisAgendas.Contains(agenda)) throw new Exception("YA TIENE UNA AGENDA PARA ESTA ACTIVIDAD");
                agenda.CostoFinal = agenda.Actividad.CalcularCostoFinal(agenda.Huesped.NivelDeFidelizacion);


                agenda.Huesped.AgregarAgenda(agenda);
                
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }



        #endregion

        #region PRECARGAS

        private void PrecargaActividades()
        {
            AltaActividad(new ActividadDelHostal("Recorrido Hostal", "Recorremos el edificio del Hostal", (DateTime.Today), 3, 10, 0, "Juan Perez", "Edificio principal", false));
            AltaActividad(new ActividadDelHostal("Fiesta en la piscina", "Fiesta nocturna en piscina del Hostal", (DateTime.Today), 100, 18, 35, "Sofia Rodriguez", "Piscina Hostal", true));
            AltaActividad(new ActividadDelHostal("Comida compartida", "Comida compartida en comedor del Hostal", (DateTime.Today), 30, 5, 50, "Maria Gutierrez", "Comedor Hostal", false));
            AltaActividad(new ActividadDelHostal("Fogata", "Fogata en patio del Hostal", (DateTime.Today), 30, 15, 10, "Pablo Suarez", "Patio del Hostal", true));
            AltaActividad(new ActividadDelHostal("Noche de truco", "Disfrutaremos una noche de truco", (DateTime.Today), 30, 18, 10, "Jose Ignacio Perez", "Comedor Hostal", false));
            AltaActividad(new ActividadDelHostal("Dia en el Spa", "Disfruta de un dia de relax en el spa del Hostal", (DateTime.Today.AddDays(2)), 5, 18, 65, "Marta Sanchez", "Spa Hostal", false));
            AltaActividad(new ActividadDelHostal("Clase de zumba", "Clase de zumba en el patio del Hostal", (DateTime.Today.AddDays(2)), 10, 13, 180, "Ramiro Gonzalez", "Patio Hostal", true));
            AltaActividad(new ActividadDelHostal("Guerra de Agua", "Estaremos realizando una guerra de agua para los mas pequeños!", (DateTime.Today.AddDays(2)), 20, 8, 0, "Juan Molina", "Patio Hostal", true));
            AltaActividad(new ActividadDelHostal("Partido de Volley", "Partido en la cancha del hostal!", (DateTime.Today.AddDays(2)), 10, 18, 25, "Julio Rios", "Cancha Hostal", true));
            AltaActividad(new ActividadDelHostal("Juegos de Mesa", "Noche de juegos en el comedor principal del Hostal", (DateTime.Today.AddDays(2)), 15, 18, 15, "Facundo Rodriguez", "Comedor Hostal", false));

            AltaActividad(new ActividadTercerizada("Paseo al museo", "visita guiada al museo Blanes recorriendo sus diferentes estilos", (DateTime.Today), 80, 18, 20, Proveedores[0]));
            AltaActividad(new ActividadTercerizada("Dia de playa", "Iremos de 14 a 19 a la hermosa playa de malvin donde nos daremos un bañito", (DateTime.Today.AddDays(2)), 50, 12, 5, Proveedores[0]));
            AltaActividad(new ActividadTercerizada("Noche de peliculas", "visitaremos el cine Cinemateca donde nos deleitaremos del mejor cine independiente del Uruguay", (DateTime.Today.AddDays(2)), 100, 18, 30, Proveedores[0]));

            AltaActividad(new ActividadTercerizada("Salida al shopping", "Iremos a divertirnos al centro comercial Montevideo a visitar todas sus tiendas y puestos gastronomicos", (DateTime.Today.AddDays(3)), 40, 18, 2, Proveedores[1]));
            AltaActividad(new ActividadTercerizada("De visita al parque", "Visitaremos el hermoso parque rodo donde realizaremos un picnic unico", (DateTime.Today.AddDays(3)), 100, 8, 5, Proveedores[1]));
            AltaActividad(new ActividadTercerizada("A remontar cometas", "Pasaremos la tarde remontando muchas cometas otorgada por Hostal donde aprovecharemos el viento para divertirnos", (DateTime.Today.AddDays(3)), 20, 5, 5, Proveedores[1]));

            AltaActividad(new ActividadTercerizada("Semana de la cerveza", "El supermercado Geant celebra la semana de la cerveza y no te lo podes perder", (DateTime.Today.AddDays(3)), 40, 18, 10, Proveedores[2]));
            AltaActividad(new ActividadTercerizada("Partido de futbol", "Invitamos coordialmente a las huespedes a participar de un partido de futbol disputado entre ellos", (DateTime.Today.AddDays(3)), 16, 16, 10, Proveedores[2]));
            AltaActividad(new ActividadTercerizada("Salida en bicicleta", "Llevaremos 15 bicicletas para realizar un tour por la ciudad", (DateTime.Today.AddDays(3)), 15, 15, 15, Proveedores[2]));

            AltaActividad(new ActividadTercerizada("Torneo de Pool", "Organizaremos un pequeño torneo de billar para los huespedes compartiendo una noche agradable", (DateTime.Today), 10, 18, 30, Proveedores[3]));
            AltaActividad(new ActividadTercerizada("Cena romantica", "Invitamos a las parejas a participar de una romantica cena en el puerto de Montevideo", (DateTime.Today.AddDays(3)), 100, 18, 10, Proveedores[3]));
            AltaActividad(new ActividadTercerizada("Noche de baile", "Sean bienvenidos a uno de los mejores clubs de salsa donde bailaremos hasta cansarnos y tomaremos sofisticados tragos ", (DateTime.Today), 100, 18, 10, Proveedores[3]));

            AltaActividad(new ActividadTercerizada("Go karts!!!!!", "Vengan y disfruten de una emocionante carrera de autos karts donde demostraremos nuestras capacidades y nos divertiremos en equipo", (DateTime.Today), 20, 18, 15, Proveedores[4]));
            AltaActividad(new ActividadTercerizada("Lluvia de estrellas", "Iremos al parque a disfrutar la lluvia de estrellas Santa rita que pasaa 1 vez cada 1500 años", (DateTime.Today), 100, 8, 6, Proveedores[4]));
            AltaActividad(new ActividadTercerizada("Humor negro", "Iremos al mejor bar de comedia del pais a reirnos en una noche de humor negro", (DateTime.Today), 80, 18, 30, Proveedores[4]));

            AltaActividad(new ActividadTercerizada("Poesia", "Visitaremos el show póetico del gran Juan Arnoldo perez Carbajal Gomez Pipistrilo", (DateTime.Today), 80, 10, 18, Proveedores[6]));

            AltaActividad(new ActividadTercerizada("Noche de carne", "Iremos a la mejor parrillada del pais a probar finos cortes de carne", (DateTime.Today), 100, 10, 20, Proveedores[7]));
        }


        private void PrecargaProveedores()
        {
            AltaProveedor(new Proveedor("DreamWorks S.R.L.", "23048549", "Suarez 3380 Apto 304", 10));
            AltaProveedor(new Proveedor("Estela Umpierrez S.A.", "33459678", "Lima 2456", 7));
            AltaProveedor(new Proveedor("TravelFun", "29152020", "Misiones 1140", 9));
            AltaProveedor(new Proveedor("Rekreation S.A.", "29162019", "Bacacay 1211", 11));
            AltaProveedor(new Proveedor("Alonso & Umpierrez", "24051920", "18 de Julio 1956 Apto 4", 10));
            AltaProveedor(new Proveedor("Electric Blue", "26018945", "Cooper 678", 5));
            AltaProveedor(new Proveedor("Lúdica S.A.", "26142967", "Dublin 560", 4));
            AltaProveedor(new Proveedor("Gimenez S.R.L.", "29001010", "Andes 1190", 7));
            AltaProveedor(new Proveedor("Agua y Sol", "22041120", "Agraciada 2512 Apto. 1", 8));
            AltaProveedor(new Proveedor("Norberto Molina", "22001189", "Paraguay 2100", 9));

        }


        private void PrecargaHuesped()
        {
            AltaUsuario(new Huesped("manuel@hostal.com", "Manuel2023", "Manuel", "Rodriguez", TipoDocumento.PASAPORTE, "23234", "110", new DateTime(1998, 02, 05)));
            AltaUsuario(new Huesped("luca@hostal.com", "123456789", "Luca", "Pucciarelli", TipoDocumento.CI, "45333236", "111", new DateTime(1998, 07, 21)));
            AltaUsuario(new Huesped("jessi@hostal.com", "123456789", "Jessica", "Casella", TipoDocumento.CI, "51665249", "112", new DateTime(1999, 05, 31)));
        }

        private void PrecargaOperador()
        {
            AltaUsuario(new Operador("operador@hostal.com", "987654321", "Martin", "Gimenez"));

        }

        private void PrecargaAgendas()
        {
            AltaAgenda(new Agenda(ObtenerUsuarioPorMail("luca@hostal.com") as Huesped, ObtenerActividadPorID(1), new DateTime(2023, 02, 10)));
            AltaAgenda(new Agenda(ObtenerUsuarioPorMail("luca@hostal.com") as Huesped, ObtenerActividadPorID(3), (DateTime.Today)));
            AltaAgenda(new Agenda(ObtenerUsuarioPorMail("jessi@hostal.com") as Huesped, ObtenerActividadPorID(1), (DateTime.Today)));
            AltaAgenda(new Agenda(ObtenerUsuarioPorMail("luca@hostal.com") as Huesped, ObtenerActividadPorID(5), (DateTime.Today)));
            AltaAgenda(new Agenda(ObtenerUsuarioPorMail("luca@hostal.com") as Huesped, ObtenerActividadPorID(9), (DateTime.Today)));
            AltaAgenda(new Agenda(ObtenerUsuarioPorMail("jessi@hostal.com") as Huesped, ObtenerActividadPorID(10), (DateTime.Today)));
            AltaAgenda(new Agenda(ObtenerUsuarioPorMail("luca@hostal.com") as Huesped, ObtenerActividadPorID(2), (DateTime.Today)));
            AltaAgenda(new Agenda(ObtenerUsuarioPorMail("manuel@hostal.com") as Huesped, ObtenerActividadPorID(1), (DateTime.Today)));
            AltaAgenda(new Agenda(ObtenerUsuarioPorMail("jessi@hostal.com") as Huesped, ObtenerActividadPorID(7), (DateTime.Today)));

        }


        #endregion





        public Proveedor ObtenerProveedorPorNombre(string nombre)
        {
            Proveedor pro = null;

            try
            {    
                foreach (Proveedor p in _proveedores)
                {
                    if ((p.Nombre.ToLower() == nombre.ToLower())) { pro = p; }
                }
                if (pro == null) throw new Exception("No se encontro un proveedor con ese nombre.");
                

            }
            catch (Exception ex) {

                throw ex;
            }

            return pro;
        }
        public void CambiarDescuentoProveedor(Proveedor p, double descuento)
        {
            p.CambiarDescuento(descuento);
        }




       public List<Actividad> FiltrarActividadesPorFecha(DateTime fecha)
       {
            List<Actividad> lista = new List<Actividad>();
            
            foreach(Actividad a in _actividades)
            {
                if(a.Fecha == fecha)
                {
                    lista.Add(a);
                }
            }

            return lista;

       }

        public Usuario Login(string usuario, string pass)
        {
            Usuario u = null;
            int i = 0;

            while(u == null && i  < _usuarios.Count)
            {
                if (_usuarios[i].Email == usuario && _usuarios[i].Password ==  pass) u = _usuarios[i];
                i++;
            }
            if (u == null) throw new Exception("Usuario o contraseña incorrecta");

            return u;
        }

        public Actividad ObtenerActividadPorID(int id)
        {
            Actividad a = null;
            int i = 0;

            while(a == null && i < _actividades.Count)
            {
                if(_actividades[i].Id == id) a = _actividades[i];
                i++;
            }

            return a;
        }

        public Usuario ObtenerUsuarioPorMail(string email)
        {
            Usuario u = null;
            int i = 0;

            while (u == null && i < _usuarios.Count)
            {
                if (_usuarios[i].Email == email) u = _usuarios[i];
                i++;
            }

            return u;
        }

        public Huesped ObtenerHuespedPorCedula(string numero, TipoDocumento tipoDoc)
        {
            Huesped h = null;
            int i = 0;

            while(h == null && i < _usuarios.Count)
            {
                if(_usuarios[i] is Huesped)
                {
                    Huesped HUESPED = _usuarios[i] as Huesped;

                    if (HUESPED.TipoDocumento == tipoDoc && HUESPED.NumeroDocumento == numero) h = HUESPED;
                   
                }

                i++;
               
            }

            return h;
        }

        public List<Agenda> ObtenerAgendas(Huesped h, DateTime fecha)
        {
           
            List<Agenda> aux = h.ObtenerAgendasAPartirDeUnaFecha(fecha);
            aux.Sort();
            return aux;
        }

        public List<Agenda> ObtenerAgendas(Huesped h)
        {
            List<Agenda> aux = h.MisAgendas;
            aux.Sort();
            return aux;
        }



        public List<Agenda> ObtenerAgendasDeUnaFecha(DateTime fecha)
        {
            List<Agenda> aux = new List<Agenda>();

            foreach (Usuario u in _usuarios)
            {

                if (u is Huesped)
                {
                    Huesped h = u as Huesped;
                    foreach (Agenda a in h.MisAgendas)
                    {
                        if (a.Actividad.Fecha == fecha) aux.Add(a);

                    }

                }
            }
            aux.Sort();
            return aux;

        }

        public void CambiarEstadoAgendaAConfirmada(Agenda agenda)
        {
            try
            {
                if(agenda == null) throw new Exception("La agenda es nula"); 

                agenda.EstadoDeLaAgenda = EstadoAgenda.CONFIRMADA;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }

        public Agenda ObtenerAgendaPorHuespedYActividad(Huesped huesped, int idAct)
        {
            Agenda agenda = huesped.ObtenerAgenda(idAct);
            return agenda;
        }


    }
}

