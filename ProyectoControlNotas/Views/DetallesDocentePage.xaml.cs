using ProyectoControlNotas.ViewModels;

namespace ProyectoControlNotas.Views;

public partial class DetallesDocentePage : ContentPage
{
	public DetallesDocentePage(DetallesEstudianteViewModels detallesEstudianteViewModels)
	{
		InitializeComponent();
		BindingContext = detallesEstudianteViewModels;
	}
}