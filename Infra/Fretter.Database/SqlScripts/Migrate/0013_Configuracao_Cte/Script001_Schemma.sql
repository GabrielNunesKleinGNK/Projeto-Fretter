

If((Select Top 1 CHARACTER_MAXIMUM_LENGTH 
	From information_schema.columns  
	Where	TABLE_NAME = 'ConfiguracaoCteTransportador' 
		And TABLE_SCHEMA = 'Fretter'
		And COLUMN_NAME = 'Alias') != 500)
Begin
	ALTER TABLE Fretter.ConfiguracaoCteTransportador ALTER COLUMN Alias VARCHAR (500) NOT NULL;
End