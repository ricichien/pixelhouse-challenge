using System.ComponentModel.DataAnnotations;

namespace PixelHouse.Turmas.DTOs;

/// <summary>
/// Dados recebidos para criar uma nova turma.
/// Validado automaticamente pelo [ApiController] (retorna 400 se inválido).
/// </summary>
public class TurmaCreateDto
{
    [Required]
    public int IdEmpresa { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string NomeTurma { get; set; } = string.Empty;

    [Range(2000, 2100)]
    public int AnoFormatura { get; set; }
}
