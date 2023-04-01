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
		Routing.RegisterRoute(nameof(ListadoEstudiantesPage), typeof(ListadoEstudiantesPage));
        Routing.RegisterRoute(nameof(DetalleEstudiantePage), typeof(DetalleEstudiantePage));
        Routing.RegisterRoute(nameof(ListadoDocentesPage), typeof(ListadoDocentesPage));
        Routing.RegisterRoute(nameof(DetallesDocentePage), typeof(DetallesDocentePage));
    }
}