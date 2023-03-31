using ProyectoControlNotas.Views;

namespace ProyectoControlNotas;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();

	}

    private async void SeleccionDocente(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync($"{nameof(ListadoEstudiantesPage)}",true);

    }

	private async void SeleccionEstudiante(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync($"{nameof(ListadoEstudiantesPage)}",true);

    }
}

