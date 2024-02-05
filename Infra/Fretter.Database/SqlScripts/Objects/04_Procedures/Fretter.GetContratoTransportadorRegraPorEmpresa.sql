Create Or Alter Procedure Fretter.GetContratoTransportadorRegraPorEmpresa
(    
	@EmpresaId					  Int = 0    
	,@TransportadorId			  Int = 0
	,@OcorrenciaEmpresaItemId	  Int = 0
	,@ConciliacaoTipoId			  Int = 0
	
)    
As Begin    
	Set Nocount On;    
    
	SELECT 
		Id = CTR.ContratoTransportadorRegraId,
		OcorrenciaId = OcorrenciaEmpresaItemId, 
		Ocorrencia = OI.Nm_Sigla + ' - ' + OI.Ds_Ocorrencia,
		EmpresaTransportadorConfigId, 
		TransportadorId, 
		TipoCondicao = ContratoTransportadorRegraTipoId, 
		Operacao = Acrescimo, 
		Valor, 
		etc.Id_Empresa,
		ConciliacaoTipoId = CTR.ConciliacaoTipoId		
	FROM Fretter.ContratoTransportadorRegra AS CTR	WITH (NOLOCK)    
	JOIN Tb_Adm_EmpresaTransportadorConfig	AS ETC	WITH (NOLOCK) ON CTR.EmpresaTransportadorConfigId = ETC.Cd_Id    
	Join Tb_Edi_OcorrenciaEmpresaItem		AS OI	WITH (NOLOCK) ON OI.Cd_Id = OcorrenciaEmpresaItemId
	WHERE ETC.Id_Empresa = @EmpresaId 
	  AND (CTR.TransportadorId = @TransportadorId Or @TransportadorId = 0)
	  AND (CTR.OcorrenciaEmpresaItemId = @OcorrenciaEmpresaItemId Or @OcorrenciaEmpresaItemId = 0)
	  AND (CTR.ConciliacaoTipoId = @ConciliacaoTipoId Or @ConciliacaoTipoId = 0)
	  AND Ativo = 1  
End  