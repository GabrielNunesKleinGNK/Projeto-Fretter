If Object_Id('Fretter.ContratoTransportadorRegra') Is NULL
Begin
	Create Table Fretter.ContratoTransportadorRegra
	(
		ContratoTransportadorRegraId		Int Identity(1,1) Constraint PK_Fretter_ContratoTransportadorRegra_Id  Primary Key 
		,TransportadorId					Int Not NULL Constraint FK_Fretter_ContratoTransportadorRegra_Transportador References dbo.Tb_Adm_Transportador(Cd_Id)
		,OcorrenciaEmpresaItemId			Int Not NULL Constraint FK_Fretter_ContratoTransportadorRegra_OcorrenciaEmpresaItem References dbo.Tb_Edi_OcorrenciaEmpresaItem(Cd_Id)
		,EmpresaTransportadorConfigId		Int NULL Constraint FK_Fretter_ContratoTransportadorRegra_EmpresaTransportadorConfig References dbo.Tb_Adm_EmpresaTransportadorConfig(Cd_Id)
		,ContratoTransportadorRegraTipoId	Tinyint  
		,Acrescimo							Bit Constraint DF_Fretter_ContratoTransportadorRegra_Acrescimo Default(1)
		,Valor								Decimal(10,4) 
		,DataCadastro						Datetime Constraint DF_Fretter_ContratoTransportadorRegra_DataCadatro Default(Getdate())
		,UsuarioCadastro					Int 
		,DataAlteracao						Datetime 
		,UsuarioAlteracao					Int
		,Ativo								Bit Constraint DF_Fretter_ContratoTransportadorRegra_Ativo Default(1)
	)
End
