using SQLite;

namespace ProyectoControlNotas.Models
{
    [Table("docente")]
    public class Docente : BaseModelo
    {
        public string Cedula { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
        public int Tipo { get; set; }
        
        [Ignore]
        public string NombreCompletoDocente => $"{Nombres} {Apellidos}";
    }
}
