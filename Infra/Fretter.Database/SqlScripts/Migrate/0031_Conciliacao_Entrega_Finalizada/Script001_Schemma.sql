IF COL_LENGTH('Fretter.ContratoTransportador', 'ConciliaEntregaFinalizada') IS NULL 
BEGIN
	ALTER TABLE Fretter.ContratoTransportador
	ADD ConciliaEntregaFinalizada BIT NOT NULL CONSTRAINT DF_ContratoTransportador_ConciliaEntregaFinalizada DEFAULT 0
END

IF COL_LENGTH('Fretter.ContratoTransportador', 'ConciliaEntregaNaoFinalizada') IS NOT NULL
BEGIN
    Declare @Command   Nvarchar(1000)

    SELECT
        Fretter.ContratoTransportador.ContratoTransportadorId,
        Fretter.ContratoTransportador.ConciliaEntregaNaoFinalizada
    INTO 
        BackupTemporarioConciliaEntregaFinalizada
    FROM
        Fretter.ContratoTransportador
    

    SELECT Top 1 @Command = 'ALTER TABLE Fretter.ContratoTransportador Drop CONSTRAINT ' +sa.name + ' ;'
    FROM sys.all_columns				co
    INNER JOIN sys.tables				ta ON co.object_id = ta.object_id
    INNER JOIN sys.schemas				sc ON ta.schema_id = sc.schema_id
    INNER JOIN sys.default_constraints	sa ON co.default_object_id = sa.object_id
    WHERE sc.name = 'Fretter'
        AND ta.name = 'ContratoTransportador'
        AND co.name = 'ConciliaEntregaNaoFinalizada'

    Execute(@Command)	

    ALTER TABLE Fretter.ContratoTransportador
    DROP COLUMN ConciliaEntregaNaoFinalizada
END