using PixelHouse.Turmas.DTOs;
using PixelHouse.Turmas.Models;

namespace PixelHouse.Turmas.Repositories
{
    public interface ITurmaRepository
    {
        Task<Turma?> ObterPorIdAsync(int idTurma);
        Task<Turma> CriarAsync(Turma turma);
    }

    public interface IPedidoRepository
    {
        Task<IReadOnlyList<Pedido>> ObterAprovadosPorTurmaAsync(int idTurma);
    }
}

namespace PixelHouse.Turmas.Services
{
    public interface ITurmaService
    {
        Task<TurmaDetalheDto?> ObterDetalheAsync(int idTurma);
        Task<IReadOnlyList<PedidoDto>> ObterPedidosAprovadosAsync(int idTurma);
        Task<Turma> CriarTurmaAsync(TurmaCreateDto dto);
    }
}
