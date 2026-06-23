namespace PixelHouse.Turmas.DTOs;

public class PedidoDto
{
    public int IdPedido { get; set; }
    public string Produto { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public DateTime DataPedido { get; set; }
}
