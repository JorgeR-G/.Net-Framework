using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Seccion_11
{
    class Program
    {
        static void Main(string[] args)
        {
            // Variable que va a guardar la ruta a explorar
            string directorio;

            do
            {
                // Pide al usuario que ingrese la ruta que vamos a explorar
                Console.Write("Por favor, ingrese la ruta del directorio: ");
                directorio = Console.ReadLine();

                // Verifica si el directorio existe
                if (!Directory.Exists(directorio))
                {
                    Console.WriteLine("La ruta especificada no existe. Por favor, ingrese una ruta válida.");
                }

            } while (!Directory.Exists(directorio)); // Mientras el directorio ingresado no exista, seguiremos pidiendo uno válido

            // Si el directorio ingresado si existe, salimos del do-while y ejecutamos el método con ese directorio como argumento
            ExplorarDirectorio(directorio);
        }

        static void ExplorarDirectorio(string directorioPa)
        {
            // Variable para controlar el ciclo encargado de seguir ejecutando el programa
            bool continuar = true;

            // Mientras la variable "continuar" sea true, el programa se seguirá ejecutando
            while (continuar)
            {
                Console.Clear();
                // Mostramos un mensaje con el nombre del directorio que estamos explorando
                Console.WriteLine($"Contenido de: {directorioPa}\n");

                // Obtenemos una lista de todos los archivos y subdirectorios
                string[] archivosSubdirectorios = Directory.GetFileSystemEntries(directorioPa);

                // Mostramos el contenido del directorio en una tabla
                MostrarTabla(archivosSubdirectorios);

                // Le pedimos al usuario que ingrese una opción de las mostradas en la tabla, según su índice. O le damos la opción de salir (s) del programa, o de navegar hacía atrás en la ruta (a)
                Console.Write("Ingresa el número de la opción que deseas explorar (o 'a' para ir hacia atrás en la ruta, 'n' para ingresar una nueva ruta, o 's' para salir): ");
                string opcion = Console.ReadLine();

                // Analizamos las opciones usando una estructura else-if

                // Lógica para salir del programa
                if (opcion.ToLower() == "s") // Convertimos a minúscula la "s"
                {
                    // Asignamos el valor de "false" a la variable continuar, lo que hace que el programa termine
                    continuar = false;
                }
                else if (opcion.ToLower() == "a") // Lógica para regresar en la ruta
                {
                    // Para regresar un nivel en la ruta, usamos a GetDirectoryName para extraer la última parte de la ruta, de esta forma logramos volver hacía atrás
                    directorioPa = Path.GetDirectoryName(directorioPa);
                }
                else if (opcion.ToLower() == "n") // Lógica para explorar una nueva ruta
                {
                    Console.Clear();

                    // Pedimos al usuario que vuelva a ingresar una ruta para poder explorarla
                    Console.Write("Ingresa la nueva ruta: ");
                    string nuevaRuta = Console.ReadLine();

                    // Verificamos que sea una ruta valida
                    if (Directory.Exists(nuevaRuta))
                    {
                        directorioPa = nuevaRuta;
                    }
                    else
                    {
                        // Le hacemos saber al usuario que ha ingresado una ruta inválida
                        Console.WriteLine("Ingresa una ruta válida");
                        // Dejamos que el usuario vea que se equivocó, y no escogió un directorio
                        Console.WriteLine("Presiona cualquier tecla para continuar...");
                        Console.ReadKey();
                    }
                }
                else if (Convert.ToInt32(opcion) >= 0 && Convert.ToInt32(opcion) < archivosSubdirectorios.Length) // Verificamos que la opción numérica (índice) sea válida, que se encuentre dentro del rango existente
                {
                    // Si es válida, entonces convertimos la opción a un valor entero que podamos manipular
                    int opcionEscogida = Convert.ToInt32(opcion);

                    // Verificamos si la opción es un directorio válido
                    if (Directory.Exists(archivosSubdirectorios[opcionEscogida]))
                    {
                        // Si lo es, entonces le asignamos esa ruta a la variable "directorioPa", para navegar dentro de él
                        directorioPa = archivosSubdirectorios[opcionEscogida];
                    }
                    else
                    {
                        // Si la opción no es un directorio, entonces es un archivo, así que llamamos al método que nos permite trabajar con ellos
                        OperacionesArchivos(archivosSubdirectorios[opcionEscogida]);
                    }
                }
                else
                {
                    // Si no se ingresa una opción válida, entonces tendremos que indicárselo al usuario
                    Console.WriteLine("¡Ingresa un número válido o 'a' para regresar, 's' para salir! o ' n' para una nueva ruta");
                }
            }
        }

        // Método usado para mostrar a los archivos y subdirectorios de forma ordenada
        static void MostrarTabla(string[] archivosSubdirectoriosPa)
        {
            // Imprimimos los títulos de la tabla, dejando espacios y colocándolos a la izquierda
            Console.WriteLine($"{"Índice",-8}{"Nombre",-50}{"Tipo",-13}");

            // Instanciamos a string para crear una cadena con guiones que separen a los títulos del contenido de la tabla, y lo hacemos del total del espacio de la suma de los títulos (8 + 50 + 13 = 71)
            String guiones = new string('-', 71);

            // Imprimimos los guiones
            Console.WriteLine(guiones);

            // Declaramos variables para guardar el nombre del archivo/subdirectorio y su tipo (extensión o subdirectorio)
            string nombre, tipo;


            // Recorremos a la matriz que contiene a los archivos y subdirectorios
            for (int i = 0; i < archivosSubdirectoriosPa.Length; i++)
            {
                // Extraemos sólo el nombre del archivo o subdirectorio de la posición en que nos encontremos y se lo asignamos a una variable "nombre"
                nombre = Path.GetFileName(archivosSubdirectoriosPa[i]);

                // Si existe un subdirectorio en nuestra posición actual (for) 
                if (Directory.Exists(archivosSubdirectoriosPa[i]))
                {
                    // Entonces el tipo será un subdirectorio
                    tipo = "Subdirectorio";
                }
                else
                {
                    // Si no, entonces extraemos la extensión del archivo en el que estemos y se la asignamos a "tipo"
                    tipo = Path.GetExtension(archivosSubdirectoriosPa[i]);
                }

                // Mostramos un índice para el elemento en el que estemos, su nombre (sin ruta completa) y su tipo.
                // Usamos la interpolación de cadenas para alinear los elementos "i", "nombre" y "tipo".
                Console.WriteLine($"{i,-8}{nombre,-50}{tipo,-13}");
            }

            //Dejamos un espacio después de mostrar la tabla.
            Console.WriteLine();
        }

        // Método que maneja las operaciones copiar, mover, eliminar y renombrar de un archivo
        static void OperacionesArchivos(string rutaArchivoPa)
        {
            // Variables para colocar rutas y respuestas de las cuatro operaciones
            string rutaCopiarArchivo, rutaMoverArchivo, destinoArchivo, respuestaReemplazo, respuestaEliminar, respuestaRenombrar, nuevoNombreArchivo, rutaArchivoRenombrado;

            // Extraemos el nombre del archivo y su extensión para poder mostrarlo
            string nombreArchivo = Path.GetFileName(rutaArchivoPa);

            // Mostramos el nombre del archivo
            Console.WriteLine($"\n\n¿Qué quieres hacer con el archivo [{nombreArchivo}]?");

            // Mostramos el menú con las operaciones que se pueden realizar
            Console.WriteLine("1. Copiar");
            Console.WriteLine("2. Mover");
            Console.WriteLine("3. Eliminar");
            Console.WriteLine("4. Renombrar");

            Console.Write("Selecciona una opción: ");
            int opcionArchivo = Convert.ToInt32(Console.ReadLine());

            switch (opcionArchivo)
            {
                case 1:

                    //Preguntamos la ruta donde se quiere copiar el archivo
                    Console.Write("\nIngrese la ruta en donde quiere copiar el archivo: ");
                    rutaCopiarArchivo = Console.ReadLine();

                    // Verificamos que la ruta ingresada exista
                    if (Directory.Exists(rutaCopiarArchivo))
                    {
                        // Creamos la ruta de destino con el nombre del archivo ya incluido
                        destinoArchivo = Path.Combine(rutaCopiarArchivo, nombreArchivo);

                        // Verificamos que el archivo no exista en el destino
                        if (!File.Exists(destinoArchivo))
                        {
                            // Copiamos el archivo
                            File.Copy(rutaArchivoPa, destinoArchivo);

                            // Le hacemos saber al usuario que la operación fue exitosa
                            MensajeRealizadoConExito("copiado");
                        }
                        else
                        {
                            Console.Write($"\nEl archivo [{nombreArchivo}] ya existe en la ruta de destino. ¿Desea reemplazarlo? (s/n): ");
                            respuestaReemplazo = Console.ReadLine();

                            if (respuestaReemplazo.ToLower() == "s")
                            {
                                // Copiamos el archivo
                                File.Copy(rutaArchivoPa, destinoArchivo, true);

                                // Le hacemos saber al usuario que la operación fue exitosa
                                MensajeRealizadoConExito("copiado");
                            }
                            else
                            {
                                // Le hacemos saber al usuario que la operación fue cancelada
                                MensajeOperacionCancelada();
                            }
                        }
                    }
                    else
                    {
                        // Método que muestra un mensaje cuando la ruta ingresada no es valida
                        MensajeRutaNoValida();
                    }
                    break;
                case 2:

                    //Preguntamos la ruta donde se quiere mover el archivo
                    Console.Write("\nIngrese la ruta en donde quiere mover el archivo: ");
                    rutaMoverArchivo = Console.ReadLine();

                    // Verificamos que la ruta ingresada exista
                    if (Directory.Exists(rutaMoverArchivo))
                    {
                        // Creamos la ruta de destino con el nombre del archivo ya incluido
                        destinoArchivo = Path.Combine(rutaMoverArchivo, nombreArchivo);

                        // Verificamos que el archivo no exista en el destino
                        if (!File.Exists(destinoArchivo))
                        {
                            // Movemos el archivo
                            File.Move(rutaArchivoPa, destinoArchivo);

                            // Le hacemos saber al usuario que la operación fue exitosa
                            MensajeRealizadoConExito("movido");
                        }
                        else
                        {
                            Console.Write($"\nEl archivo [{nombreArchivo}] ya existe en la ruta de destino. ¿Desea reemplazarlo? (s/n): ");
                            respuestaReemplazo = Console.ReadLine();

                            if (respuestaReemplazo.ToLower() == "s")
                            {
                                // Primero eliminamos el archivo, porque .NET Framework no permite sobrescribir cuando movemos un archivo
                                File.Delete(rutaArchivoPa);

                                // Ahora sí, movemos el archivo
                                File.Move(rutaArchivoPa, destinoArchivo);

                                // Le hacemos saber al usuario que la operación fue exitosa
                                MensajeRealizadoConExito("movido");
                            }
                            else
                            {
                                // Le hacemos saber al usuario que la operación fue cancelada
                                MensajeOperacionCancelada();
                            }
                        }
                    }
                    else
                    {
                        // Método que muestra un mensaje cuando la ruta ingresada no es válida
                        MensajeRutaNoValida();
                    }
                    break;
                case 3:
                    // Preguntamos si se desea eliminar el archivo
                    Console.Write($"\n¿Está seguro de que desea eliminar el archivo [{nombreArchivo}] (s/n)?: ");
                    respuestaEliminar = Console.ReadLine();

                    if (respuestaEliminar.ToLower() == "s")
                    {
                        // Eliminamos el archivo
                        File.Delete(rutaArchivoPa);

                        // Le hacemos saber al usuario que la operación fue exitosa
                        MensajeRealizadoConExito("eliminado");
                    }
                    else
                    {
                        // Le hacemos saber al usuario que la operación fue cancelada
                        MensajeOperacionCancelada();
                    }
                    break;
                case 4:
                    // Pedimos el nuevo nombre para el archivo
                    Console.Write("\nIngresa el nuevo nombre para el archivo (con extensión): ");
                    nuevoNombreArchivo = Console.ReadLine();

                    // Confirmamos que sea correcto
                    Console.Write($"El nuevo nombre de [{nombreArchivo}] será: [{nuevoNombreArchivo}], ¿es correcto? (s/n): ");
                    respuestaRenombrar = Console.ReadLine();

                    if (respuestaRenombrar.ToLower() == "s")
                    {
                        // En caso de querer renombrar, creamos la nueva ruta del archivo con el nuevo nombre. Para ello, quitamos el nombre de la ruta original y le concatenamos el nuevo
                        rutaArchivoRenombrado = Path.Combine(Path.GetDirectoryName(rutaArchivoPa), nuevoNombreArchivo);

                        // Movemos el archivo, esto para simular el renombre
                        File.Move(rutaArchivoPa, rutaArchivoRenombrado);

                        // Le hacemos saber al usuario que la operación fue exitosa
                        MensajeRealizadoConExito("renombrado");
                    }
                    else if (respuestaRenombrar.ToLower() == "n")
                    {
                        // Le hacemos saber al usuario que la operación fue cancelada
                        MensajeOperacionCancelada();
                    }
                    break;
                default:
                    Console.WriteLine("¡Escoge una opción correcta!");
                    Console.Write("Presione cualquier tecla para continuar...");
                    Console.ReadKey();
                    break;
            }
        }

        // Método que muestra un mensaje cuando la ruta ingresada no es válida
        static void MensajeRutaNoValida()
        {
            // Le hacemos saber al usuario que ha ingresado una ruta inválida
            Console.WriteLine("\n¡Ingresa una ruta válida!");

            // Dejamos que el usuario vea que se equivocó, y no escogió un directorio
            Console.Write("Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        // Método que muestra un mensaje cuando alguna operación es cancelada
        static void MensajeOperacionCancelada()
        {
            // Cancelamos la operación
            Console.WriteLine("\n¡Operación cancelada por el usuario!");
            Console.Write("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        // Método que muestra un mensaje cuando las operaciones copiar, mover, eliminar y renombrar son exitosas
        static void MensajeRealizadoConExito(string tipoMovimientoPa)
        {
            Console.WriteLine($"\n¡El archivo se ha {tipoMovimientoPa} con éxito!");
            Console.Write("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}
