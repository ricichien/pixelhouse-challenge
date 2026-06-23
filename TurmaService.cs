using PixelHouse.Turmas.DTOs;
using PixelHouse.Turmas.Models;
using PixelHouse.Turmas.Repositories;

namespace PixelHouse.Turmas.Services;

/// <summary>
/// Concentra a regra de negócio para que o Controller fique responsável
/// apenas por roteamento/HTTP (request -> service -> response).
/// </summary>
public class TurmaService : ITurmaService
{
    private readonly ITurmaRepository _turmaRepository;
    private readonly IPedidoRepository _pedidoRepository;

    public TurmaService(ITurmaRepository turmaRepository, IPedidoRepository pedidoRepository)
    {
        _turmaRepository = turmaRepository;
        _pedidoRepository = pedidoRepository;
    }

    public async Task<TurmaDetalheDto?> ObterDetalheAsync(int idTurma)
    {
        var turma = await _turmaRepository.ObterPorIdAsync(idTurma);
        if (turma is null)
            return null; // Controller decide o que fazer (404)

        var pedidosAprovados = await _pedidoRepository.ObterAprovadosPorTurmaAsync(idTurma);

        return new TurmaDetalheDto
        {
            IdTurma = turma.IdTurma,
            NomeTurma = turma.NomeTurma,
            AnoFormatura = turma.AnoFormatura,
            Status = turma.Status,
            TotalPedidosAprovados = pedidosAprovados.Count,
            ReceitaTotal = pedidosAprovados.Sum(p => p.Valor)
        };
    }

    public async Task<IReadOnlyList<PedidoDto>> ObterPedidosAprovadosAsync(int idTurma)
    {
        var pedidos = await _pedidoRepository.ObterAprovadosPorTurmaAsync(idTurma);

        return pedidos
            .Select(p => new PedidoDto
            {
                IdPedido = p.IdPedido,
                Produto = p.Produto,
                Valor = p.Valor,
                DataPedido = p.DataPedido
            })
            .ToList();
    }

    public async Task<Turma> CriarTurmaAsync(TurmaCreateDto dto)
    {
        var turma = new Turma
        {
            IdEmpresa = dto.IdEmpresa,
            NomeTurma = dto.NomeTurma,
            AnoFormatura = dto.AnoFormatura,
            Status = "ATIVA"
        };

        return await _turmaRepository.CriarAsync(turma);
    }
}
