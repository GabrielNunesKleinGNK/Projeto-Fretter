Create or Alter Procedure [Fretter].[GetCamposDeParaTelaIntegracoes]
As Begin

	Drop Table If Exists #DeParaTelaDeIntegracoes

	Create Table #DeParaTelaDeIntegracoes(
		Nome Varchar(64),
		Campo Varchar(256)
	)

	Insert Into 
		#DeParaTelaDeIntegracoes
	Values
			('Código', 'Cd_Id')
		,	('Pedido', 'Cd_Pedido')
		--Inserir os campos para parse na integração
		--,	('Nome que aparecerá na tela', 'Campo do banco de dados')

	Select 
		Nome, 
		Campo 
	from #DeParaTelaDeIntegracoes
End