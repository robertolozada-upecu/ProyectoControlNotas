using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoControlNotas.Models;
using ProyectoControlNotas.Services;
using ProyectoControlNotas.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ProyectoControlNotas.ViewModels
{
    public partial class ListadoEstudiantesViewModels : BaseViewModels
    {
        [ObservableProperty]
        bool estaRefrescando;

        private readonly EstudianteApiService _estudianteApiService;

        public ObservableCollection<Estudiante> Estudiantes { get; private set; } = new();

        public ListadoEstudiantesViewModels(EstudianteApiService estudianteApiService)
        {
            Titulo = "Listado de estudiantes";
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
        async Task ObtenerListaEstudiantes()
        {
            if (EstaCargando) return;
            try
            {
                EstaCargando = true;
                if (Estudiantes.Any())
                    Estudiantes.Clear();

                var estudiantes = await _estudianteApiService.ObtenerEstudiantes();
                foreach (var estudiante in estudiantes)
                {
                    Estudiantes.Add(estudiante);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"No se pueden obtener el listado de los estudiantes: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "No se pudo obtener la lista de estudiantes", "Ok");
            }
            finally
            {
                EstaRefrescando = false;
                EstaCargando = false;
            }
        }

        [RelayCommand]
        public async Task AgregarEstudiante()
        {
            var datosEstudiante = new Dictionary<string, object>();
            var estudiante = new Estudiante();
            datosEstudiante.Add("DetalleEstudiante", estudiante);
            await AppShell.Current.GoToAsync(nameof(DetalleEstudiantePage), datosEstudiante);
        }

        [RelayCommand]
        public async Task EliminarEstudiante(Estudiante estudiante)
        {
            if (estudiante.Id == 0)
            {
                await Shell.Current.DisplayAlert("Error", "Por favor, intente otra vez", "Ok");
                return;
            }
           
                await _estudianteApiService.EliminarEstudiante(estudiante.Id);
                await Shell.Current.DisplayAlert("Info", _estudianteApiService.MensajeEstado, "Ok");
            await ObtenerListaEstudiantes();
        }

        [RelayCommand]
        async Task EditarEstudiante(Estudiante estudiante)
        {
            if (estudiante == null) return;

            var datosEstudiante = new Dictionary<string, object>();
            datosEstudiante.Add("DetalleEstudiante", estudiante);
            await AppShell.Current.GoToAsync(nameof(DetalleEstudiantePage), datosEstudiante);
        }
    }
}
