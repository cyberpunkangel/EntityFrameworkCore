using EFCorePeliculas;
using EFCorePeliculas.CompiledModels;
using EFCorePeliculas.Servicios;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opciones => 
  opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Películas API", Version = "v1" });
    // opcional: para que la UI sugiera la base correcta
    c.AddServer(new OpenApiServer {
      Url = "https://cyberpunkangel.onthewifi.com/efcorepeliculas"
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
{
    opciones.UseSqlServer(connectionString, sqlServer => sqlServer.UseNetTopologySuite());
    opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //opciones.UseModel(ApplicationDbContextModel.Instance);
    //opciones.UseLazyLoadingProxies();
}
    );

//builder.Services.AddDbContext<ApplicationDbContext>();

builder.Services.AddScoped<IActualizadorObservableCollection, ActualizadorObservableCollection>();
builder.Services.AddScoped<IServicioUsuario, ServicioUsuario>();
builder.Services.AddScoped<IEventosDbContext, EventosDbContext>();
builder.Services.AddSingleton<Singleton>();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    applicationDbContext.Database.Migrate();
//}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    // La spec en /swagger/v1/swagger.json
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Películas API V1");
    // Y la UI en /swagger/index.html
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
