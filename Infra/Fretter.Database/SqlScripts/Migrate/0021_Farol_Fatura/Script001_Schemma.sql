IF COL_LENGTH('Fretter.FaturaStatus', 'Icon') IS NULL
BEGIN
	Alter Table Fretter.FaturaStatus Add Icon Varchar(64)
END
Go

IF COL_LENGTH('Fretter.FaturaStatus', 'IconColor') IS NULL
BEGIN
	Alter Table Fretter.FaturaStatus Add IconColor Varchar(16)
END
Go