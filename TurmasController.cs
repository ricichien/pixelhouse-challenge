using Microsoft.AspNetCore.Mvc;
using PixelHouse.Turmas.DTOs;
using PixelHouse.Turmas.Services;

namespace PixelHouse.Turmas.Controllers;

[ApiController]
[Route("api/turmas")]
public class TurmasController : ControllerBase
{
    private readonly ITurmaService _turmaService;

    public TurmasController(ITurmaService turmaService)
    {
        _turmaService = turmaService;
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TurmaDetalheDto>> ObterPorId(int id)
    {
        var detalhe = await _turmaService.ObterDetalheAsync(id);
        return detalhe is null ? NotFound() : Ok(detalhe);
    }

    [HttpGet("{id:int}/pedidos")]
    public async Task<ActionResult<IReadOnlyList<PedidoDto>>> ObterPedidosAprovados(int id)
    {
        var pedidos = await _turmaService.ObterPedidosAprovadosAsync(id);
        return Ok(pedidos);
    }

    [HttpPost]
    public async Task<IActionResult> CriarTurma([FromBody] TurmaCreateDto dto)
    {
        var turma = await _turmaService.CriarTurmaAsync(dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = turma.IdTurma }, turma);
    }
}
