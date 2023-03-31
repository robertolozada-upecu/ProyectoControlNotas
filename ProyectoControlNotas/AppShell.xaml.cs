using ProyectoControlNotas.Views;

namespace ProyectoControlNotas;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(LogoutPage), typeof(LogoutPage));
        Routing.RegisterRoute(nameof(DetallesEstudiantePage), typeof(DetallesEstudiantePage));
		Routing.RegisterRoute(nameof(ListadoEstudiantesPage), typeof(ListadoEstudiantesPage));
    }
}
