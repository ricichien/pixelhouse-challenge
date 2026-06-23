namespace PixelHouse.Turmas.DTOs;

/// <summary>
/// Resposta do endpoint GET /api/turmas/{id}.
/// Os nomes das propriedades viram camelCase no JSON automaticamente
/// (padrão do System.Text.Json no ASP.NET Core), conforme contrato pedido:
/// idTurma, nomeTurma, anoFormatura, status, totalPedidosAprovados, receitaTotal.
/// </summary>
public class TurmaDetalheDto
{
    public int IdTurma { get; set; }
    public string NomeTurma { get; set; } = string.Empty;
    public int AnoFormatura { get; set; }
    public string Status { get; set; } = string.Empty;
    public int TotalPedidosAprovados { get; set; }
    public decimal ReceitaTotal { get; set; }
}
