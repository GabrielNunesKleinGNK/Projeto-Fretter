Create Or Alter Procedure Fretter.SetContratoTransportadorArquivoTipo
(
	@ContratoTransportadorArquivoTipoId			Int = 0
	,@EmpresaId									Int = 0
	,@TransportadorId							Int = 0
	,@ImportacaoArquivoTipoItemId				Int = 0
	,@Alias										varchar(64) = null
	,@UsuarioCadastro							int = 0
	,@UsuarioAlteracao							Int = 0
	,@Ativo										bit = 1
)
As Begin
	Set Nocount On;

	;WITH A AS
	(
		SELECT 
			*
		From Fretter.ContratoTransportadorArquivoTipo		Cr(Nolock)
		WHERE cr.TransportadorId = @TransportadorId 
			  And EmpresaId = @EmpresaId 
			  And cr.ImportacaoArquivoTipoItemId = @ImportacaoArquivoTipoItemId  
			  And cr.Alias = @Alias
			  Or cr.ContratoTransportadorArquivoTipoId = @ContratoTransportadorArquivoTipoId
	)

	MERGE A AS E  
    USING
	(
		Select 
			ContratoTransportadorArquivoTipoId  = @ContratoTransportadorArquivoTipoId,	
			EmpresaId							= @EmpresaId,							
			TransportadorId						= @TransportadorId,
			ImportacaoArquivoTipoItemId			= @ImportacaoArquivoTipoItemId,	
			Alias								= @Alias,			
			UsuarioCadastro						= @UsuarioCadastro,	
			UsuarioAlteracao					= @UsuarioAlteracao,		
			Ativo								= @Ativo			
	) AS T On 
	(	
				E.TransportadorId = @TransportadorId 
			  And E.EmpresaId = @EmpresaId  
			  And E.ImportacaoArquivoTipoItemId = @ImportacaoArquivoTipoItemId
			  And E.Alias = @Alias
			  Or E.ContratoTransportadorArquivoTipoId = @ContratoTransportadorArquivoTipoId
	)
	WHEN MATCHED THEN    
       Update Set 
			 E.ImportacaoArquivoTipoItemId		= @ImportacaoArquivoTipoItemId	
	  		,E.DataAlteracao					= GETDATE()
			,E.UsuarioAlteracao					= @UsuarioAlteracao
			,E.Alias							= @Alias
			,E.Ativo							= @Ativo			
    WHEN NOT MATCHED THEN   
		Insert
		(
			EmpresaId, 
			TransportadorId, 
			ImportacaoArquivoTipoItemId, 
			Alias, 
			DataCadastro,
			UsuarioCadastro,	
			Ativo	
		)
		Values
		(
			 T.EmpresaId
			,T.TransportadorId
			,T.ImportacaoArquivoTipoItemId
			,T.Alias
			,GETDATE()
			,T.UsuarioCadastro
			,1
		);


	--Select SCOPE_IDENTITY() As ContratoTransportadorRegraId
End



