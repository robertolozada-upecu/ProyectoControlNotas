using Microsoft.Extensions.Logging;
using ProyectoControlNotas.Services;
using ProyectoControlNotas.ViewModels;
using ProyectoControlNotas.Views;

namespace ProyectoControlNotas;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        var pathDBEstudiante = Path.Combine(FileSystem.AppDataDirectory, "estudiante.db");
        var pathDBDocente = Path.Combine(FileSystem.AppDataDirectory, "docente.db");

        builder.Services.AddSingleton<EstudianteApiService>(servicios => ActivatorUtilities.CreateInstance<EstudianteApiService>(servicios, pathDBEstudiante));
        builder.Services.AddSingleton<DocenteApiService>(servicios => ActivatorUtilities.CreateInstance<DocenteApiService>(servicios, pathDBDocente));
        builder.Services.AddSingleton<EstudianteApiService>();
        builder.Services.AddSingleton<DocenteApiService>();
        builder.Services.AddSingleton<LoginService>();

        builder.Services.AddTransient<ListadoEstudiantesViewModels>();
        builder.Services.AddTransient<ListadoDocentesViewModels>();
        builder.Services.AddSingleton<InicioViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddTransient<LogoutViewModels>();
        builder.Services.AddTransient<DetalleEstudianteViewModels>();
        builder.Services.AddTransient<DetalleDocenteViewModels>();

        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddTransient<LogoutPage>();
        builder.Services.AddSingleton<InicioPage>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ListadoEstudiantesPage>();
        builder.Services.AddTransient<DetalleEstudiantePage>();
        builder.Services.AddSingleton<ListadoDocentesPage>();
        builder.Services.AddTransient<DetallesDocentePage>();

        return builder.Build();
	}
}
