If Not Exists(Select Top 1 1 From Fretter.ConciliacaoStatus (Nolock) Where ConciliacaoStatusId = 4 And Ativo = 1)
Begin
	Set Identity_Insert Fretter.ConciliacaoStatus ON
	Insert Into Fretter.ConciliacaoStatus 
	(
		ConciliacaoStatusId
		,Nome
		,Ativo
		,UsuarioCadastro
		,DataCadastro
	)
	Select
		ConciliacaoStatusId		= 4
		,Nome					= 'Pendente Recotação'
		,Ativo					= 1
		,UsuarioCadastro		= 1
		,DataCadastro			= Getdate()
	Set Identity_Insert Fretter.ConciliacaoStatus OFF
End