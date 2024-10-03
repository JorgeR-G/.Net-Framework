using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generador_de_Contraseñas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Variables necesarias
            string nombreUsuario, opcion, contraseña;

            // Declaramos "verificarContraseña" para que reciba dos valores
            (bool contraseñaValida, string mensajeError) verificarContraseña;

            //Titulo para el programa
            Console.WriteLine("\t\tRegistro\n\n");

            //Pedimos el nombre de usuario
            Console.Write("Ingrese un nombre de usuario:");
            nombreUsuario = Console.ReadLine();


            Console.Write("¿Desea que le generemos una contraseña segura? (si/no): ");
            opcion = Console.ReadLine();
            opcion = opcion.ToLower();

            switch (opcion)

            {
                case "si":
                    //Intanciar la clase Contraseña
                    Contraseña contraseña1 = new Contraseña();

                    // Llama a su método "GenerarContraseña"
                    contraseña = contraseña1.GenerarContraseña();

                    Console.WriteLine($"Esta es la contraseña que generamos para ti, guardala en un lugar seguro: {contraseña}");

                    Console.Write("\nPrtesiona cualquier tecla para terminar tu registro ");
                    Console.ReadKey();
                    Console.Clear();

                    //Mostramos un resumen de los datos
                    Console.WriteLine($"\nTus datos de acceso son los siguietes: \n\tusuario: {nombreUsuario}\n\tcontraseña: {contraseña}");
                    break;
                case "no":
                    Console.Write("\nIngrese una contraseña segura (La contraseña debe contener entre 8-20 caracteres, incluido un número, una mayúscula, una minúscula y uno de los siguientes carácteres especiales: $%#&!?): ");

                    contraseña = Console.ReadLine();
                    Contraseña contraseña2 = new Contraseña();
                    verificarContraseña = contraseña2.ComprobarContraseña(contraseña);

                    if (verificarContraseña.contraseñaValida)
                    {
                        Console.Write("\nPresiona cualquier tecla para terminar tu registro ");
                        Console.ReadKey();
                        Console.Clear();

                        Console.WriteLine($"\nTus datos de acceso son los siguientes:\n\tusuario: {nombreUsuario}\n\tcontraseña: {contraseña}");
                    }
                    else
                    {
                        Console.WriteLine(verificarContraseña.mensajeError + ". Ingresa una contraseña válida ");

                    }
                    break;
            }
        }
    }

    class Contraseña
    {
        // 4 Colecciones de caracteres para escoger  y generar la contraseña
        string numeros = "012345678";
        string letrasMin = "abcdefghijklmnopqrstuvwxyz";
        string letrasMay = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string caracterEspecial = "$%#&!?";

        // Contadores para verificar el número de caracteres de cada grupo
        int numContiene = 0, minContiene = 0, mayContiene = 0, espContiene = 0;

        // Método para generar la contraseña
        public string GenerarContraseña()
        {
            // Aqui guardamos la contraseña
            string contraseñaGenerada = "";

            //  Instanciamos a la clase Random para usarla más adelante
            Random random = new Random();

            // Declaramos una variable que guarda el tamaño que tendrá la contraseña
            int longitudContraseña = random.Next(8, 21);

            // Variables que van a determinar el número de caracteres que se usarán de cada grupo.
            double numTener = longitudContraseña * .15;
            double minTener = longitudContraseña * .35;
            double mayTener = longitudContraseña * .35;
            double espTener = longitudContraseña * .15;

            // Variable de tipo char que va a almacenar a cada uno de los caracteres que van a conformar a la contraseña
            char caracterEscogido;

            // Usamos una interación while para ir colocanndo un carácter
            while (contraseñaGenerada.Length < longitudContraseña)
            {
                // Seleccionar uno ded los 4 grupos de string que tenemos
                switch (random.Next(0, 4))
                {
                    case 0:
                        if (numContiene < numTener)
                        {
                            caracterEscogido = numeros[random.Next(numeros.Length)];
                            contraseñaGenerada += caracterEscogido;
                            numContiene++;
                        }
                        break;
                    case 1:
                        if (minContiene < minTener)
                        {
                            caracterEscogido = letrasMin[random.Next(letrasMin.Length)];
                            contraseñaGenerada += caracterEscogido;
                            minContiene++;
                        }
                        break;
                    case 2:
                        if (mayContiene < mayTener)
                        {
                            caracterEscogido = letrasMay[random.Next(letrasMay.Length)];
                            contraseñaGenerada += caracterEscogido;
                            mayContiene++;
                        }
                        break;
                    case 3:
                        if (espContiene < espTener)
                        {
                            caracterEscogido = caracterEspecial[random.Next(caracterEspecial.Length)];
                            contraseñaGenerada += caracterEscogido;
                            espContiene++;
                        }
                        break;
                }
            }
            return contraseñaGenerada;
        }

        // Método para comprobar contraseña
        public (bool, string) ComprobarContraseña(string contraseñaPa)
        {
            // Variable que guardará el valor bool cuando compruebe todas las caracteristicas de la contraseña
            bool contraseñaValida = false;

            // Variables para cada criterio de la contraseña
            bool hayNumero = false, hayMinuscula = false, hayMayuscula = false, hayEspecial = false;

            // Variable que contendrá el mensaje de error
            string mensajeError = "";

            // Verificar la longitud
            if (contraseñaPa.Length >= 8 && contraseñaPa.Length <= 20)
            {
                // Verificación que contenga al menos un número
                foreach (char elemento in numeros)
                {
                    if (contraseñaPa.IndexOf(elemento) >= 0)
                    {
                        hayNumero = true;
                        break;

                    }
                    else
                    {
                        hayNumero = false;
                        mensajeError = "La contraseña debe contener al menos un número";
                    }
                }
                // Verificación de un número de la contraseña
                if (hayNumero)
                {
                    // Verificamos que contenga al menos una letra minusculas
                    foreach (char elemento in letrasMin)
                    {
                        if (contraseñaPa.IndexOf(elemento) >= 0)
                        {
                            hayMinuscula = true;
                            break;
                        }
                        else
                        {
                            hayMinuscula = false;
                            mensajeError = "La contraseña debe contener al menos una letra minuscula";
                        }
                    }
                    if (hayMinuscula)
                    {
                        // Verificamos que contenga al menos una letra minusculas
                        foreach (char elemento in letrasMay)
                        {
                            if (contraseñaPa.IndexOf(elemento) >= 0)
                            {
                                hayMayuscula = true;
                                break;
                            }
                            else
                            {
                                hayMayuscula = false;
                                mensajeError = "La contraseña debe contener al menos una letra mayúsculas";
                            }
                        }
                    }
                    if (hayMayuscula)
                    {
                        // Verificamos que contenga al menos una letra minusculas
                        foreach (char elemento in caracterEspecial)
                        {
                            if (contraseñaPa.IndexOf(elemento) >= 0)
                            {
                                hayEspecial = true;
                                break;
                            }
                            else
                            {
                                hayEspecial = false;
                                mensajeError = "La contraseña debe contener al menos una carácter especial ($%#&!?)";
                            }
                        }
                    }
                }
                // Verificación que exista un número, una letra minúscula, una mayúscula y un carácter especial
                if (hayNumero && hayMinuscula && hayMayuscula && hayEspecial)
                {
                    contraseñaValida = true;
                }
                else
                {
                    contraseñaValida = false;
                }
            }
            else
            {
                mensajeError = "La contraseña debe contener entre 8-20 caracteres";
                contraseñaValida = false;
            }

            return (contraseñaValida, mensajeError);
        }
    }
}

