using CommunityToolkit.Mvvm.Input;
using ProyectoControlNotas.Views;

namespace ProyectoControlNotas.ViewModels
{
    public partial class LogoutViewModels : BaseViewModels
    {
        public LogoutViewModels()
        {
            CerrarSesion();
        }

        [RelayCommand]
        private async void CerrarSesion()
        {
            SecureStorage.Remove("token");
            App.InfoUsuario = null;
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }   
    }
}
