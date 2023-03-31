using SQLite;

namespace ProyectoControlNotas.Models
{
    public abstract class BaseModelo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
