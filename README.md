# Teste Técnico — Pixel House

Execute na raiz com `dotnet restore` e `dotnet run` (.NET 8). Swagger em `/swagger` no Development. Dados mockados em memória; sem banco real.

Decisões: Controller → Service → Repository, interfaces para DI, DTOs para entrada/saída e SP idempotente com `NOT EXISTS`.
