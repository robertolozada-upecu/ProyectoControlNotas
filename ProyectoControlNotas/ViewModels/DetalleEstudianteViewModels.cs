using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoControlNotas.Models;
using ProyectoControlNotas.Services;
using ProyectoControlNotas.Views;

namespace ProyectoControlNotas.ViewModels
{
    [QueryProperty(nameof(DetalleEstudiante), "DetalleEstudiante")]

    public partial class DetallesEstudianteViewModels : BaseViewModels
    {
        [ObservableProperty]
        Estudiante detalleEstudiante;
        [ObservableProperty]
        bool estaRefrescando;
        [ObservableProperty]
        int identificador;
        [ObservableProperty]
        string cedula;
        [ObservableProperty]
        string nombres;
        [ObservableProperty]
        string apellidos;
        [ObservableProperty]
        string correo;
        [ObservableProperty]
        string usuario;


        private readonly EstudianteApiService _estudianteApiService;
        public DetallesEstudianteViewModels(EstudianteApiService estudianteApiService)
        {
            _estudianteApiService = estudianteApiService;
        }

        [RelayCommand]
        async Task AgregarActualizarEstudiante()
        {
            //if (string.IsNullOrEmpty(DetalleEstudiante.Cedula) || string.IsNullOrEmpty(DetalleEstudiante.Nombres) || string.IsNullOrEmpty(DetalleEstudiante.Apellidos) || string.IsNullOrEmpty(DetalleEstudiante.Correo) || string.IsNullOrEmpty(DetalleEstudiante.Usuario))
            //{
            //    await Shell.Current.DisplayAlert("Error", "Por favor ingrese valores válidos", "Ok");
            //    return;
            //}
            if(DetalleEstudiante.Id != 0)
            {
                await _estudianteApiService.ActualizarEstudiante(DetalleEstudiante.Id, DetalleEstudiante);
                await Shell.Current.DisplayAlert("Info", _estudianteApiService.MensajeEstado, "Ok");
            }
            else
            {
                await _estudianteApiService.AgregarEstudiante(new Estudiante
                {
                    Cedula = Cedula,
                    Nombres = Nombres,
                    Apellidos = Apellidos,
                    Correo = Correo,
                    Usuario = Usuario,


                });
                await Shell.Current.DisplayAlert("Info", _estudianteApiService.MensajeEstado, "Ok");
            }
            await Shell.Current.GoToAsync(nameof(ListadoEstudiantesPage), true);
        }

        //[RelayCommand]
        //async Task LimpiarFormulario()
        //{
        //    Id = 0;
        //    Cedula = string.Empty;
        //    Nombres = string.Empty;
        //    Identificador = 0;
        //    await ObtenerListaEstudiantes();
        //}
    }
}
