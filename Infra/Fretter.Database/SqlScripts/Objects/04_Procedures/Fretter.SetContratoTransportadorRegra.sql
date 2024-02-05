Create Or Alter Procedure Fretter.SetContratoTransportadorRegra
(
	@OcorrenciaEmpresaItemId			Int = 0
	,@EmpresaId							Int = 0
	,@TransportadorId					Int = 0
	,@ContratoTransportadorRegraTipoId	Int = 0
	,@Acrescimo							Bit = 0
	,@UsuarioAlteracao					Int = 0
	,@Valor								Decimal(20,4) = 0
	,@Ativo								bit = 1
	,@ConciliacaoTipoId					Int = null	

)
As Begin
	Set Nocount On;
	Declare @EmpresaTransportadorConfigId	Int = 0;

	Select Top 1 @EmpresaTransportadorConfigId = Cd_Id
	From dbo.Tb_Adm_EmpresaTransportadorConfig (nolock)
	Where Id_Transportador	= @TransportadorId 
		And Id_Empresa		= @EmpresaId 
		And Id_TipoServico	= 1
	Order By Dt_Inclusao Desc;

	;WITH A AS
	(
		SELECT 
			*
		From Fretter.ContratoTransportadorRegra		Cr(Nolock)
		Join dbo.Tb_Adm_EmpresaTransportadorConfig	Co(Nolock) on Cr.EmpresaTransportadorConfigId = Co.Cd_Id
		WHERE co.Id_Transportador = @TransportadorId 
			  And co.Id_Empresa = @EmpresaId 
			  And co.Id_TipoServico = 1 
			  And cr.OcorrenciaEmpresaItemId = @OcorrenciaEmpresaItemId
	)

	MERGE A AS E  
    USING
	(
		Select 
			 OcorrenciaEmpresaItemId			= @OcorrenciaEmpresaItemId			
			,EmpresaId							= @EmpresaId							
			,TransportadorId					= @TransportadorId
			,ContratoTransportadorRegraTipoId	= @ContratoTransportadorRegraTipoId	
			,Acrescimo							= @Acrescimo							
			,UsuarioAlteracao					= @UsuarioAlteracao					
			,Valor								= @Valor								
			,Ativo								= @Ativo
			,ConciliacaoTipoId					= @ConciliacaoTipoId			
		
	) AS T On 
	(	
				E.Id_Transportador = @TransportadorId 
			  And E.Id_Empresa = @EmpresaId 
			  And E.Id_TipoServico = 1 
			  And E.OcorrenciaEmpresaItemId = @OcorrenciaEmpresaItemId
	)
	WHEN MATCHED THEN    
       Update Set 
	  		 E.DataAlteracao					= GETDATE()
			,E.UsuarioAlteracao					= @UsuarioAlteracao
			,ContratoTransportadorRegraTipoId	= @ContratoTransportadorRegraTipoId
			,Acrescimo							= @Acrescimo
			,Valor								= @Valor
			,E.Ativo							= @Ativo	
			,E.ConciliacaoTipoId				= @ConciliacaoTipoId					
    WHEN NOT MATCHED THEN   
		Insert
		(
			TransportadorId
			,OcorrenciaEmpresaItemId
			,EmpresaTransportadorConfigId
			,ContratoTransportadorRegraTipoId
			,Acrescimo
			,Valor
			,DataCadastro
			,UsuarioCadastro	
			,Ativo	
			,ConciliacaoTipoId			
		)
		Values
		(
			 T.TransportadorId
			,T.OcorrenciaEmpresaItemId
			,@EmpresaTransportadorConfigId
			,T.ContratoTransportadorRegraTipoId
			,T.Acrescimo
			,T.Valor
			,GETDATE()
			,T.UsuarioAlteracao
			,1
			,T.ConciliacaoTipoId			
		);


	--Select SCOPE_IDENTITY() As ContratoTransportadorRegraId
End



