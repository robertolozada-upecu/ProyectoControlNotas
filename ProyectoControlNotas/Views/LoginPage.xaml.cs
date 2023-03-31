using ProyectoControlNotas.ViewModels;

namespace ProyectoControlNotas.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
		BindingContext = loginViewModel;
	}
}