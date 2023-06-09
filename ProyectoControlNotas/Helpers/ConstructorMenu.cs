﻿using ProyectoControlNotas.Controles;
using ProyectoControlNotas.Views;

namespace ProyectoControlNotas.Helpers
{
    public static class ConstructorMenu
    {
        public static void ConstruirMenu()
        {
            Shell.Current.Items.Clear();

            Shell.Current.FlyoutHeader = new CabeceraFlyout();

            var rol = App.InfoUsuario.Rol;

            if (rol.Equals("Administrador"))
            {
                var ItemFlyout = new FlyoutItem()
                {
                    Title = "Administración",
                    Route = nameof(MainPage),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                        new ShellContent
                        {
                            Icon = "hogar",
                            Title = "Página principal",
                            ContentTemplate = new DataTemplate(typeof(MainPage))
                        },
                        new ShellContent
                        {
                            Icon = "estudiante",
                            Title = "Estudiantes",
                            ContentTemplate = new DataTemplate(typeof(ListadoEstudiantesPage))
                        },
                        new ShellContent
                        {
                            Icon = "docente",
                            Title = "Docentes",
                            ContentTemplate = new DataTemplate(typeof(ListadoDocentesPage))
                        },
                    }
                };

                if (!Shell.Current.Items.Contains(ItemFlyout))
                {
                    Shell.Current.Items.Add(ItemFlyout);
                }
            }
            else if (rol.Equals("Coordinador"))
            {
                var ItemFlyout = new FlyoutItem()
                {
                    Title = "Listado",
                    Route = nameof(MainPage),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                        new ShellContent
                        {
                            Icon = "hogar",
                            Title = "Página principal",
                            ContentTemplate = new DataTemplate(typeof(MainPage))
                        },
                        new ShellContent
                        {
                            Icon = "estudiante",
                            Title = "Estudiantes",
                            ContentTemplate = new DataTemplate(typeof(ListadoEstudiantesPage))
                        },
                        new ShellContent
                        {
                            Icon = "docente",
                            Title = "Docentes",
                            ContentTemplate = new DataTemplate(typeof(ListadoDocentesPage))
                        },
                    }
                };

                if (!Shell.Current.Items.Contains(ItemFlyout))
                {
                    Shell.Current.Items.Add(ItemFlyout);
                }
            }
            else if (rol.Equals("Usuario"))
            {
                var ItemFlyout = new FlyoutItem()
                {
                    Title = "Listado",
                    Route = nameof(MainPage),
                    FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                    Items =
                    {
                        new ShellContent
                        {
                            Icon = "hogar",
                            Title = "Página principal",
                            ContentTemplate = new DataTemplate(typeof(MainPage))
                        },
                        new ShellContent
                        {
                            Icon = "estudiante",
                            Title = "Estudiantes",
                            ContentTemplate = new DataTemplate(typeof(ListadoEstudiantesPage))
                        },
                        new ShellContent
                        {
                            Icon = "docente",
                            Title = "Docentes",
                            ContentTemplate = new DataTemplate(typeof(ListadoDocentesPage))
                        },
                    }
                };

                if (!Shell.Current.Items.Contains(ItemFlyout))
                {
                    Shell.Current.Items.Add(ItemFlyout);
                }
            }
            var flyoutCerrarSesion = new FlyoutItem()
            {
                Title = "Cerrar Sesión",
                Route = nameof(LogoutPage),
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                {
                    new ShellContent
                    {
                        Icon = "cerrar",
                        Title = "Cerrar Sesión",
                        ContentTemplate = new DataTemplate(typeof(LogoutPage))
                    }
                }
            };

            if (!Shell.Current.Items.Contains(flyoutCerrarSesion))
            {
                Shell.Current.Items.Add(flyoutCerrarSesion);
            }
            
        }
    }
}
