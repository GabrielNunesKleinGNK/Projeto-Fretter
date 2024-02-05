CREATE OR ALTER PROCEDURE [Fretter].[SetFaturaConciliacaoReenvio]
(
	@FaturaConciliacoesReenvio Fretter.Tp_FaturaConciliacaoReenvio READONLY
)
AS

UPDATE 
	Fretter.FaturaConciliacao
SET
	Fretter.FaturaConciliacao.DataAlteracao = GETDATE()
FROM
	@FaturaConciliacoesReenvio Cr
INNER JOIN
	Fretter.FaturaConciliacao Fc (NOLOCK) ON Cr.FaturaConciliacaoId = Fc.FaturaConciliacaoId AND						 
											 Cr.FaturaId = Fc.FaturaId

INSERT INTO Fretter.FaturaConciliacaoReenvio
(
	FaturaConciliacaoId,
	FaturaId,
	ConciliacaoId,
	UsuarioCadastro,
	DataCadastro
)
SELECT
	FaturaConciliacaoId,
	FaturaId,
	ConciliacaoId,
	UsuarioCadastro,
	GETDATE()
FROM
	@FaturaConciliacoesReenvio