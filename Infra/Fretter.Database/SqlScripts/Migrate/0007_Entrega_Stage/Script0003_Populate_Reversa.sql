If Not Exists(Select Top 1 1 From Tb_Edi_EntregaTransporteServico)
Begin
	Insert Into Tb_Edi_EntregaTransporteServico
	(
		Id_Empresa					
		,Id_Transportador			
		,Ds_Nome					
		,Ds_Usuario					
		,Ds_Senha					
		,Cd_CodigoContrato			
		,Cd_CodigoIntegracao		
		,Cd_CodigoCartao			
		,Ds_URLBase					
		,Flg_Ativo					
	)
	Select	
		Id_Empresa					= (Select Top 1 Cd_Id From Tb_Adm_Empresa (Nolock) Where Ds_NomeFantasia = 'CARREFOUR' Order By 1 Desc )
		,Id_Transportador			= 3
		,Ds_Nome					= 'Contrato CORREIOS'		
		,Ds_Usuario					= 'empresacws' --IdCorreios
		,Ds_Senha					= '123456'
		,Cd_CodigoContrato			= '9992157880' -- Contrato
		,Cd_CodigoIntegracao		= '17000190'   -- Codigo Administrativo
		,Cd_CodigoCartao			= '0067599079'	
		,Ds_URLBase					= 'https://apphom.correios.com.br/'
		,Flg_Ativo					= 1
End
Go
If Not Exists(Select Top 1 1 From Tb_Edi_EntregaTransporteTipo)
Begin
	Insert Into Tb_Edi_EntregaTransporteTipo
	(
		Id_EntregaTransporteServico	
		,Ds_Nome						
		,Ds_URL							
		,Ds_Layout						
		,Ds_ApiKey						
		,Ds_Usuario						
		,Ds_Senha						
		,Nr_DiasValidadeMinimo			
		,Nr_DiasValidadeMaximo			
		,Cd_Alias						
		,Cd_CodigoIntegracao			
		,Flg_Producao					
		,Flg_Ativo						
	)
	Select 
		Id_EntregaTransporteServico		= 1
		,Ds_Nome						= 'Auorizacao de Postagem'	
		,Ds_URL							= NULL	
		,Ds_Layout						= NULL
		,Ds_ApiKey						= NULL
		,Ds_Usuario						= NULL
		,Ds_Senha						= NULL
		,Nr_DiasValidadeMinimo			= 5
		,Nr_DiasValidadeMaximo			= 90
		,Cd_Alias						= 'A'
		,Cd_CodigoIntegracao			= 'A'
		,Flg_Producao					= 1
		,Flg_Ativo						= 1
	Union All
	Select 
		Id_EntregaTransporteServico		= 1
		,Ds_Nome						= 'Coleta domiciliar'	
		,Ds_URL							= NULL	
		,Ds_Layout						= NULL
		,Ds_ApiKey						= NULL
		,Ds_Usuario						= NULL
		,Ds_Senha						= NULL
		,Nr_DiasValidadeMinimo			= 5
		,Nr_DiasValidadeMaximo			= 90
		,Cd_Alias						= 'CA'
		,Cd_CodigoIntegracao			= 'CA'
		,Flg_Producao					= 1
		,Flg_Ativo						= 1
End
Go
If Not Exists(Select Top 1 1 From dbo.Tb_Edi_EntregaDevolucaoStatus)
Begin
	SET IDENTITY_INSERT dbo.Tb_Edi_EntregaDevolucaoStatus ON
	Insert Into dbo.Tb_Edi_EntregaDevolucaoStatus -- Aberta | Autorizada | Agendada | Em Transito | Finalizada //Status dos Transportador
	(
		Cd_Id
		,Id_EntregaTransporteTipo		
		,Cd_CodigoIntegracao			
		,Cd_Alias						
		,Ds_Nome						
		,Flg_Ativo						
	)
	Select 
		Cd_Id = 1
		,Id_EntregaTransporteTipo		= 1
		,Cd_CodigoIntegracao			= 0
		,Cd_Alias						= 'ABE'	
		,Ds_Nome						= 'Em Aberto'
		,Flg_Ativo						= 1
	Union All
	Select 
		Cd_Id = 2
		,Id_EntregaTransporteTipo		= 1
		,Cd_CodigoIntegracao			= 0
		,Cd_Alias						= 'AGP'	
		,Ds_Nome						= 'Ag. Processamento'
		,Flg_Ativo						= 1
	Union All
	Select 
		Cd_Id = 3
		,Id_EntregaTransporteTipo		= 1
		,Cd_CodigoIntegracao			= 0
		,Cd_Alias						= 'AUT'	
		,Ds_Nome						= 'Autorizado'
		,Flg_Ativo						= 1
	Union All
	Select 
		Cd_Id = 4
		,Id_EntregaTransporteTipo		= 1
		,Cd_CodigoIntegracao			= 0
		,Cd_Alias						= 'NAU'	
		,Ds_Nome						= 'Não Autorizado'
		,Flg_Ativo						= 1
	Union All
	Select 
		Cd_Id = 5
		,Id_EntregaTransporteTipo		= 1
		,Cd_CodigoIntegracao			= 0
		,Cd_Alias						= 'REJ'	
		,Ds_Nome						= 'Rejeitado'
		,Flg_Ativo						= 1
	Union All
	Select 
		Cd_Id = 6
		,Id_EntregaTransporteTipo		= 1
		,Cd_CodigoIntegracao			= 0
		,Cd_Alias						= 'AGC'	
		,Ds_Nome						= 'Ag. Cancelamento'
		,Flg_Ativo						= 1
	Union All
	Select 
		Cd_Id = 7
		,Id_EntregaTransporteTipo		= 1
		,Cd_CodigoIntegracao			= 0
		,Cd_Alias						= 'CAN'	
		,Ds_Nome						= 'Cancelado'
		,Flg_Ativo						= 1
	Union All
	Select 
		Cd_Id = 8
		,Id_EntregaTransporteTipo		= 1
		,Cd_CodigoIntegracao			= 0
		,Cd_Alias						= 'ERR'	
		,Ds_Nome						= 'Erro'
		,Flg_Ativo						= 1
	SET IDENTITY_INSERT dbo.Tb_Edi_EntregaDevolucaoStatus OFF
End
Go
If Not Exists(Select Top 1 1 From dbo.Tb_Edi_EntregaDevolucaoAcao)
Begin
	SET IDENTITY_INSERT dbo.Tb_Edi_EntregaDevolucaoAcao ON
	Insert Into dbo.Tb_Edi_EntregaDevolucaoAcao -- Aprovar -- Rejeitar --Cancelar
	(
		Cd_Id
		,Ds_Nome						
		,Flg_Ativo					
	)
	Select	
		Cd_Id						= 1
		,Ds_Nome					= 'Aprovar'				
		,Flg_Ativo					= 1
	Union All
	Select	
		Cd_Id						= 2
		,Ds_Nome					= 'Reprovar'				
		,Flg_Ativo					= 1
	Union All
	Select	
		Cd_Id						= 3
		,Ds_Nome					= 'Cancelar'				
		,Flg_Ativo					= 1
	SET IDENTITY_INSERT dbo.Tb_Edi_EntregaDevolucaoAcao OFF
End
Go
If Not Exists(Select Top 1 1 From dbo.Tb_Edi_EntregaDevolucaoStatusAcao)
Begin
	Insert Into dbo.Tb_Edi_EntregaDevolucaoStatusAcao -- Aprovar -- Rejeitar --Cancelar
	(
		Id_EntregaTransporteTipo		
		,Id_EntregaDevolucaoStatus		
		,Id_EntregaDevolucaoAcao		
		,Id_EntregaDevolucaoResultado	
		,Flg_InformaMotivo				
		,Flg_Ativo						
	)
	Select 
		Id_EntregaTransporteTipo		= 1
		,Id_EntregaDevolucaoStatus		= 1
		,Id_EntregaDevolucaoAcao		= 1
		,Id_EntregaDevolucaoResultado	= 2
		,Flg_InformaMotivo				= 0
		,Flg_Ativo						= 1
	Union All
	Select 
		Id_EntregaTransporteTipo		= 1
		,Id_EntregaDevolucaoStatus		= 1
		,Id_EntregaDevolucaoAcao		= 2
		,Id_EntregaDevolucaoResultado	= 5
		,Flg_InformaMotivo				= 1
		,Flg_Ativo						= 1
	Union All
	Select 
		Id_EntregaTransporteTipo		= 1
		,Id_EntregaDevolucaoStatus		= 3
		,Id_EntregaDevolucaoAcao		= 3
		,Id_EntregaDevolucaoResultado	= 6
		,Flg_InformaMotivo				= 1
		,Flg_Ativo						= 1
End
Go
If Not Exists(Select Top 1 1 From dbo.Tb_Edi_EntregaDevolucaoLogTipo)
Begin
	SET IDENTITY_INSERT  dbo.Tb_Edi_EntregaDevolucaoLogTipo ON
	Insert Into dbo.Tb_Edi_EntregaDevolucaoLogTipo
	(
		Cd_Id
		,Ds_Nome
	)
	Values
	(
		1
		,'Solicitacao'
	),
	(
		2
		,'Ocorrencia'
	)
	SET IDENTITY_INSERT  dbo.Tb_Edi_EntregaDevolucaoLogTipo OFF
End