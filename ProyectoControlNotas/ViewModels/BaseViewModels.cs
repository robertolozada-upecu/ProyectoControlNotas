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

        public bool NoEstaCargando => !EstaCargando;
    }
}
