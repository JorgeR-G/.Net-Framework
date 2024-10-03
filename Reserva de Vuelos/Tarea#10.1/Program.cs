using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAREA_10
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instanciamos a la clase CompraBoletos
            CompraBoletos compraBoletos = new CompraBoletos();
            compraBoletos.Reservacion();

        }
    }

    // Definimos una estructura para representar la información del cliente
    struct Cliente
    {
        // Campos
        string nombre;
        string apellido;
        string id;
        int edad;

        // Propiedades
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Id { get => id; set => id = value; }
        public int Edad { get => edad; set => edad = value; }
    }

    enum Destinos
    {
        Guadalajara = 900,
        Monterrey = 1000,
        LosAngeles = 1700
    }

    enum Horarios
    {
        Siete_AM = 7,
        Tres_PM = 15,
        Ocho_PM = 20
    }

    enum SeccionAvion
    {
        Atras = 0,
        Centro = 50,
        Adelante = 80
    }

    enum TipoAsiento
    {
        Medio = 20,
        Pasillo = 60,
        Ventana = 90
    }

    class CompraBoletos
    {
        // Campos
        Destinos destinoEscogido;
        Horarios horarioEscogido;
        SeccionAvion seccionEscogida;
        TipoAsiento asientoEscogido;

        int precioBase;
        int precioSeccion;
        int precioAsiento;
        int precioFinal;

        public void Reservacion()
        {
            // Mensaje de bienvenida
            Console.WriteLine("\n\t\tBienvenido a la reserva de vuelos\n");

            //Creamos un objeto cliente para poder guardar su información
            Cliente cliente = new Cliente();

            // Pedimos información del cliente
            Console.WriteLine("Ingrese la información que se le pide a continuación\n");

            Console.Write("Nombre: ");
            cliente.Nombre = Console.ReadLine();
            Console.Write("Apellido: ");
            cliente.Apellido = Console.ReadLine();
            Console.Write("Edad: ");
            cliente.Edad = Convert.ToInt32(Console.ReadLine());
            Console.Write("Número de identificación oficial: ");
            cliente.Id = Console.ReadLine();

            // Mandamos a llamar a los métodos
            SeleccionarDestino();
            SeleccionarHorario();
            SeleccionarSeccion();
            SeleccionarAsiento();
            ResumenReservacion(cliente);
        }

        public void SeleccionarDestino()
        {
            // Variables
            int opcionDestino;
            int indice = 1;

            // Mostramos los destinos disponibles
            Console.WriteLine("\nDestinos disponibles:\n");

            foreach (Destinos elemento in Enum.GetValues(typeof(Destinos)))
            {
                Console.WriteLine($"{indice++}. {elemento} - ${(int)elemento}");
            }

            Console.Write("Seleccione un destino (número): ");
            opcionDestino = Convert.ToInt32(Console.ReadLine());

            switch (opcionDestino)
            {
                case 1:
                    destinoEscogido = Destinos.Guadalajara;
                    precioBase = (int)destinoEscogido;
                    break;
                case 2:
                    destinoEscogido = Destinos.Monterrey;
                    precioBase = (int)destinoEscogido;
                    break;
                case 3:
                    destinoEscogido = Destinos.LosAngeles;
                    precioBase = (int)destinoEscogido;
                    break;
                default:
                    Console.WriteLine("Destino no válido");
                    break;
            }
        }

        public void SeleccionarHorario()
        {
            // Variables
            int opcionHorario;
            int indice = 1;

            // Mostramos los destinos disponibles
            Console.WriteLine("\nHorarios disponibles:\n");

            foreach (Horarios elemento in Enum.GetValues(typeof(Horarios)))
            {
                Console.WriteLine($"{indice++}. {(int)elemento}:00");
            }

            Console.Write("Seleccione un horario (número): ");
            opcionHorario = Convert.ToInt32(Console.ReadLine());

            switch (opcionHorario)
            {
                case 1:
                    horarioEscogido = Horarios.Siete_AM;
                    break;
                case 2:
                    horarioEscogido = Horarios.Tres_PM;
                    break;
                case 3:
                    horarioEscogido = Horarios.Ocho_PM;
                    break;
                default:
                    Console.WriteLine("Horario no válido");
                    break;
            }
        }

        public void SeleccionarSeccion()
        {
            // Variables
            int opcionSeccion;
            int indice = 1;

            // Mostramos las secciones disponibles
            Console.WriteLine("\nSecciones disponibles:\n");

            foreach (SeccionAvion elemento in Enum.GetValues(typeof(SeccionAvion)))
            {
                Console.WriteLine($"{indice++}. {elemento} - ${(int)elemento}");
            }

            Console.Write("Seleccione la sección de su preferencia (número): ");
            opcionSeccion = Convert.ToInt32(Console.ReadLine());

            switch (opcionSeccion)
            {
                case 1:
                    seccionEscogida = SeccionAvion.Atras;
                    precioSeccion = (int)seccionEscogida;
                    break;
                case 2:
                    seccionEscogida = SeccionAvion.Centro;
                    precioSeccion = (int)seccionEscogida;
                    break;
                case 3:
                    seccionEscogida = SeccionAvion.Adelante;
                    precioSeccion = (int)seccionEscogida;
                    break;
                default:
                    Console.WriteLine("Sección no válida");
                    break;
            }
        }

        public void SeleccionarAsiento()
        {
            // Variables
            int opcionAsiento;
            int indice = 1;

            // Mostramos las secciones disponibles
            Console.WriteLine("\nAsientos disponibles:\n");

            foreach (TipoAsiento elemento in Enum.GetValues(typeof(TipoAsiento)))
            {
                Console.WriteLine($"{indice++}. {elemento} - ${(int)elemento}");
            }

            Console.Write("Seleccione el tipo de asiento de su preferencia (número): ");
            opcionAsiento = Convert.ToInt32(Console.ReadLine());

            switch (opcionAsiento)
            {
                case 1:
                    asientoEscogido = TipoAsiento.Medio;
                    precioAsiento = (int)asientoEscogido;
                    break;
                case 2:
                    asientoEscogido = TipoAsiento.Pasillo;
                    precioAsiento = (int)asientoEscogido;
                    break;
                case 3:
                    asientoEscogido = TipoAsiento.Ventana;
                    precioAsiento = (int)asientoEscogido;
                    break;
                default:
                    Console.WriteLine("Asiento no válido");
                    break;
            }
        }

        public void ResumenReservacion(Cliente cliente)
        {
            // Limpiamos la consola para mostrar el resumen
            Console.Clear();

            // Mostramos un resumen de la reserva
            Console.WriteLine("Resumen de la reserva:\n");

            Console.WriteLine($"Nombre: {cliente.Nombre} {cliente.Apellido}");
            Console.WriteLine($"Edad: {cliente.Edad}");
            Console.WriteLine($"Número de identificación oficial: {cliente.Id}");
            Console.WriteLine($"Destino: {destinoEscogido} - ${(int)destinoEscogido}");
            Console.WriteLine($"Horario: {(int)horarioEscogido}:00");
            Console.WriteLine($"Sección: {seccionEscogida} - ${(int)seccionEscogida}");
            Console.WriteLine($"Asiento: {asientoEscogido} - ${(int)asientoEscogido}");
            Console.WriteLine($"Precio Total: ${precioFinal = precioBase + precioSeccion + precioAsiento}");

            // Pedimos confirmación al usuario
            Console.Write("\nConfirme su reserva (Presione cualquier tecla para confirmar): ");
            Console.ReadKey();
        }
    }
}
