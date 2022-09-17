using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjetoFinal.M06.Core.Interface;
using ProjetoFinal.M06.Core.Service;
using ProjetoFinal.M06.Filters;
using ProjetoFinal.M06.Infra.Data;
using ProjetoFinal.M06.Infra.Data.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
builder.Services.AddScoped<IEventReservationRepository, EventReservationRepository>();
builder.Services.AddScoped<ICityEventRepository, CityEventRepository>();
builder.Services.AddScoped<IEventReservationService, EventReservationService>();
builder.Services.AddScoped<ICityEventService, CityEventService>();
builder.Services.AddScoped<IConnectionDataBase, ConnectionDataBase>();

//Add filter service
builder.Services.AddScoped<CheckIdEventActionFilter_ER>();
builder.Services.AddScoped<CheckIdEventActionFilter_CE>();
builder.Services.AddScoped<CheckDateActionFilter_CE>();
builder.Services.AddScoped<CheckPriceValuesActionFilter_CE>();
builder.Services.AddScoped<CheckIdReservationActionFilter>();

builder.Services.AddMvc(options =>
{
    //aqui você acaba de add um filtro GLOBAL para todas as controllers/metodos, para TUDO.
    options.Filters.Add<GeneralExceptionFilter>();

});


//Autorização e Autenticação
var key = Encoding.ASCII.GetBytes(builder.Configuration["secretKey"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = "APIClientes.com",
            ValidAudience = "APIEvents.com"
        };
    });

builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put *ONLY* your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
