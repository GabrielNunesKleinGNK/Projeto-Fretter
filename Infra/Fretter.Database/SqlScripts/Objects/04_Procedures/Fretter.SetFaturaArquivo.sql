CREATE OR ALTER PROCEDURE [Fretter].[SetFaturaArquivo]
(
	@FaturaArquivo [Fretter].[Tp_FaturaArquivo]        READONLY,
	@Criticas      [Fretter].[Tp_FaturaArquivoCritica] READONLY
)

AS BEGIN 
SET NOCOUNT ON;

DECLARE 
	@FaturaArquivoId INT,
	@TransportadorId INT

SELECT TOP 1
	@TransportadorId = TransportadorCnpj.Id_Transportador
FROM
	@FaturaArquivo Arquivo
INNER JOIN
	dbo.Tb_Adm_TransportadorCnpj TransportadorCnpj ON Arquivo.TransportadorCnpj = TransportadorCnpj.Cd_Cnpj

INSERT INTO Fretter.FaturaArquivo
(
	EmpresaId,
	NomeArquivo,
	UrlBlobStorage,
	QtdeRegistros,
	QtdeCriticas,
	ValorTotal,
	TransportadorId,
	Faturado,
	UsuarioCadastro 
)
SELECT
	EmpresaId,
	NomeArquivo,
	UrlBlobStorage,
	QtdeRegistros,
	QtdeCriticas,
	ValorTotal,
	@TransportadorId,
	Faturado,
	UsuarioCadastro 
FROM
	@FaturaArquivo

SELECT @FaturaArquivoId = SCOPE_IDENTITY()

IF EXISTS (SELECT 1 FROM @Criticas)
BEGIN
	INSERT INTO Fretter.FaturaArquivoCritica
	(
		FaturaArquivoId,
		Linha,
		Posicao,
		Descricao,
		UsuarioCadastro
	)
	SELECT
		@FaturaArquivoId,
		Linha,
		Posicao,
		Descricao,
		UsuarioCadastro
	FROM
		@Criticas
END

SELECT @FaturaArquivoId

END