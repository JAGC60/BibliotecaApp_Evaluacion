using System;

namespace BibliotecaApp_Evaluacion
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Nacionalidad { get; set; } = string.Empty;

        
        public override string ToString()
        {
            return $"ID: {Id,-3} | Autor: {Nombre,-25} | Pais: {Nacionalidad}";
        }
    }
}