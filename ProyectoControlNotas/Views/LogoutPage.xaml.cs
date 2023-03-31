using ProyectoControlNotas.ViewModels;

namespace ProyectoControlNotas.Views;

public partial class LogoutPage : ContentPage
{
	public LogoutPage(LogoutViewModels logoutViewModels)
	{
        Content = new VerticalStackLayout
        {
            Children =
            {
                new Label
                {
                    HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Cerrando Sesión..."
                },
            }
        };
        BindingContext = logoutViewModels;
    }
}