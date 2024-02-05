If Not Exists(Select Top 1 1 From Fretter.ConciliacaoTipo (nolock))
Begin
	SET Identity_Insert Fretter.ConciliacaoTipo ON
	Insert Into Fretter.ConciliacaoTipo
	(
		ConciliacaoTipoId		
		,Nome					
		,Descricao				
		,Ativo					
	)
	Values
	(
		1
		,'ENTREGA'
		,'ENTREGA'
		,1
	),
	(
		2
		,'DEVOLUCAO'
		,'DEVOLUCAO'
		,1
	),
	(
		3
		,'REENTREGA'
		,'REENTREGA'
		,1
	)
	SET Identity_Insert Fretter.ConciliacaoTipo OFF
End
Go

If COL_LENGTH('Fretter.ImportacaoArquivoTipoItem', 'Descricao') IS Not NULL
Begin
	Alter Table Fretter.ImportacaoArquivoTipoItem Add ConciliacaoTipoId Int Constraint FK_Fretter_ImportacaoArquivoTipoItem_ConciliacaoTipoId References Fretter.ConciliacaoTipo(ConciliacaoTipoId)
End
Go
If COL_LENGTH('Fretter.ImportacaoArquivoTipoItem', 'ConciliacaoTipoId') IS NULL
Begin
	Update Fretter.ImportacaoArquivoTipoItem
	Set ConciliacaoTipoId = Case 
								When Descricao = 'ENTREGA' Then 1 
								When Descricao = 'DEVOLUCAO' Then 2 
								When Descricao = 'REENTREGA' Then 3  Else ConciliacaoTipoId End
	Where ConciliacaoTipoId Is NULL

	Alter Table Fretter.ImportacaoArquivoTipoItem Drop Column Descricao
End
Go
If COL_LENGTH('Fretter.ControleProcessoOcorrencia', 'CteIdInicial') IS Not NULL
Begin
	EXEC sp_RENAME 'Fretter.ControleProcessoOcorrencia.CteIdInicial' , 'IdInicial', 'COLUMN'
End
Go
If COL_LENGTH('Fretter.ControleProcessoOcorrencia', 'CteIdFinal') IS Not NULL
Begin
	EXEC sp_RENAME 'Fretter.ControleProcessoOcorrencia.CteIdFinal' , 'IdFinal', 'COLUMN'
End
Go
If COL_LENGTH('Fretter.ControleProcessoOcorrencia', 'DtInclusao') IS NULL
Begin
	Alter table Fretter.ControleProcessoOcorrencia Add DtInclusao Datetime Default(Getdate())
End
Go
IF EXISTS(Select 1 From sys.procedures sp
			Join sys.schemas ss on sp.schema_id = ss.schema_id
			Where ss.name = 'Fretter' and sp.name = 'ProcessaOcorrencia')
Begin
    DROP PROCEDURE Fretter.ProcessaOcorrencia
End