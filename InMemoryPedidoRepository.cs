using PixelHouse.Turmas.Models;

namespace PixelHouse.Turmas.Repositories;

public class InMemoryPedidoRepository : IPedidoRepository
{
    private readonly List<Pedido> _pedidos;

    public InMemoryPedidoRepository()
    {
        _pedidos = new List<Pedido>
        {
            new() { IdPedido = 100, IdTurma = 1, Produto = "ALBUM_PREMIUM", Valor = 450.00m, DataPedido = DateTime.Today.AddDays(-10), Status = "APROVADO" },
            new() { IdPedido = 101, IdTurma = 1, Produto = "ALBUM_PADRAO",  Valor = 280.00m, DataPedido = DateTime.Today.AddDays(-8),  Status = "APROVADO" },
            new() { IdPedido = 102, IdTurma = 1, Produto = "FOTO_AVULSA",  Valor = 35.00m,  DataPedido = DateTime.Today.AddDays(-5),  Status = "PENDENTE" },
            new() { IdPedido = 103, IdTurma = 1, Produto = "ALBUM_PADRAO",  Valor = 280.00m, DataPedido = DateTime.Today.AddDays(-2),  Status = "CANCELADO" },
            new() { IdPedido = 104, IdTurma = 2, Produto = "ALBUM_PREMIUM", Valor = 500.00m, DataPedido = DateTime.Today.AddDays(-20), Status = "APROVADO" },
        };
    }

    public Task<IReadOnlyList<Pedido>> ObterAprovadosPorTurmaAsync(int idTurma)
    {
        IReadOnlyList<Pedido> aprovados = _pedidos
            .Where(p => p.IdTurma == idTurma && p.Status == "APROVADO")
            .ToList();

        return Task.FromResult(aprovados);
    }
}
