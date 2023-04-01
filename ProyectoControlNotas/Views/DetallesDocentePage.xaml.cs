using ProyectoControlNotas.ViewModels;

namespace ProyectoControlNotas.Views;

public partial class DetallesDocentePage : ContentPage
{
	public DetallesDocentePage(DetalleDocenteViewModels detalleDocenteViewModels)
	{
		InitializeComponent();
		BindingContext = detalleDocenteViewModels;
	}
}