using ProyectoControlNotas.ViewModels;

namespace ProyectoControlNotas.Views;

public partial class ListadoEstudiantesPage : ContentPage
{
	public ListadoEstudiantesPage(ListadoEstudiantesViewModels listadoEstudiantesViewModels)
	{
		InitializeComponent();
		BindingContext = listadoEstudiantesViewModels;
	}
}