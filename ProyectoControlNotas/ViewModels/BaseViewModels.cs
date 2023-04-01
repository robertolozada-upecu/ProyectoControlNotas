using CommunityToolkit.Mvvm.ComponentModel;

namespace ProyectoControlNotas.ViewModels
{
    public abstract partial class BaseViewModels : ObservableObject
    {
        [ObservableProperty]
        string titulo;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NoEstaCargando))]
        bool estaCargando;
        [ObservableProperty]
        bool opcionAgregar;
        [ObservableProperty]
        bool opcionEditar;
        [ObservableProperty]
        bool opcionBorrar;
        [ObservableProperty]
        bool opcionGuardar;

        public bool NoEstaCargando => !EstaCargando;
    }
}
