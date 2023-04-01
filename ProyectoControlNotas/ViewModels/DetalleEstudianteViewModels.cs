using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoControlNotas.Models;
using ProyectoControlNotas.Services;
using ProyectoControlNotas.Views;

namespace ProyectoControlNotas.ViewModels
{
    [QueryProperty(nameof(DetalleEstudiante), "DetalleEstudiante")]

    public partial class DetalleEstudianteViewModels : BaseViewModels
    {
        [ObservableProperty]
        Estudiante detalleEstudiante;
        [ObservableProperty]
        bool estaRefrescando;

        private readonly EstudianteApiService _estudianteApiService;
        public DetalleEstudianteViewModels(EstudianteApiService estudianteApiService)
        {
            _estudianteApiService = estudianteApiService;
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
        async Task AgregarActualizarEstudiante()
        {
            if (string.IsNullOrEmpty(DetalleEstudiante.Cedula) || string.IsNullOrEmpty(DetalleEstudiante.Nombres) || string.IsNullOrEmpty(DetalleEstudiante.Apellidos) || string.IsNullOrEmpty(DetalleEstudiante.Correo) || string.IsNullOrEmpty(DetalleEstudiante.Usuario))
            {
                await Shell.Current.DisplayAlert("Error", "Por favor ingrese valores válidos", "Ok");
                return;
            }
            if (DetalleEstudiante.Id != 0)
            {
                await _estudianteApiService.ActualizarEstudiante(DetalleEstudiante.Id, DetalleEstudiante);
                await Shell.Current.DisplayAlert("Info", _estudianteApiService.MensajeEstado, "Ok");
            }
            else
            {
                await _estudianteApiService.AgregarEstudiante(new Estudiante
                {
                    Cedula = DetalleEstudiante.Cedula,
                    Nombres = DetalleEstudiante.Nombres,
                    Apellidos = DetalleEstudiante.Apellidos,
                    Correo = DetalleEstudiante.Correo,
                    Usuario = DetalleEstudiante.Usuario,
                });
                await Shell.Current.DisplayAlert("Info", _estudianteApiService.MensajeEstado, "Ok");
            }
            await Shell.Current.GoToAsync(nameof(ListadoEstudiantesPage), true);
        }

        [RelayCommand]
        async Task Cancelar()
        {
            await Shell.Current.GoToAsync(nameof(ListadoEstudiantesPage), true);
        }
    }
}
