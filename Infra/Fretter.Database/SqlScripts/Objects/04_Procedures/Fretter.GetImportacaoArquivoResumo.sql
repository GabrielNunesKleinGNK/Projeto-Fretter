CREATE OR ALTER PROCEDURE [Fretter].[GetImportacaoArquivoResumo]
(
	@DataInicio DATETIME,
	@DataTermino DATETIME,
	@Nome VARCHAR(128) = NULL,
	@EmpresaId INTEGER,
	@TransportadorId INTEGER = 0,
	@ImportacaoArquivoStatusId INTEGER = 0
)

AS BEGIN 
SET NOCOUNT ON;

DECLARE 
	@QtdeTotal INTEGER,
	@QtdeConcluido INTEGER,
	@QtdePendente INTEGER,
	@QtdeFalha INTEGER,
	@QtdeComCritica INTEGER 

DECLARE @ImportacaoArquivoPorEmpresa TABLE
(
	ImportacaoArquivoId INTEGER,
	ImportacaoArquivoStatusId INTEGER,
	TemCriticas BIT
)

INSERT INTO @ImportacaoArquivoPorEmpresa
(
	ImportacaoArquivoId,
	ImportacaoArquivoStatusId,
	TemCriticas
)
SELECT DISTINCT
	Arquivo.ImportacaoArquivoId,
	Arquivo.ImportacaoArquivoStatusId,
	CASE WHEN Critica.ImportacaoArquivoCriticaId IS NOT NULL THEN 1 ELSE 0 END
FROM
	Fretter.ImportacaoArquivo Arquivo (NOLOCK)
LEFT JOIN
	Fretter.ImportacaoArquivoCritica Critica (NOLOCK) ON Arquivo.ImportacaoArquivoId = Critica.ImportacaoArquivoId
WHERE
	EmpresaId = @EmpresaId AND
	Arquivo.DataCadastro BETWEEN @DataInicio AND @DataTermino AND
	(@Nome IS NULL OR Arquivo.Nome LIKE '%' + @Nome + '%') AND
	(@TransportadorId = 0 OR @TransportadorId = Arquivo.TransportadorId) AND
	(@ImportacaoArquivoStatusId = 0 OR @ImportacaoArquivoStatusId = Arquivo.ImportacaoArquivoStatusId)

SELECT
	@QtdeTotal = COUNT(1),
	@QtdeConcluido = COUNT(CASE WHEN ImportacaoArquivoStatusId = 3 THEN 1 END),
	@QtdePendente = COUNT(CASE WHEN ImportacaoArquivoStatusId IN (1,2) THEN 1 END),
	@QtdeFalha = COUNT(CASE WHEN ImportacaoArquivoStatusId IN (4,5) THEN 1 END),
	@QtdeComCritica = SUM(CASE WHEN TemCriticas = 1 THEN 1 END)
FROM
	@ImportacaoArquivoPorEmpresa


SELECT
	@QtdeTotal 'Total',
	@QtdeConcluido 'Concluido',
	@QtdePendente 'Pendente',
	@QtdeFalha 'Falha',
	@QtdeComCritica 'Critica'
END