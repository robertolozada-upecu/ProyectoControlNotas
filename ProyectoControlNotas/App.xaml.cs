using ProyectoControlNotas.Models;

namespace ProyectoControlNotas;

public partial class App : Application
{
    //public static AutoService AutoService { get; private set; }
    public static InfoUsuario InfoUsuario;
    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
