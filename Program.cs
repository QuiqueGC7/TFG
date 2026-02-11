using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TFG.Controllers;
using TFG.Repositories;
using TFG.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
        };
    });


var connectionString = builder.Configuration.GetConnectionString("MambaDB");

builder.Services.AddScoped<IEquipoRepository, EquipoRepository>();
builder.Services.AddScoped<IJugadoresARepository, JugadoresARepository>();
builder.Services.AddScoped<IJugadoresNacRepository, JugadoresNacRepository>();
builder.Services.AddScoped<IPatrocinadoresRepository, PatrocinadoresRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEquipoService, EquipoService>();
builder.Services.AddScoped<IJugadoresAService, JugadoresAService>();
builder.Services.AddScoped<IJugadoresNacService, JugadoresNacService>();
builder.Services.AddScoped<IPatrocinadoresService, PatrocinadoresService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

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



//PlatoPrincipalController.InicializarDatos();
app.Run();
