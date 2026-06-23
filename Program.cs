using PixelHouse.Turmas.Repositories;
using PixelHouse.Turmas.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositórios mockados em memória — Singleton para simular persistência
// dos dados durante a execução. Trocar por implementação real (Dapper/EF
// Core) bastaria registrar a nova classe aqui, sem tocar em Controller/Service.
builder.Services.AddSingleton<ITurmaRepository, InMemoryTurmaRepository>();
builder.Services.AddSingleton<IPedidoRepository, InMemoryPedidoRepository>();
builder.Services.AddScoped<ITurmaService, TurmaService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
