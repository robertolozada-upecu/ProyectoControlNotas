using ProyectoControlNotas.ViewModels;

namespace ProyectoControlNotas.Views;

public partial class DetalleEstudiantePage : ContentPage
{
	public DetalleEstudiantePage(DetalleEstudianteViewModels detalleEstudianteViewModels)
	{
		InitializeComponent();
		BindingContext = detalleEstudianteViewModels;
	}
}