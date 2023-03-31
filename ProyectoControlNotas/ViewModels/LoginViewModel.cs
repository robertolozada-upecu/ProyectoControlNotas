using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProyectoControlNotas.Helpers;
using ProyectoControlNotas.Models;
using ProyectoControlNotas.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProyectoControlNotas.ViewModels
{
    public partial class LoginViewModel : BaseViewModels
    {
        [ObservableProperty]
        string usuario;
        [ObservableProperty]
        string clave;

        private readonly LoginService _loginService;

        public LoginViewModel(LoginService loginService)
        {
            _loginService = loginService;
        }

        [RelayCommand]
        async Task Login()
        {
            if (string.IsNullOrEmpty(Usuario) || string.IsNullOrEmpty(Clave))
            {
                await MostrarInfoLogin("Usuario/Clave no válido");
            }
            else
            {
                //Llamar API
                var loginModel = new LoginModel
                {
                    NombreUsuario = Usuario,
                    Contrasenia = Clave
                };

                var respuestaAutenticacion = await _loginService.Login(loginModel);

                await MostrarInfoLogin(_loginService.MensajeEstado);

                if (!string.IsNullOrEmpty(respuestaAutenticacion?.Token))
                {
                    await SecureStorage.SetAsync("token", respuestaAutenticacion.Token);

                    var tokenJson = new JwtSecurityTokenHandler().ReadJwtToken(respuestaAutenticacion.Token);
                    var rol = tokenJson.Claims.FirstOrDefault(claim => claim.Type.Equals(ClaimTypes.Role)).Value;
                    App.InfoUsuario = new InfoUsuario
                    {
                        NombreUsuario = Usuario,
                        Rol = rol
                    };

                    ConstructorMenu.ConstruirMenu();
                    await Shell.Current.GoToAsync($"{nameof(MainPage)}");
                }
            }
        }

        private async Task MostrarInfoLogin(string mensaje)
        {
            await Shell.Current.DisplayAlert("Información Login", mensaje, "Ok");
            Clave = string.Empty;
        }
    }
}
