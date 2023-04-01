using ProyectoControlNotas.ViewModels;

namespace ProyectoControlNotas.Views;

public partial class ListadoDocentesPage : ContentPage
{
    private ListadoDocentesViewModels _listadoDocentesViewModels;
    public ListadoDocentesPage(ListadoDocentesViewModels listadoDocentesViewModels)
	{
		InitializeComponent();
        _listadoDocentesViewModels = listadoDocentesViewModels;
        BindingContext = listadoDocentesViewModels;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _listadoDocentesViewModels.ObtenerListaDocentesCommand.Execute(null);
        //SwipeView.Close();
    }
}