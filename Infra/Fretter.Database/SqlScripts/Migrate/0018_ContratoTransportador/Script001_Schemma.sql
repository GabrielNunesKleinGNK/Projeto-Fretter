IF COL_LENGTH('Fretter.ContratoTransportador', 'FaturaAutomatica') IS NULL
BEGIN
	Alter Table Fretter.ContratoTransportador Add FaturaAutomatica bit Default(1)
END
Go