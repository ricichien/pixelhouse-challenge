namespace PixelHouse.Turmas.Models;

public class Turma
{
    public int IdTurma { get; set; }
    public int IdEmpresa { get; set; }
    public string NomeTurma { get; set; } = string.Empty;
    public int AnoFormatura { get; set; }
    public string Status { get; set; } = "ATIVA"; // ATIVA, ENCERRADA
}
