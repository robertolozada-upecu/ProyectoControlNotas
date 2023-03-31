using Newtonsoft.Json;
using ProyectoControlNotas.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ProyectoControlNotas.Services
{
    public class EstudianteApiService
    {
        HttpClient _httpClient;
        public static string DireccionBase = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:3030" : "http://localhost:3030";
        public string MensajeEstado { get; private set; }

        public EstudianteApiService()
        {
            _httpClient = new() { BaseAddress = new Uri(DireccionBase) };
        }

        public async Task<List<Estudiante>> ObtenerEstudiantes()
        {
            try
            {
                await InicializarTokenAutenticacion();
                var respuesta = await _httpClient.GetStringAsync("/estudiante");
                return JsonConvert.DeserializeObject<List<Estudiante>>(respuesta);
            }
            catch (Exception)
            {

                MensajeEstado = "No se ha podido recuperar la información";
            }

            return null;
        }

        public async Task AgregarEstudiante(Estudiante estudiante)
        {
            try
            {
                var respuesta = await _httpClient.PostAsJsonAsync("/estudiante", estudiante);
                respuesta.EnsureSuccessStatusCode();
                MensajeEstado = respuesta.EnsureSuccessStatusCode().IsSuccessStatusCode ? "Ingreso exitoso" : "La operación de inserción ha fallado";
            }
            catch (Exception)
            {
                MensajeEstado = "No se ha podido insertar el item";
            }
        }

        public async Task EliminarEstudiante(int id)
        {
            try
            {
                var respuesta = await _httpClient.DeleteAsync($"/estudiante/{id}");
                respuesta.EnsureSuccessStatusCode();
                MensajeEstado = respuesta.EnsureSuccessStatusCode().IsSuccessStatusCode ? "Eliminación exitosa" : "La operación de borrado ha fallado";
            }
            catch (Exception)
            {
                MensajeEstado = "No se ha podido eliminar el item";
            }
        }

        public async Task<Estudiante> ObtenerEstudiante(int id)
        {
            try
            {
                var respuesta = await _httpClient.GetStringAsync($"/estudiante/{id}");
                return JsonConvert.DeserializeObject<Estudiante>(respuesta);
            }
            catch (Exception)
            {

                MensajeEstado = "No se ha podido recuperar la información";
            }

            return null;
        }

        public async Task ActualizarEstudiante(int id, Estudiante estudianteModificado)
        {
            try
            {
                if (estudianteModificado == null)
                    throw new Exception("Estudiante no válido");
                var resultado = await _httpClient.PutAsJsonAsync($"/estudiante/{id}", estudianteModificado);
                resultado.EnsureSuccessStatusCode();
                MensajeEstado = resultado.EnsureSuccessStatusCode().IsSuccessStatusCode ? "Actualización exitosa" : "La operación de actualización ha fallado";
            }
            catch (Exception)
            {
                MensajeEstado = "No se ha podido actualizar el item";
            }
        }

        public async Task InicializarTokenAutenticacion()
        {
            var token = await SecureStorage.GetAsync("token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
