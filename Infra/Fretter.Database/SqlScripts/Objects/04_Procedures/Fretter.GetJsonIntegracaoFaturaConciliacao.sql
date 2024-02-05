CREATE OR ALTER PROCEDURE Fretter.GetJsonIntegracaoFaturaConciliacao
(
	@EmpresaIntegracaoItemDetalheId INT
)

AS

BEGIN

SET NOCOUNT OFF;

SELECT
	EmpresaIntegracaoItemDetalheId  = Cd_Id,
	JsonEnvio                       = ISNULL(Ds_JsonEnvio, ''),
	JsonRetorno                     = ISNULL(Ds_JsonRetorno, '')
FROM
	Tb_Adm_EmpresaIntegracaoItemDetalhe (NOLOCK)
WHERE
	Cd_Id = @EmpresaIntegracaoItemDetalheId

END