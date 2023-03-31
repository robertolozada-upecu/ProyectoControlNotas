using CommunityToolkit.Mvvm.ComponentModel;
using ProyectoControlNotas.Models;

namespace ProyectoControlNotas.ViewModels
{
    [QueryProperty("docente", "Docente")]
    public partial class DetallesDocenteViewModels : BaseViewModels
    {
        [ObservableProperty]
        Docente docente;
        public DetallesDocenteViewModels()
        {

        }
    }
}