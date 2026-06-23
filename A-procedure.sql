-- =====================================================================
-- sp_ConsolidarVendasMensais
-- Consolida, na tabela Relatorio_VendasMensal, o volume de pedidos e a
-- receita total do MES CORRENTE, por empresa.
--
-- Regras aplicadas:
--   1. Apenas empresas com Status = 'ATIVO'
--   2. Apenas turmas com Status = 'ATIVA'
--   3. Apenas pedidos com Status = 'APROVADO', dentro do mes corrente
--   4. TotalPedidos = COUNT de pedidos aprovados
--   5. ReceitaTotal = SUM dos valores dos pedidos aprovados
--   6. Idempotente: nao duplica registro para o mesmo IdEmpresa + MesReferencia
--   7. DataCarga = data/hora atual do sistema
-- =====================================================================

CREATE OR ALTER PROCEDURE sp_ConsolidarVendasMensais
AS
BEGIN
    SET NOCOUNT ON;

    -- MesReferencia = primeiro dia do mes corrente (ex: 2025-05-01)
    DECLARE @MesReferencia DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);

    -- Limites [InicioMes, FimMes) usados para filtrar DataPedido sem
    -- truncar a parte de hora/minuto (evita problemas de performance
    -- com funcoes aplicadas em cima da coluna, ex: CONVERT/DATEPART na coluna)
    DECLARE @InicioMes DATETIME = CAST(@MesReferencia AS DATETIME);
    DECLARE @FimMes    DATETIME = DATEADD(MONTH, 1, @InicioMes);

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Relatorio_VendasMensal
            (IdEmpresa, MesReferencia, RazaoSocial, TotalPedidos, ReceitaTotal, DataCarga)
        SELECT
            e.IdEmpresa,
            @MesReferencia,
            e.RazaoSocial,
            COUNT(p.IdPedido)   AS TotalPedidos,
            SUM(p.Valor)        AS ReceitaTotal,
            GETDATE()           AS DataCarga
        FROM Empresas e
        INNER JOIN Turmas t
                ON t.IdEmpresa = e.IdEmpresa
               AND t.Status = 'ATIVA'
        INNER JOIN Pedidos p
                ON p.IdTurma = t.IdTurma
               AND p.Status = 'APROVADO'
               AND p.DataPedido >= @InicioMes
               AND p.DataPedido <  @FimMes
        WHERE e.Status = 'ATIVO'
          -- Regra 5: evita duplicidade para o mesmo IdEmpresa + MesReferencia
          AND NOT EXISTS (
                SELECT 1
                FROM Relatorio_VendasMensal r
                WHERE r.IdEmpresa     = e.IdEmpresa
                  AND r.MesReferencia = @MesReferencia
          )
        GROUP BY e.IdEmpresa, e.RazaoSocial;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW; -- repropaga o erro original (mensagem, severidade e linha)
    END CATCH
END;
GO

-- Exemplo de execucao:
-- EXEC sp_ConsolidarVendasMensais;
