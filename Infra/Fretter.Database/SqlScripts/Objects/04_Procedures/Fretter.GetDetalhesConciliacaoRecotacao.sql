CREATE OR ALTER PROCEDURE Fretter.GetDetalhesConciliacaoRecotacao
(
	@ListaConciliacoesId dbo.Tp_Int READONLY
)

AS
BEGIN

SET NOCOUNT ON;

CREATE TABLE #Conciliacoes
(
	ConciliacaoId INT
)

INSERT INTO 
	#Conciliacoes
SELECT DISTINCT
	Id
FROM
	@ListaConciliacoesId ListaId
INNER JOIN
	Fretter.Conciliacao Conciliacao (NOLOCK) ON ListaId.Id = Conciliacao.ConciliacaoId
INNER JOIN
	Fretter.ContratoTransportador Contrato (NOLOCK) ON Conciliacao.EmpresaId = Contrato.EmpresaId AND
												Conciliacao.TransportadorId = Contrato.TransportadorId AND
												Contrato.Ativo = 1 AND
												Contrato.VigenciaInicial <= GETDATE() AND
												Contrato.VigenciaFinal >= GETDATE() AND
												Contrato.RecotaPesoTransportador = 1

SELECT 
	Recotacao.ConciliacaoRecotacaoId,
	Recotacao.ConciliacaoId,
	ISNULL(Recotacao.ValorCustoFrete, 0) ValorCustoFrete,
	ISNULL(Recotacao.ValorCustoAdicional, 0) ValorCustoAdicional,
	ISNULL(Recotacao.ValorCustoReal, 0) ValorCustoReal,
	ISNULL(Recotacao.JsonRetornoRecotacao, '') JsonRetornoRecotacao,
	Tabela.Cd_Id TabelaId,
	Tabela.Ds_Nome TabelaDescricao,
	Recotacao.DataCadastro,
	Recotacao.DataProcessamento
FROM
	#Conciliacoes
INNER JOIN
	Fretter.ConciliacaoRecotacao Recotacao (NOLOCK) ON Recotacao.ConciliacaoId = #Conciliacoes.ConciliacaoId AND
														Recotacao.Processado = 1 AND
														Recotacao.Sucesso = 1
LEFT JOIN
	dbo.Tb_MF_Tabela Tabela (NOLOCK) ON Recotacao.TabelaId = Tabela.Cd_Id


DROP TABLE #Conciliacoes

END