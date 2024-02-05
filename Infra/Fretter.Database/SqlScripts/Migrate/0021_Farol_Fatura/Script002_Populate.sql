
If Exists(Select Top 1 1 From Fretter.FaturaStatus Where Descricao = 'Pendente' And Icon Is Null)
Begin
	Update Fretter.FaturaStatus
	Set Icon = 'fas fa-clock', IconColor = '#eb9a2a'
	Where Descricao = 'Pendente'
End
Go

If Exists(Select Top 1 1 From Fretter.FaturaStatus Where Descricao = 'Em Processamento' And Icon Is Null)
Begin
	Update Fretter.FaturaStatus
	Set Icon = 'fas fa-cog fa-spin', IconColor = '#9803fc'
	Where Descricao = 'Em Processamento'
End
Go

If Exists(Select Top 1 1 From Fretter.FaturaStatus Where Descricao = 'Em Aberto' And Icon Is Null)
Begin
	Update Fretter.FaturaStatus
	Set Icon = 'fas fa-folder-open', IconColor = '#408c01'
	Where Descricao = 'Em Aberto'
End
Go

If Exists(Select Top 1 1 From Fretter.FaturaStatus Where Descricao = 'Liquidado' And Icon Is Null)
Begin
	Update Fretter.FaturaStatus
	Set Icon = 'fa fa-check-circle', IconColor = '#0380fc'
	Where Descricao = 'Liquidado'
End
Go

If Exists(Select Top 1 1 From Fretter.FaturaStatus Where Descricao = 'Cancelado' And Icon Is Null)
Begin
	Update Fretter.FaturaStatus
	Set Icon = 'fas fa-ban', IconColor = '#f74848'
	Where Descricao = 'Cancelado'
End
Go