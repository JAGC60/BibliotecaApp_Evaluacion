using System;

namespace BibliotecaApp_Evaluacion
{
    public class Libro
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public decimal PrecioAlquiler { get; set; }
        public int CopiasDisponibles { get; set; }
        public string Estado { get; set; } = string.Empty;
        public int AutorId { get; set; }

        // Método para procesar el alquiler de ejemplares
        public void AlquilarEjemplar(int cantidad)
        {
            if (CopiasDisponibles >= cantidad)
            {
                CopiasDisponibles -= cantidad;
                if (CopiasDisponibles == 0)
                {
                    Estado = "Agotado";
                }
            }
        }

        public override string ToString()
        {
            return $"[{Codigo}] | {Titulo,-28} | Gen: {Genero,-10} | Alquiler: ${PrecioAlquiler,5:F2} | " +
                   $"Stock: {CopiasDisponibles,3} copias | Estado: {Estado}";
        }
    }
}