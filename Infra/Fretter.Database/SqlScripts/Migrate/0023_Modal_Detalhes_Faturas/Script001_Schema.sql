If Col_length('Fretter.FaturaItem', 'FaturaId') IS NULL
Begin
	Alter Table Fretter.FaturaItem
	Add FaturaId Int Constraint FK_Fretter_FaturaItem_Fatura References Fretter.Fatura(FaturaId)
End