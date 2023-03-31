using ControlNotas.API.Modelos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o =>
{
    o.AddPolicy("PermitirTodas", a => a.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

var pathDB = Path.Join("C:", "sqlite", "controlnotas.db");
//var pathDB = Path.Join(Directory.GetCurrentDirectory(), "listaautos.db");
var conexion = new SqliteConnection($"Data Source={pathDB}");
builder.Services.AddDbContext<ProyectoControlNotasDbContext>(o => o.UseSqlite(conexion));

builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ProyectoControlNotasDbContext>();

builder.Services.AddAuthentication(opciones =>
{
    opciones.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opciones.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opciones =>
{
    opciones.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["ConfiguracionJwt:Emisor"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["ConfiguracionJwt:Audiencia"],
        ValidateLifetime = true,
        //Tiempo de gracia
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["ConfiguracionJwt:Llave"]!))
    };
});

builder.Services.AddAuthorization(opciones =>
{
    opciones.FallbackPolicy = new AuthorizationPolicyBuilder()
    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
    .RequireAuthenticatedUser()
    .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors("PermitirTodas");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/estudiante", async (ProyectoControlNotasDbContext db) => await db.Estudiantes.ToListAsync()).WithTags("Estudiante");
app.MapGet("/estudiante/{id}", async (int id, ProyectoControlNotasDbContext db) =>
    await db.Estudiantes.FindAsync(id) is Estudiante estudiante ? Results.Ok(estudiante) : Results.NotFound()
).WithTags("Estudiante");
app.MapPut("/estudiante/{id}", async (int id, Estudiante estudianteModificado, ProyectoControlNotasDbContext db) =>
{
var estudiante = await db.Estudiantes.FindAsync(id);
if (estudiante == null) return Results.NotFound();

estudiante.Cedula = estudianteModificado.Cedula;
estudiante.Apellidos = estudianteModificado.Apellidos;
estudiante.Nombres = estudianteModificado.Nombres;
estudiante.Correo = estudianteModificado.Correo;
estudiante.Usuario = estudianteModificado.Usuario;
await db.SaveChangesAsync();

return Results.Ok();
}).WithTags("Estudiante");
app.MapDelete("/estudiante/{id}", async (int id, ProyectoControlNotasDbContext db) =>
{
var estudiante = await db.Estudiantes.FindAsync(id);
if (estudiante == null) return Results.NotFound();
db.Remove(estudiante);
await db.SaveChangesAsync();

return Results.Ok();
}).WithTags("Estudiante");
app.MapPost("/estudiante", async (Estudiante estudiante, ProyectoControlNotasDbContext db) =>
{
await db.AddAsync(estudiante);
await db.SaveChangesAsync();

return Results.Created($"/estudiante/{estudiante.Id}", estudiante);
}).WithTags("Estudiante");

app.MapGet("/docente", async (ProyectoControlNotasDbContext db) => await db.Docentes.ToListAsync()).WithTags("Docente");
app.MapGet("/docente/{id}", async (int id, ProyectoControlNotasDbContext db) =>
    await db.Docentes.FindAsync(id) is Docente docente ? Results.Ok(docente) : Results.NotFound()
).WithTags("Docente");
app.MapPut("/docente/{id}", async (int id, Docente docenteModificado, ProyectoControlNotasDbContext db) =>
{
    var docente = await db.Docentes.FindAsync(id);
    if (docente == null) return Results.NotFound();

    docente.Cedula = docenteModificado.Cedula;
    docente.Apellidos = docenteModificado.Apellidos;
    docente.Nombres = docenteModificado.Nombres;
    docente.Correo = docenteModificado.Correo;
    docente.Usuario = docenteModificado.Usuario;
    await db.SaveChangesAsync();

    return Results.Ok();
}).WithTags("Docente");
app.MapDelete("/docente/{id}", async (int id, ProyectoControlNotasDbContext db) =>
{
    var docente = await db.Docentes.FindAsync(id);
    if (docente == null) return Results.NotFound();
    db.Remove(docente);
    await db.SaveChangesAsync();

    return Results.Ok();
}).WithTags("Docente");
app.MapPost("/docente", async (Docente docente, ProyectoControlNotasDbContext db) =>
{
    await db.AddAsync(docente);
    await db.SaveChangesAsync();

    return Results.Created($"/docente/{docente.Id}", docente);
}).WithTags("Docente");

app.MapPost("/login", async (LoginDto loginDto, UserManager<IdentityUser> _userManager) =>
{
var usuario = await _userManager.FindByNameAsync(loginDto.NombreUsuario);

if (usuario is null)
return Results.Unauthorized();

var loginValido = await _userManager.CheckPasswordAsync(usuario, loginDto.Contrasenia);

if (!loginValido)
return Results.Unauthorized();

//Generar token de acceso
var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["ConfiguracionJwt:Llave"]!));
var credenciales = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

var roles = await _userManager.GetRolesAsync(usuario);
var claim = await _userManager.GetClaimsAsync(usuario);
var listaClaimsJwt = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, usuario.Id),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
        new Claim("Confirmado", usuario.EmailConfirmed.ToString()),
    }.Union(claim)
.Union(roles.Select(rol => new Claim(ClaimTypes.Role, rol)));

var securityToken = new JwtSecurityToken(
    issuer: builder.Configuration["ConfiguracionJwt:Emisor"],
    audience: builder.Configuration["ConfiguracionJwt:Audiencia"],
    claims: listaClaimsJwt,
    expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(builder.Configuration["ConfiguracionJwt:DuracionEnMinutos"])),
    signingCredentials: credenciales);

var tokenAcceso = new JwtSecurityTokenHandler().WriteToken(securityToken);

var respuesta = new RespuestaAtenticacionDto
{
IdUsuario = usuario.Id,
NombreUsuario = usuario.UserName,
Token = tokenAcceso
};
return Results.Ok(respuesta);
}).AllowAnonymous();

app.Run();

public class LoginDto
{
    public string NombreUsuario { get; set; }
    public string Contrasenia { get; set; }
}

public class RespuestaAtenticacionDto
{
    public string IdUsuario { get; set; }
    public string NombreUsuario { get; set; }
    public string Token { get; set; }
}