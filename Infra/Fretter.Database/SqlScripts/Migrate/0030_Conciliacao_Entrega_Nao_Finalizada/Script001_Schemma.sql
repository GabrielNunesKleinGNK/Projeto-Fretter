IF COL_LENGTH('Fretter.ContratoTransportador', 'ConciliaEntregaNaoFinalizada') IS NULL
BEGIN
	ALTER TABLE Fretter.ContratoTransportador
	ADD ConciliaEntregaNaoFinalizada BIT NOT NULL DEFAULT 0
End
GO