using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaApp_Evaluacion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Asegura que la consola muestre caracteres especiales en UTF-8
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("Sistema de Gestión de Biblioteca - UNAB\n");

            // FASE 2: Carga de Datos y Gestión de Colecciones
            List<Autor> autores = new List<Autor>
            {
                new Autor { Id = 1, Nombre = "Gabriel García Márquez", Nacionalidad = "Colombiana" },
                new Autor { Id = 2, Nombre = "Isabel Allende", Nacionalidad = "Chilena" },
                new Autor { Id = 3, Nombre = "Mario Vargas Llosa", Nacionalidad = "Peruana" },
                new Autor { Id = 4, Nombre = "Jorge Luis Borges", Nacionalidad = "Argentina" },
                new Autor { Id = 5, Nombre = "Miguel de Cervantes", Nacionalidad = "Española" }
            };

            List<Libro> libros = new List<Libro>
            {
                new Libro { Id = 101, Codigo = "LIB-001", Titulo = "Cien Años de Soledad", Genero = "Novela", PrecioAlquiler = 3.50m, CopiasDisponibles = 5, Estado = "Disponible", AutorId = 1 },
                new Libro { Id = 102, Codigo = "LIB-002", Titulo = "La Casa de los Espíritus", Genero = "Novela", PrecioAlquiler = 2.80m, CopiasDisponibles = 2, Estado = "Disponible", AutorId = 2 },
                new Libro { Id = 103, Codigo = "LIB-003", Titulo = "La Ciudad y los Perros", Genero = "Ficción", PrecioAlquiler = 4.00m, CopiasDisponibles = 3, Estado = "Disponible", AutorId = 3 },
                new Libro { Id = 104, Codigo = "LIB-004", Titulo = "Ficciones", Genero = "Cuento", PrecioAlquiler = 2.50m, CopiasDisponibles = 4, Estado = "Disponible", AutorId = 4 },
                new Libro { Id = 105, Codigo = "LIB-005", Titulo = "Don Quijote de la Mancha", Genero = "Clásico", PrecioAlquiler = 5.00m, CopiasDisponibles = 1, Estado = "Disponible", AutorId = 5 }
            };

            // Simulación del registro de alquileres (Actualización de stock y estados)
            Console.WriteLine("--- SIMULACIÓN DE REGISTRO DE ALQUILERES ---");
            Console.WriteLine("Procesando alquiler de 2 copias de 'La Casa de los Espíritus'...");
            libros[1].AlquilarEjemplar(2); // Alquilamos 2 copias (pasa a 0 stock y cambia estado a Agotado)

            Console.WriteLine("Procesando alquiler de 2 copias de 'Ficciones'...");
            libros[3].AlquilarEjemplar(2); // Alquilamos 2 copias de 4 (quedan 2 copias disponibles)
            Console.WriteLine("Registro de préstamos completado exitosamente.\n");

            // FASE 3: Consultas LINQ con Query Syntax
            Console.WriteLine(" FASE 3: CONSULTAS LINQ (QUERY SYNTAX) ");

            // Ejercicio 1: Filtro y Ordenamiento
            var librosDisponiblesQuery = from l in libros
                                         where l.CopiasDisponibles > 0
                                         orderby l.PrecioAlquiler ascending
                                         select l;

            Console.WriteLine("\nLibros disponibles ordenados por precio:");
            foreach (var l in librosDisponiblesQuery)
            {
                Console.WriteLine($"{l.Codigo} | {l.Titulo,-25} | Copias: {l.CopiasDisponibles,2} | Precio: ${l.PrecioAlquiler:F2}");
            }

            // Ejercicio 2: Relación de Colecciones (Join)
            var librosConAutorQuery = from l in libros
                                      join a in autores on l.AutorId equals a.Id
                                      select new
                                      {
                                          l.Titulo,
                                          Autor = a.Nombre,
                                          l.Genero,
                                          l.CopiasDisponibles
                                      };

            Console.WriteLine("\nRelación de Libros y Autores:");
            foreach (var item in librosConAutorQuery)
            {
                Console.WriteLine($"Libro: {item.Titulo,-25} | Autor: {item.Autor,-22} | Género: {item.Genero,-8} | Copias: {item.CopiasDisponibles}");
            }

            // FASE 4: Method Syntax y Agregaciones
            Console.WriteLine("\n FASE 4: METHOD SYNTAX Y AGREGACIONES ");

            // Reescritura Ejercicio 1 con Method Syntax (.Where / .OrderBy)
            var librosDisponiblesMethod = libros
                .Where(l => l.CopiasDisponibles > 0)
                .OrderBy(l => l.PrecioAlquiler);

            Console.WriteLine("\nLibros disponibles (Method Syntax):");
            foreach (var l in librosDisponiblesMethod)
            {
                Console.WriteLine($"{l.Codigo} | {l.Titulo,-25} | Copias disponibles: {l.CopiasDisponibles} | ${l.PrecioAlquiler:F2}");
            }

            // Reportes Estadísticos y Agregaciones
            Console.WriteLine("\nReporte General de la Biblioteca:");
            decimal totalAlquileres = libros.Sum(l => l.PrecioAlquiler);

            // Calculamos el promedio exacto y lo redondeamos a entero
            double promedioExacto = libros.Average(l => l.CopiasDisponibles);
            int promedioCopias = (int)Math.Round(promedioExacto);

            bool hayAgotados = libros.Any(l => l.CopiasDisponibles == 0);

            Console.WriteLine($"Valor total de alquileres posibles: ${totalAlquileres:F2}");
            Console.WriteLine($"Promedio de copias disponibles por libro: {promedioCopias} copias");
            Console.WriteLine($"¿Existen libros con stock agotado?: {hayAgotados}");

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
}