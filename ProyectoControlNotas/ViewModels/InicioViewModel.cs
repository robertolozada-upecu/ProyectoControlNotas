using ProyectoControlNotas.Helpers;
using ProyectoControlNotas.Models;
using ProyectoControlNotas.Views;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ProyectoControlNotas.ViewModels
{
    public class InicioViewModel
    {
        public InicioViewModel()
        {
            ValidarUsuarioLogeado();
        }

        private async void ValidarUsuarioLogeado()
        {
            var token = await SecureStorage.GetAsync("token");

            if (string.IsNullOrEmpty(token))
            {
                IrPaginaLogin();
            }
            else
            {
                var tokenJson = new JwtSecurityTokenHandler().ReadJwtToken(token);

                if (tokenJson.ValidTo < DateTime.UtcNow)
                {
                    SecureStorage.Remove("token");
                    IrPaginaLogin();
                }
                else
                {
                    var rol = tokenJson.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role))?.Value;
                    var usuario = tokenJson.Claims.FirstOrDefault(c => c.Type.Equals(JwtRegisteredClaimNames.Email))?.Value;

                    App.InfoUsuario = new InfoUsuario
                    {
                        NombreUsuario = usuario,
                        Rol = rol
                    };

                    ConstructorMenu.ConstruirMenu();
                    IrAMainPage();
                }
            }
        }

        private async void IrAMainPage()
        {
            await Shell.Current.GoToAsync($"{nameof(MainPage)}");
        }

        private async void IrPaginaLogin()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }
    }
}
