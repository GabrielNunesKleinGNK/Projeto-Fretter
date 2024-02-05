If COL_LENGTH('Fretter.ImportacaoCte', 'VersaoAplicacao') IS NULL
	Alter Table Fretter.ImportacaoCte Add VersaoAplicacao Varchar(32)
Go
If COL_LENGTH('Fretter.ImportacaoCte', 'ChaveCte') IS NULL
	Alter Table Fretter.ImportacaoCte Add ChaveCte Varchar(128)
Go
If COL_LENGTH('Fretter.ImportacaoCte', 'DigestValue') IS NULL
	Alter Table Fretter.ImportacaoCte Add DigestValue Varchar(256)
Go
If COL_LENGTH('Fretter.ImportacaoCte', 'DataAutorizacao') IS NULL
	Alter Table Fretter.ImportacaoCte Add DataAutorizacao Datetime
Go
If COL_LENGTH('Fretter.ImportacaoCte', 'StatusAutorizacao') IS NULL
	Alter Table Fretter.ImportacaoCte Add StatusAutorizacao Varchar(32)
Go
If COL_LENGTH('Fretter.ImportacaoCte', 'ProtocoloAutorizacao') IS NULL
	Alter Table Fretter.ImportacaoCte Add ProtocoloAutorizacao Varchar(128)
Go
If COL_LENGTH('Fretter.ImportacaoCte', 'MotivoAutorizacao') IS NULL
	Alter Table Fretter.ImportacaoCte Add MotivoAutorizacao Varchar(256)
Go