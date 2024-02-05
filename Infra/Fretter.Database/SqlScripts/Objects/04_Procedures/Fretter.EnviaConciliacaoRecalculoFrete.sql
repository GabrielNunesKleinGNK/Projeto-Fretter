CREATE OR ALTER PROCEDURE [Fretter].[EnviaConciliacaoRecalculoFrete]
(
	@ListaConciliacoesId dbo.Tp_Int READONLY,
	@UsuarioId INT,
	@ParametrosJson VARCHAR(MAX)
)

AS

BEGIN

SET NOCOUNT ON;

CREATE TABLE #Conciliacoes
(
	ConciliacaoId INT
)


INSERT INTO #Conciliacoes
SELECT Id FROM @ListaConciliacoesId

UPDATE
	Fretter.Conciliacao
SET
	ConciliacaoStatusId = 4
FROM
	Fretter.Conciliacao
INNER JOIN
	#Conciliacoes ON Fretter.Conciliacao.ConciliacaoId = #Conciliacoes.ConciliacaoId

DECLARE @Total INT

SELECT @Total = COUNT(1) FROM #Conciliacoes 

IF (@Total > 0)
BEGIN
	INSERT INTO Fretter.ConciliacaoTransacao
	(
		Descricao,
		ParametroJson,
		Quantidade,
		UsuarioCadastro
	)
	VALUES
	(
		'Enviado para Recalculo de Frete',
		@ParametrosJson,
		@Total,
		@UsuarioId
	)
END

DROP TABLE #Conciliacoes

END