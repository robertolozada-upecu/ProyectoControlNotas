using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoControlNotas.Models;
using ProyectoControlNotas.Services;
using ProyectoControlNotas.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ProyectoControlNotas.ViewModels
{
    public partial class ListadoDocentesViewModels : BaseViewModels
    {
        [ObservableProperty]
        bool estaRefrescando;

        private readonly DocenteApiService _docenteApiService;

        public ObservableCollection<Docente> Docentes { get; private set; } = new();

        public ListadoDocentesViewModels(DocenteApiService docenteApiService)
        {
            Titulo = "Listado de docentes";
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
        public async Task ObtenerListaDocentes()
        {
            if (EstaCargando) return;
            try
            {
                EstaCargando = true;
                if (Docentes.Any())
                    Docentes.Clear();

                var docentes = await _docenteApiService.ObtenerDocentes();
                foreach (var docente in docentes)
                {
                    Docentes.Add(docente);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"No se pueden obtener el listado de los docentes: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "No se pudo obtener la lista de docentes", "Ok");
            }
            finally
            {
                EstaRefrescando = false;
                EstaCargando = false;
            }
        }

        [RelayCommand]
        public async Task AgregarDocente()
        {
            var datosDocente= new Dictionary<string, object>();
            var docente = new Docente();
            datosDocente.Add("DetalleDocente", docente);
            await AppShell.Current.GoToAsync(nameof(DetallesDocentePage), datosDocente);
        }

        [RelayCommand]
        public async Task EliminarDocente(Docente docente)
        {
            if (docente.Id == 0)
            {
                await Shell.Current.DisplayAlert("Error", "Por favor, intente otra vez", "Ok");
                return;
            }

            await _docenteApiService.EliminarDocente(docente.Id);
            await Shell.Current.DisplayAlert("Info", _docenteApiService.MensajeEstado, "Ok");
            await ObtenerListaDocentes();
        }

        [RelayCommand]
        async Task EditarDocente(Docente docente)
        {
            if (docente == null) return;

            var datosDocente = new Dictionary<string, object>();
            datosDocente.Add("DetalleDocente", docente);
            await AppShell.Current.GoToAsync(nameof(DetallesDocentePage), datosDocente);
        }
    }
}
