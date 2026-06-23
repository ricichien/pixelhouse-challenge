using System.Collections.Concurrent;
using PixelHouse.Turmas.Models;

namespace PixelHouse.Turmas.Repositories;

/// <summary>
/// Repositório mockado em memória. Registrado como Singleton no DI para que
/// os dados "persistam" entre requisições durante a execução da aplicação.
/// Em produção, esta classe seria substituída por uma implementação que
/// acessa o SQL Server (ex: via Dapper ou EF Core), sem precisar alterar
/// o Controller/Service, já que ambos dependem apenas da interface.
/// </summary>
public class InMemoryTurmaRepository : ITurmaRepository
{
    private readonly ConcurrentDictionary<int, Turma> _turmas;
    private int _proximoId;

    public InMemoryTurmaRepository()
    {
        _turmas = new ConcurrentDictionary<int, Turma>
        {
            [1] = new Turma
            {
                IdTurma = 1,
                IdEmpresa = 10,
                NomeTurma = "Engenharia Civil 2024",
                AnoFormatura = 2024,
                Status = "ATIVA"
            },
            [2] = new Turma
            {
                IdTurma = 2,
                IdEmpresa = 10,
                NomeTurma = "Direito 2023",
                AnoFormatura = 2023,
                Status = "ENCERRADA"
            }
        };

        _proximoId = _turmas.Keys.Max() + 1;
    }

    public Task<Turma?> ObterPorIdAsync(int idTurma)
    {
        _turmas.TryGetValue(idTurma, out var turma);
        return Task.FromResult(turma);
    }

    public Task<Turma> CriarAsync(Turma turma)
    {
        turma.IdTurma = Interlocked.Increment(ref _proximoId);
        _turmas[turma.IdTurma] = turma;
        return Task.FromResult(turma);
    }
}
