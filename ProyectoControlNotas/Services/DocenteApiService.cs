using Newtonsoft.Json;
using ProyectoControlNotas.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ProyectoControlNotas.Services
{
    public class DocenteApiService
    {
        HttpClient _httpClient;
        public static string DireccionBase = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:3030" : "http://localhost:3030";
        public string MensajeEstado { get; private set; }

        public DocenteApiService()
        {
            _httpClient = new() { BaseAddress = new Uri(DireccionBase) };
        }

        public async Task<List<Docente>> ObtenerDocentes()
        {
            try
            {
                await InicializarTokenAutenticacion();
                var respuesta = await _httpClient.GetStringAsync("/docente");
                return JsonConvert.DeserializeObject<List<Docente>>(respuesta);
            }
            catch (Exception)
            {

                MensajeEstado = "No se ha podido recuperar la información";
            }

            return null;
        }

        public async Task AgregarDocente(Docente docente)
        {
            try
            {
                var respuesta = await _httpClient.PostAsJsonAsync("/docente", docente);
                respuesta.EnsureSuccessStatusCode();
                MensajeEstado = respuesta.EnsureSuccessStatusCode().IsSuccessStatusCode ? "Ingreso exitoso" : "La operación de inserción ha fallado";
            }
            catch (Exception)
            {
                MensajeEstado = "No se ha podido insertar el item";
            }
        }

        public async Task EliminarDocente(int id)
        {
            try
            {
                var respuesta = await _httpClient.DeleteAsync($"/docente/{id}");
                respuesta.EnsureSuccessStatusCode();
                MensajeEstado = respuesta.EnsureSuccessStatusCode().IsSuccessStatusCode ? "Eliminación exitosa" : "La operación de borrado ha fallado";
            }
            catch (Exception)
            {
                MensajeEstado = "No se ha podido eliminar el item";
            }
        }

        public async Task<Docente> ObtenerDocente(int id)
        {
            try
            {
                var respuesta = await _httpClient.GetStringAsync($"/docente/{id}");
                return JsonConvert.DeserializeObject<Docente>(respuesta);
            }
            catch (Exception)
            {

                MensajeEstado = "No se ha podido recuperar la información";
            }

            return null;
        }

        public async Task ActualizarDocente(int id, Docente docenteModificado)
        {
            try
            {
                if (docenteModificado == null)
                    throw new Exception("Docente no válido");
                var resultado = await _httpClient.PutAsJsonAsync($"/docente/{id}", docenteModificado);
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
