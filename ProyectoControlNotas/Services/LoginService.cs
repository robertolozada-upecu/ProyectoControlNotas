using Newtonsoft.Json;
using ProyectoControlNotas.Models;
using System.Net.Http.Json;

namespace ProyectoControlNotas.Services
{
    public class LoginService
    {
        HttpClient _httpClient;
        public static string DireccionBase = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:3030" : "http://localhost:3030";

        public string MensajeEstado { get; private set; }

        public LoginService()
        {
            _httpClient = new() { BaseAddress = new Uri(DireccionBase) };
        }

        public async Task<RespuestaAtenticacion> Login(LoginModel loginModel)
        {
            try
            {
                var respuesta = await _httpClient.PostAsJsonAsync("/login", loginModel);
                respuesta.EnsureSuccessStatusCode();
                MensajeEstado = "Inicio de sesión exitoso";
                return JsonConvert.DeserializeObject<RespuestaAtenticacion>(await respuesta.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                MensajeEstado = "Inicio de sesión fallido";
                return default;
            }
        }
    }
}
