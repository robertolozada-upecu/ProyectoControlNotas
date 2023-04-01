using ProyectoControlNotas.ViewModels;

namespace ProyectoControlNotas.Views;

public partial class ListadoEstudiantesPage : ContentPage
{
	private readonly ListadoEstudiantesViewModels _listadoEstudiantesViewModels;
	public ListadoEstudiantesPage(ListadoEstudiantesViewModels listadoEstudiantesViewModels)
	{
		InitializeComponent();
		_listadoEstudiantesViewModels = listadoEstudiantesViewModels;
		BindingContext = listadoEstudiantesViewModels;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_listadoEstudiantesViewModels.ObtenerListaEstudiantesCommand.Execute(null);
        //SwipeView.Close();
    }
}