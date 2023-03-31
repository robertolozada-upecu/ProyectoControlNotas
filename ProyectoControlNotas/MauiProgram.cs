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
        builder.Services.AddTransient<EstudianteApiService>();
        builder.Services.AddTransient<DocenteApiService>();
        builder.Services.AddTransient<LoginService>();

        builder.Services.AddSingleton<ListadoEstudiantesViewModels>();
        builder.Services.AddSingleton<ListadoDocentesViewModels>();
        builder.Services.AddSingleton<InicioViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<LogoutViewModels>();
        builder.Services.AddTransient<DetallesEstudianteViewModels>();

        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<LogoutPage>();
        builder.Services.AddSingleton<InicioPage>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<ListadoEstudiantesPage>();
        builder.Services.AddTransient<DetallesEstudiantePage>();

        return builder.Build();
	}
}
