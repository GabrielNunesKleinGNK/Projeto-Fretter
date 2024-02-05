If Col_length('Fretter.ConciliacaoRecotacao', 'ValorPesoCte') IS NULL
Begin
	Alter Table Fretter.ConciliacaoRecotacao Add ValorPesoCte Decimal(10,4)
End
Go
IF OBJECT_ID('Fretter.DF_Fretter_Fatura_QuantidadeDevolvido', 'D') IS NULL 
Begin
	Update Fretter.Fatura
	Set QuantidadeDevolvidoRemetente = 0
	Where QuantidadeDevolvidoRemetente Is NULL

	Update Fretter.Fatura
	Set QuantidadeEntrega = 0
	Where QuantidadeEntrega Is NULL
	
	Update Fretter.Fatura
	Set QuantidadeSucesso = 0
	Where QuantidadeSucesso Is NULL	

	Alter Table Fretter.Fatura Alter Column QuantidadeDevolvidoRemetente Int Not NULL;
	Alter Table Fretter.Fatura Add Constraint DF_Fretter_Fatura_QuantidadeDevolvido  Default(0) For QuantidadeDevolvidoRemetente;
	Alter Table Fretter.Fatura Alter Column QuantidadeEntrega Int Not NULL;
	Alter Table Fretter.Fatura Add Constraint DF_Fretter_Fatura_QuantidadeEntrega  Default(0) For QuantidadeEntrega;
	Alter Table Fretter.Fatura Alter Column QuantidadeSucesso Int Not NULL;
	Alter Table Fretter.Fatura Add Constraint DF_Fretter_Fatura_QuantidadeSucesso  Default(0) For QuantidadeSucesso;
End