using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ControlNotas.API.Modelos
{
    public class ProyectoControlNotasDbContext : IdentityDbContext
    {
        public ProyectoControlNotasDbContext(DbContextOptions<ProyectoControlNotasDbContext> opciones) : base(opciones)
        {

        }

        public DbSet<Estudiante> Estudiantes { get; set; }

        public DbSet<Docente> Docentes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Estudiante>().HasData(
                new List<Estudiante>
                {
                    new Estudiante {Id = 1, Cedula = "1711223344", Apellidos ="Lozada", Nombres="Roberto", Correo="roberto@upecu.edu.ec", Usuario="roberto"},
                    new Estudiante {Id = 2, Cedula = "1811223344", Apellidos ="Lozada", Nombres="Franklin", Correo="franklin@upecu.edu.ec", Usuario="franklin"},
                    new Estudiante {Id = 3, Cedula = "1911223344", Apellidos ="Lozada", Nombres="Alexander", Correo="alexander@upecu.edu.ec", Usuario="alexander"},
                    new Estudiante {Id = 4, Cedula = "2011223344", Apellidos ="Tipán", Nombres="Fernando", Correo="fernando@upecu.edu.ec", Usuario="fernando"},
                    new Estudiante {Id = 5, Cedula = "2111223344", Apellidos ="Sánchez", Nombres="Diego", Correo="diego@upecu.edu.ec", Usuario="diego"}
                });
            modelBuilder.Entity<Docente>().HasData(
                new List<Docente>
                {
                    new Docente {Id = 1, Cedula = "1011223344", Apellidos ="Lozada", Nombres="Roberto", Correo="roberto@upecu.edu.ec", Usuario="roberto", Tipo=1},
                    new Docente {Id = 2, Cedula = "1111223344", Apellidos ="Lozada", Nombres="Franklin", Correo="franklin@upecu.edu.ec", Usuario="franklin", Tipo=1},
                    new Docente {Id = 3, Cedula = "1211223344", Apellidos ="Lozada", Nombres="Alexander", Correo="alexander@upecu.edu.ec", Usuario="alexander", Tipo=2},
                    new Docente {Id = 4, Cedula = "1311223344", Apellidos ="Tipán", Nombres="Fernando", Correo="fernando@upecu.edu.ec", Usuario="fernando", Tipo=3},
                    new Docente {Id = 5, Cedula = "1411223344", Apellidos ="Sánchez", Nombres="Diego", Correo="diego@upecu.edu.ec", Usuario="diego", Tipo=3}
                });
            modelBuilder.Entity<IdentityRole>().HasData(new List<IdentityRole>
                {
                    new IdentityRole
                    {
                        Id="12753439-c981-4edc-90ef-1c9327ea07e6",
                        Name="Administrador",
                        NormalizedName="ADMINISTRADOR"
                    },
                    new IdentityRole
                    {
                        Id="9155f87f-7fae-494e-8409-b133f56964dd",
                        Name="Coordinador",
                        NormalizedName="COORDINADOR"
                    },
                    new IdentityRole
                    {
                        Id="8c65a483-2354-4cd1-bff6-a54e51917833",
                        Name="Usuario",
                        NormalizedName="USUARIO"
                    },
                });

            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<IdentityUser>().HasData(new List<IdentityUser>
                {
                    new IdentityUser
                    {
                        Id="48efcce9-184c-4d5a-82e2-09eb20dd481d",
                        Email = "admin@localhost.com",
                        NormalizedEmail = "ADMIN@LOCALHOST.COM",
                        UserName = "admin@localhost.com",
                        NormalizedUserName="ADMIN@LOCALHOST.COM",
                        PasswordHash = hasher.HashPassword(null, "Admin1234."),
                        EmailConfirmed=true
                    },
                    new IdentityUser
                    {
                        Id="dec25a1a-f00d-46cc-9279-1efb101ffffa",
                        Email = "coordinador@localhost.com",
                        NormalizedEmail = "COORDINADOR@LOCALHOST.COM",
                        UserName = "coordinador@localhost.com",
                        NormalizedUserName="COORDINADOR@LOCALHOST.COM",
                        PasswordHash = hasher.HashPassword(null, "Coordinador1234."),
                        EmailConfirmed=true
                    },
                    new IdentityUser
                    {
                        Id="aa45eba9-da45-40bb-aa31-641968f00818",
                        Email = "usuario@localhost.com",
                        NormalizedEmail = "USUARIO@LOCALHOST.COM",
                        UserName = "usuario@localhost.com",
                        NormalizedUserName="USUARIO@LOCALHOST.COM",
                        PasswordHash = hasher.HashPassword(null, "Usuario1234."),
                        EmailConfirmed=true
                    }
                });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId="12753439-c981-4edc-90ef-1c9327ea07e6",
                    UserId="48efcce9-184c-4d5a-82e2-09eb20dd481d"
                },
                new IdentityUserRole<string>
                {
                    RoleId="9155f87f-7fae-494e-8409-b133f56964dd",
                    UserId="dec25a1a-f00d-46cc-9279-1efb101ffffa",
                },
                new IdentityUserRole<string>
                {
                    RoleId="8c65a483-2354-4cd1-bff6-a54e51917833",
                    UserId="aa45eba9-da45-40bb-aa31-641968f00818"
                }
            });
        }
    }
}
