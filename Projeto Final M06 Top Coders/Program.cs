using ProjetoFinal.M06.Core.Interface;
using ProjetoFinal.M06.Core.Service;
using ProjetoFinal.M06.Infra.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddScoped<IEventReservationRepository, EventReservationRepository>();
//builder.Services.AddScoped<ICityEventRepository, CityEventRepository>();
//builder.Services.AddScoped<IEventReservationService, EventReservationService>();
//builder.Services.AddScoped<ICityEventService, CityEventService>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
