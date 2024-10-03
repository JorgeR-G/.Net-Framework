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
            // Variables
            bool repetir = true; //Se encarga de repetir el menú hasta que nosotros decidamos salir del programa
            int opcion;

            Inventario inventario = new Inventario();

            do
            {
                Console.Clear();

                Console.WriteLine("\nMundo Celular\n");
                Console.WriteLine("1. Agregar producto");
                Console.WriteLine("2. Mostrar inventario");
                Console.WriteLine("3. Eliminar producto");
                Console.WriteLine("4. Salir");

                Console.Write("Ingresar una opción: ");
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        inventario.AgregarProducto();
                        break;
                    case 2:
                        // Mostrar inventario
                        inventario.MostrarProductos();
                        break;
                    case 3:
                        inventario.EliminarProducto();
                        break;
                    case 4:  // Finalizar el programa
                        repetir = false; //El bucle se repite mientras "repetir == true", por lo tanto, esta instrucción nos sacará del programa
                        break;
                    default:
                        Console.WriteLine("\n¡Opción invalida, intenta de nuevo!\n");
                        break;
                }
            } while (repetir);
        }
    }

    struct Celular
    {
        // Campos
        string marca;
        string modelo;
        int memoriaPrincipal;
        double precio;
        int stock;

        // Propiedades
        public string Marca { get => marca; set => marca = value; }
        public string Modelo { get => modelo; set => modelo = value; }
        public int MemoriaPrincipal { get => memoriaPrincipal; set => memoriaPrincipal = value; }
        public double Precio { get => precio; set => precio = value; }
        public int Stock { get => stock; set => stock = value; }
    }

    class Inventario
    {
        // Crea una lista de productos para almacenar el inventario
        List<Celular> listaCelulares = new List<Celular>();

        public void AgregarProducto()
        {
            // Creamos un producto (objeto) de tipo Celular
            Celular nuevoProducto = new Celular();

            // Limpiamos consola y colocamos un título
            Console.Clear();
            Console.WriteLine("\n\t\tAgregar producto\n");

            // Preguntamos los valores que tendrá el producto y se los asignamos
            Console.Write("Marca: ");
            nuevoProducto.Marca = Console.ReadLine();

            Console.Write("Modelo: ");
            nuevoProducto.Modelo = Console.ReadLine();

            Console.Write("Memoria: ");
            nuevoProducto.MemoriaPrincipal = Convert.ToInt32(Console.ReadLine());

            Console.Write("Precio: $");
            nuevoProducto.Precio = Convert.ToDouble(Console.ReadLine());

            Console.Write("Stock: ");
            nuevoProducto.Stock = Convert.ToInt32(Console.ReadLine());

            // Agregamos el producto "Celular" al inventario (List)
            listaCelulares.Add(nuevoProducto);

            Console.Write("Producto agregado al inventario. Presiona cualquier tecla para continuar...");
            Console.ReadKey();
        }

        public void MostrarProductos()
        {
            Console.Clear();
            if (listaCelulares.Count == 0)
            {
                Console.WriteLine("¡El inventario está vacío!");
            }
            else
            {
                int indice = 1;
                Console.WriteLine("Inventario de productos:\n");

                foreach (Celular elemento in listaCelulares)
                {
                    Console.WriteLine($"{indice}.- Marca: {elemento.Marca}, Modelo: {elemento.Modelo}, Memoria: {elemento.MemoriaPrincipal}, Precio: ${elemento.Precio}, Stock: {elemento.Stock},");
                    indice++;
                }
            }

            Console.Write("\nPresiona cualquier tecla para continuar... ");
            Console.ReadKey();
        }

        public void EliminarProducto()
        {
            // Variable para indicar el índice del producto a eliminar
            int productoEliminar;

            Console.Clear();
            if (listaCelulares.Count == 0)
            {
                Console.WriteLine("¡El inventario está vacío, no hay nada que eliminar!");
            }
            else
            {
                Console.Write($"Ingresa el número de producto (indice) que deseas eliminar (del 1 al {listaCelulares.Count}): ");
                productoEliminar = Convert.ToInt32(Console.ReadLine()) - 1; //Decimos que es -1 para que el índice ingresado coincida con el indice real de la List

                // Verificamos que el número ingresado sea válido
                if (productoEliminar >= 0 && productoEliminar < listaCelulares.Count)
                {
                    //Confirmamos si el producto ingresado es el que se desea eliminar
                    Console.Write($"¿El producto que deseas eliminar es: \"{listaCelulares[productoEliminar].Marca} {listaCelulares[productoEliminar].Modelo}\"? (Sí/No): ");
                    string opcion = Console.ReadLine().ToLower();

                    if (opcion == "si")
                    {
                        // Variables para mostrar un mensaje de cuál fue el libro eliminado
                        string marcaEliminado = listaCelulares[productoEliminar].Marca;
                        string modeloEliminado = listaCelulares[productoEliminar].Modelo;

                        // Eliminamos el producto
                        listaCelulares.RemoveAt(productoEliminar);

                        // Le mostramos al usuario el libro que se eliminó
                        Console.WriteLine($"\n¡El producto \"{marcaEliminado} {modeloEliminado}\" fue eliminado!");
                    }
                    else
                    {
                        Console.WriteLine("Operación \"eliminar producto\" cancelada");
                    }
                }
                else
                {
                    Console.WriteLine("¡El número de producto no es válido, revisa la lista e intenta nuevamente!");
                }
            }
            Console.Write("\nPresiona cualquier tecla para continuar... ");
            Console.ReadKey();
        }
    }
}
