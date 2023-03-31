namespace ControlNotas.API.Modelos
{
    public class Docente
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public string Usuario { get; set; }
        public int Tipo { get; set; }
    }
}
