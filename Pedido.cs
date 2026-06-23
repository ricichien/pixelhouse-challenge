namespace PixelHouse.Turmas.Models;

public class Pedido
{
    public int IdPedido { get; set; }
    public int IdTurma { get; set; }
    public DateTime DataPedido { get; set; }
    public string Produto { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string Status { get; set; } = "PENDENTE"; // APROVADO, PENDENTE, CANCELADO
}
