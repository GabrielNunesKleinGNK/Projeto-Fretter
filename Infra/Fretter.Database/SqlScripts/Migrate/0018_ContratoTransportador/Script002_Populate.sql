
If Exists(Select Top 1 1 From Fretter.ContratoTransportador (nolock) Where FaturaAutomatica is null)
Begin
	UPDATE Fretter.ContratoTransportador
	SET FaturaAutomatica = 1
	WHERE FaturaAutomatica is null

END