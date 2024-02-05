IF NOT EXISTS(SELECT TOP 1 1 FROM Fretter.ImportacaoArquivoStatus WHERE Nome = 'Não Classificado')
BEGIN
    INSERT INTO Fretter.ImportacaoArquivoStatus
	(
		Nome,
		UsuarioCadastro
	) 
	VALUES
	(
		'Não Classificado',
		1
	)
END
GO