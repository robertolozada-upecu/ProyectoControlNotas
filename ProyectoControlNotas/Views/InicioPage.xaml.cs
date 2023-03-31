using ProyectoControlNotas.ViewModels;

namespace ProyectoControlNotas.Views;

public partial class InicioPage : ContentPage
{
	public InicioPage(InicioViewModel inicioViewModel)
	{
		InitializeComponent();
		BindingContext = inicioViewModel;
	}
}