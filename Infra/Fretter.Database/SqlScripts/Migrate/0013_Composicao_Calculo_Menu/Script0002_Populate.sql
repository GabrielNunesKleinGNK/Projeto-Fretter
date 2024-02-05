If((Select count(1) From fretter.ConfiguracaoCteTipo) = 3)
Begin
	Insert Into fretter.ConfiguracaoCteTipo Values
	('Gris', 1, 'Gris'), 
	('Advalorem', 1, 'Advalorem'),
	('Pedagio', 1, 'Pedagio'),
	('Fretepeso', 1, 'Fretepeso'),
	('TaxaTRT', 1, 'TaxaTRT'),
	('TaxaTDE', 1, 'TaxaTDE'),
	('TaxaTDA', 1, 'TaxaTDA'),
	('TaxaCtE', 1, 'TaxaCtE'),
	('TaxaRisco', 1, 'TaxaRisco'),
	('Suframa', 1, 'Suframa'),
	('PesoConsiderado', 1, 'PesoConsiderado');
End

IF EXISTS( SELECT TOP 1 1 From fretter.ConfiguracaoCteTipo Where ConfiguracaoCteTipoId = 3 And Descricao = 'Outros')
Begin
	Update fretter.ConfiguracaoCteTipo 
	Set Descricao = 'Icms', Chave = 'Icms'
	Where ConfiguracaoCteTipoId = 3
End