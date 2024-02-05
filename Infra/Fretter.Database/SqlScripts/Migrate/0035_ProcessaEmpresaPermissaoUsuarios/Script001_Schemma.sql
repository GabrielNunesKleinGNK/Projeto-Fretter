If Object_Id('Fretter.ProcessaEmpresaPermissaoUsuarios') Is NULL
Begin
	Create Table Fretter.ProcessaEmpresaPermissaoUsuarios
	(
	UserId	Int,
	Email	Varchar(200)
	)
End
Go