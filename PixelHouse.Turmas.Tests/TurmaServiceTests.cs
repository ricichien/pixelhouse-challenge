using PixelHouse.Turmas.DTOs;
using PixelHouse.Turmas.Models;
using PixelHouse.Turmas.Repositories;
using PixelHouse.Turmas.Services;
using Xunit;

namespace PixelHouse.Turmas.Tests;

// Fakes simples (sem precisar de biblioteca de mock) — implementam as
// mesmas interfaces que o InMemoryTurmaRepository/InMemoryPedidoRepository,
// só que com dados fixos, controlados pelo teste.
public class TurmaRepositoryFake : ITurmaRepository
{
    public Turma? Turma { get; set; }

    public Task<Turma?> ObterPorIdAsync(int idTurma)
        => Task.FromResult(Turma is not null && Turma.IdTurma == idTurma ? Turma : null);

    public Task<Turma> CriarAsync(Turma turma) => Task.FromResult(turma);
}

public class PedidoRepositoryFake : IPedidoRepository
{
    public List<Pedido> Pedidos { get; set; } = new();

    public Task<IReadOnlyList<Pedido>> ObterAprovadosPorTurmaAsync(int idTurma)
    {
        IReadOnlyList<Pedido> resultado = Pedidos
            .Where(p => p.IdTurma == idTurma && p.Status == "APROVADO")
            .ToList();

        return Task.FromResult(resultado);
    }
}

public class TurmaServiceTests
{
    // -------------------------------------------------------------
    // EXEMPLO PRONTO — estude este antes de tentar os TODOs abaixo.
    // -------------------------------------------------------------
    [Fact]
    public async Task ObterDetalheAsync_DeveSomarApenasPedidosAprovados()
    {
        // Arrange (preparar o cenário)
        var turmaRepo = new TurmaRepositoryFake
        {
            Turma = new Turma { IdTurma = 1, NomeTurma = "Teste", AnoFormatura = 2024, Status = "ATIVA" }
        };

        var pedidoRepo = new PedidoRepositoryFake
        {
            Pedidos = new List<Pedido>
            {
                new() { IdPedido = 1, IdTurma = 1, Valor = 100m, Status = "APROVADO" },
                new() { IdPedido = 2, IdTurma = 1, Valor = 50m,  Status = "APROVADO" },
                new() { IdPedido = 3, IdTurma = 1, Valor = 999m, Status = "CANCELADO" }, // não deve contar
            }
        };

        var service = new TurmaService(turmaRepo, pedidoRepo);

        // Act (executar o que está sendo testado)
        var resultado = await service.ObterDetalheAsync(1);

        // Assert (verificar o resultado)
        Assert.NotNull(resultado);
        Assert.Equal(2, resultado!.TotalPedidosAprovados);
        Assert.Equal(150m, resultado.ReceitaTotal);
    }

    // -------------------------------------------------------------
    // TODO 1 — complete você mesmo.
    // Objetivo: garantir que, quando a turma NÃO existe no repositório,
    // o serviço retorna null (e não lança exceção).
    // Dica: não precisa de PedidoRepositoryFake com dados nenhum,
    // só não cadastre nenhuma Turma no TurmaRepositoryFake.
    // -------------------------------------------------------------
    [Fact]
    public async Task ObterDetalheAsync_DeveRetornarNull_QuandoTurmaNaoExiste()
    {
        var turmaRepo = new TurmaRepositoryFake { Turma = null };
        var pedidoRepo = new PedidoRepositoryFake();

        var service = new TurmaService(turmaRepo, pedidoRepo);

        var resultado = await service.ObterDetalheAsync(99);

        Assert.Null(resultado);
    }

    // -------------------------------------------------------------
    // TODO 2 — complete você mesmo.
    // Objetivo: testar ObterPedidosAprovadosAsync — deve devolver só os
    // pedidos com Status = "APROVADO" da turma pedida, como PedidoDto.
    // Dica: reaproveite o padrão do exemplo pronto acima, mas chame
    // service.ObterPedidosAprovadosAsync(idTurma) em vez de ObterDetalheAsync.
    // -------------------------------------------------------------
    [Fact]
    public async Task ObterPedidosAprovadosAsync_DeveFiltrarSomenteAprovados()
    {
        var turmaRepo = new TurmaRepositoryFake
        {
            Turma = new Turma { IdTurma = 2, NomeTurma = "Teste de Pedidos", AnoFormatura = 2024, Status = "ATIVA" }
        };

        var pedidoRepo = new PedidoRepositoryFake
        {
            Pedidos = new List<Pedido>
            {
                new() { IdPedido = 1, IdTurma = 2, Valor = 100m, Status = "APROVADO" },
                new() { IdPedido = 2, IdTurma = 2, Valor = 50m,  Status = "APROVADO" },
                new() { IdPedido = 3, IdTurma = 2, Valor = 999m, Status = "CANCELADO" },
            }
        };

        var service = new TurmaService(turmaRepo, pedidoRepo);

        var resultado = await service.ObterPedidosAprovadosAsync(2);

        Assert.Equal(2, resultado.Count);
        Assert.Contains(resultado, p => p.IdPedido == 1);
        Assert.Contains(resultado, p => p.IdPedido == 2);
    }
    }