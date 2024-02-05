If OBJECT_ID('Fretter.ConciliacaoTipo') IS NULL
		Create Table Fretter.ConciliacaoTipo
		(
			ConciliacaoTipoId			Int Identity(1,1) Constraint PK_Fretter_ConciliacaoTipo_ConciliacaoTipoId Primary Key
			,Nome						Varchar(64)
			,Descricao					Varchar(256)
			,Ativo						Bit Constraint DF_Fretter_ConciliacaoTipo_Ativo Default(1)
		)
Go

IF COL_LENGTH('Fretter.ContratoTransportadorRegra', 'ImportacaoArquivoTipoId') IS NOT NULL
BEGIN
    Declare @Command   Nvarchar(1000)   
    

    SELECT Top 1 @Command = 'ALTER TABLE Fretter.ContratoTransportadorRegra Drop CONSTRAINT ' + object_name(sp.constid) + ' ;'	
	FROM sys.all_columns				co
    INNER JOIN sys.tables				ta ON co.object_id = ta.object_id
    INNER JOIN sys.schemas				sc ON ta.schema_id = sc.schema_id    
	INNER JOIN sys.sysconstraints		sp ON co.object_id = sp.id And co.column_id = sp.colid
    WHERE sc.name = 'Fretter'
        AND ta.name = 'ContratoTransportadorRegra'
        AND co.name = 'ImportacaoArquivoTipoId'
	
    Execute(@Command)	

    ALTER TABLE Fretter.ContratoTransportadorRegra
    DROP COLUMN ImportacaoArquivoTipoId
END

IF COL_LENGTH('Fretter.ContratoTransportadorRegra', 'ImportacaoArquivoTipoItemId') IS NOT NULL
BEGIN
    Declare @Command1   Nvarchar(1000)       

    SELECT Top 1 @Command1 = 'ALTER TABLE Fretter.ContratoTransportadorRegra Drop CONSTRAINT ' + object_name(sp.constid) + ' ;'	
	FROM sys.all_columns				co
    INNER JOIN sys.tables				ta ON co.object_id = ta.object_id
    INNER JOIN sys.schemas				sc ON ta.schema_id = sc.schema_id    
	INNER JOIN sys.sysconstraints		sp ON co.object_id = sp.id And co.column_id = sp.colid
    WHERE sc.name = 'Fretter'
        AND ta.name = 'ContratoTransportadorRegra'
        AND co.name = 'ImportacaoArquivoTipoItemId'

    Execute(@Command1)	

    ALTER TABLE Fretter.ContratoTransportadorRegra
    DROP COLUMN ImportacaoArquivoTipoItemId
END
Go
IF COL_LENGTH('Fretter.ContratoTransportadorRegra', 'ConciliacaoTipoId') IS NULL
	Alter Table Fretter.ContratoTransportadorRegra Add ConciliacaoTipoId Int Constraint FK_Fretter_ContratoTransportadorRegra_ConciliacaoTipoId References Fretter.ConciliacaoTipo(ConciliacaoTipoId)
Go
IF COL_LENGTH('Fretter.Conciliacao', 'ConciliacaoTipoId') IS NULL
	Alter Table Fretter.Conciliacao Add ConciliacaoTipoId Int Constraint FK_Fretter_Conciliacao_ConciliacaoTipoId References Fretter.ConciliacaoTipo(ConciliacaoTipoId)