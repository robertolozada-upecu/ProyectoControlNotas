using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoControlNotas.Models;
using ProyectoControlNotas.Services;
using ProyectoControlNotas.Views;

namespace ProyectoControlNotas.ViewModels
{
    [QueryProperty(nameof(DetalleDocente), "DetalleDocente")]

    public partial class DetalleDocenteViewModels : BaseViewModels
    {
        [ObservableProperty]
        Docente detalleDocente;
        [ObservableProperty]
        bool estaRefrescando;

        private readonly DocenteApiService _docenteApiService;

        public DetalleDocenteViewModels(DocenteApiService docenteApiService)
        {
            _docenteApiService = docenteApiService;
            var rol = App.InfoUsuario.Rol;
            if (rol == "Administrador")
            {
                OpcionAgregar = true;
                OpcionEditar = true;
                OpcionBorrar = true;
                OpcionGuardar = true;
            }
            else if (rol.Equals("Coordinador"))
            {
                OpcionAgregar = false;
                OpcionEditar = true;
                OpcionBorrar = false;
                OpcionGuardar = true;
            }
            else
            {
                OpcionAgregar = false;
                OpcionEditar = false;
                OpcionBorrar = false;
                OpcionGuardar = false;
            }
        }

        [RelayCommand]
        async Task AgregarActualizarDocente()
        {
            if (string.IsNullOrEmpty(DetalleDocente.Cedula) || string.IsNullOrEmpty(DetalleDocente.Nombres) || string.IsNullOrEmpty(DetalleDocente.Apellidos) || string.IsNullOrEmpty(DetalleDocente.Correo) || string.IsNullOrEmpty(DetalleDocente.Usuario))
            {
                await Shell.Current.DisplayAlert("Error", "Por favor ingrese valores válidos", "Ok");
                return;
            }
            if (DetalleDocente.Id != 0)
            {
                await _docenteApiService.ActualizarDocente(DetalleDocente.Id, DetalleDocente);
                await Shell.Current.DisplayAlert("Info", _docenteApiService.MensajeEstado, "Ok");
            }
            else
            {
                await _docenteApiService.AgregarDocente(new Docente
                {
                    Cedula = DetalleDocente.Cedula,
                    Nombres = DetalleDocente.Nombres,
                    Apellidos = DetalleDocente.Apellidos,
                    Correo = DetalleDocente.Correo,
                    Usuario = DetalleDocente.Usuario,
                });
                await Shell.Current.DisplayAlert("Info", _docenteApiService.MensajeEstado, "Ok");
            }
            await Shell.Current.GoToAsync(nameof(ListadoDocentesPage), true);
        }

        [RelayCommand]
        async Task Cancelar()
        {
            await Shell.Current.GoToAsync(nameof(ListadoDocentesPage), true);
        }
    }
}