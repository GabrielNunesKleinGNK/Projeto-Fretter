CREATE  OR ALTER   Procedure [Fretter].[GetPedidoEntrega]	
(
	@ListCdEntrega Fretter.Tp_CdEntregaList READONLY
)
AS BEGIN   
SET NOCOUNT ON;           

SELECT 
	e.Cd_Entrega CdEntrega, 
	e.cd_Id IdEntrega, 
	e.Id_Empresa IdEmpresa, 
	e.Id_Transportador IdTransportador, 
	e.cd_danfe Danfe ,
	e.Cd_DanfeDRS DanfeDRS,
	e.Cd_NotaFiscal NotaFiscal,
	e.Cd_NotaFiscalDRS NotaFiscalDRS,
	c.Cd_Cnpj CnpjCanal,
	cdrs.Cd_Cnpj as CnpjCanalDRS,
	ISNULL(ppi.PedidoPendenteIntegracaoId , 0)  PedidoPendenteIntegracaoId,
	m.Ds_Descricao as DescricaoMicroServico,
	e.Cd_Sro Sro,
	e.Ds_Ocorrencia StatusOcorrencia,
	e.Dt_Ocorrencia DataOcorrencia
FROM @ListCdEntrega					l
LEFT JOIN [dbo].[Tb_Edi_Entrega]	e (Nolock) On l.Entrega = e.Cd_Entrega
left join [Fretter].[PedidoPendenteIntegracao]  ppi (Nolock)On ppi.TransportadorId = e.Id_Transportador
left join [dbo].[Tb_Adm_Canal]			c	 (Nolock) On c.Cd_Id = e.Id_Canal
left join [dbo].[Tb_Adm_Canal]			cdrs (Nolock) On cdrs.Cd_Id = e.Id_CanalDRS
left join tb_adm_microservico			m	 (Nolock) On e.id_microservico = m.cd_id
end

GO