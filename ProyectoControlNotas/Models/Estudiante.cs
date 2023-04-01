using SQLite;

namespace ProyectoControlNotas.Models
{
    [Table("estudiante")]
    public class Estudiante : BaseModelo
    {
        public string Cedula { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }

        [Ignore]
        public string NombreCompletoEstudiante => $"{Nombres} {Apellidos}";

    }
}
