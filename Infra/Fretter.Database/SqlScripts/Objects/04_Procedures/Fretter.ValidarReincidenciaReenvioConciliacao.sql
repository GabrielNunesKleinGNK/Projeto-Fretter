CREATE OR ALTER PROCEDURE [Fretter].[ValidarReincidenciaReenvioConciliacao]
(
	@ListaFaturaConciliacaoId dbo.Tp_BigInt READONLY
)
AS

SELECT TOP 1
	Fc.FaturaConciliacaoId
FROM
	@ListaFaturaConciliacaoId Lfc
INNER JOIN
	Fretter.FaturaConciliacao Fc (NOLOCK) ON Lfc.Cd_Id = Fc.FaturaConciliacaoId
WHERE
	DATEDIFF(MINUTE, DATEADD(MINUTE, 30, Fc.DataAlteracao), GETDATE()) <= 0