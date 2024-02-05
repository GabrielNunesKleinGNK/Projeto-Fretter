IF COL_LENGTH('Fretter.ContratoTransportador', 'ConciliaEntregaFinalizada') IS NOT NULL AND
    OBJECT_ID('dbo.BackupTemporarioConciliaEntregaFinalizada') IS NOT NULL
BEGIN
    UPDATE
        Fretter.ContratoTransportador
    SET
        Fretter.ContratoTransportador.ConciliaEntregaFinalizada = 1
    FROM
        Fretter.ContratoTransportador
    INNER JOIN
        BackupTemporarioConciliaEntregaFinalizada ON Fretter.ContratoTransportador.ContratoTransportadorId = BackupTemporarioConciliaEntregaFinalizada.ContratoTransportadorId AND 
                                                        BackupTemporarioConciliaEntregaFinalizada.ConciliaEntregaNaoFinalizada = 0

    UPDATE
        Fretter.ContratoTransportador
    SET
        Fretter.ContratoTransportador.ConciliaEntregaFinalizada = 0
    FROM
        Fretter.ContratoTransportador
    INNER JOIN
        BackupTemporarioConciliaEntregaFinalizada ON Fretter.ContratoTransportador.ContratoTransportadorId = BackupTemporarioConciliaEntregaFinalizada.ContratoTransportadorId AND 
                                                        BackupTemporarioConciliaEntregaFinalizada.ConciliaEntregaNaoFinalizada = 1

	DROP TABLE BackupTemporarioConciliaEntregaFinalizada
END