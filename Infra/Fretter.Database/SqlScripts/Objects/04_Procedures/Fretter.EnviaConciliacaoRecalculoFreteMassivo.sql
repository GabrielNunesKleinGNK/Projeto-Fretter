CREATE OR ALTER PROCEDURE [Fretter].[EnviaConciliacaoRecalculoFreteMassivo]
(
	@EmpresaId	     INT = 0,
	@DataInicial	 DATETIME = NULL,
	@DataFinal	     DATETIME = NULL,
	@TransportadorId INT = 0,
	@StatusId	     INT = 0, 
	@FaturaId	     INT = 0,
	@CodigoEntrega	 VARCHAR(100) = NULL,
	@CodigoPedido	 VARCHAR(100) = NULL,
	@CodigoDanfe	 VARCHAR(44) = NULL,
	@ListSize        INT,
	@ParametrosJson  VARCHAR(MAX) = NULL,
	@UsuarioId       INT
)

AS

BEGIN

SET NOCOUNT ON;

CREATE TABLE #ListaIdConciliacoes
(
	CodigoConciliacao			Bigint,
	CodigoEntrega				Varchar(100),
	CodigoPedido				Varchar(100),
	Transportador				Varchar(512),
	ValorCustoFrete				Decimal(10,4),
	ValorCustoAdicional			Decimal(10,4),
	ValorCustoReal				Decimal(10,4),
	QtdTentativas				Int,
	PossuiDivergenciaPeso		Bit,
	PossuiDivergenciaTarifa		Bit,
	StatusConciliacao			Varchar(256),
	StatusConciliacaoId			Int,
	DataCadastro				Datetime,
	DataEmissao					Datetime,
	QtdRegistrosQuery			Int,
	Finalizado					Datetime,
	ProcessadoIndicador			Bit,
	EntregaPeso					Decimal(10,4),
	EntregaAltura				Decimal(10,4),
	EntregaComprimento			Decimal(10,4),
	EntregaLargura				Decimal(10,4),
	EntregaValorDeclarado		Decimal(10,4),
	MicroServicoId				Int,
	MicroServico				Varchar(100),
	CanalId						Int,
	CanalCNPJ					Varchar(32),
	CanalVendaId				Int,
	CanalVenda					Varchar(200),
	JsonValoresRecotacao		Varchar(Max),
	JsonValoresCte				Varchar(Max),
	TipoCobranca				Varchar(64)
)

INSERT INTO #ListaIdConciliacoes
EXEC Fretter.GetRelatorioDetalhado 0, @ListSize, 'Asc', @EmpresaId, @DataInicial, @DataFinal, @TransportadorId, @StatusId, @FaturaId, @CodigoEntrega, @CodigoPedido, @CodigoDanfe

DELETE FROM #ListaIdConciliacoes WHERE StatusConciliacaoId IN (1, 4)

UPDATE
	Fretter.Conciliacao
SET
	ConciliacaoStatusId = 4
FROM
	Fretter.Conciliacao
INNER JOIN
	#ListaIdConciliacoes ON #ListaIdConciliacoes.CodigoConciliacao = Fretter.Conciliacao.ConciliacaoId 

DECLARE @Total INT

SELECT @Total = COUNT(1) FROM #ListaIdConciliacoes 

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

DROP TABLE #ListaIdConciliacoes

END