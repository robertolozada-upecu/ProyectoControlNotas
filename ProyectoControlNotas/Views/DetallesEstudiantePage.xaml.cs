using ProyectoControlNotas.ViewModels;

namespace ProyectoControlNotas.Views;

public partial class DetallesEstudiantePage : ContentPage
{
	public DetallesEstudiantePage(DetallesEstudianteViewModels detallesEstudianteViewModels)
	{
		InitializeComponent();
		BindingContext = detallesEstudianteViewModels;
	}
}