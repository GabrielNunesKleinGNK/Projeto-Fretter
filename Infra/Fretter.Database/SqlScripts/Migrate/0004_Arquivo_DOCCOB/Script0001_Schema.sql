
If Object_Id('Fretter.ArquivoCobranca') Is NULL
Begin
	Create Table Fretter.ArquivoCobranca
	(
		 ArquivoCobrancaId			Int Identity Constraint PK_ArquivoCobranca Primary Key
		,FaturaId					Int References Fretter.Fatura(FaturaId)
		,IdentificacaoRemetente		Varchar(35)
		,IdentificacaoDestinatario	Varchar(35)
		,Data						DateTime
		,QtdTotal					Int
		,QtdItens					Int
		,ValorTotal					Decimal(10,2)
		,ArquivoUrl					Varchar(512)
		,DataCadastro				DateTime
		,UsuarioCadastro			Int
		,DataAlteracao				DateTime
		,UsuarioAlteracao			Int
		,Ativo						Bit
	)
End
Go


If Object_Id('Fretter.ArquivoCobrancaDocumento') Is NULL
Begin
	Create Table Fretter.ArquivoCobrancaDocumento
	(
		 ArquivoCobrancaDocumentoId		Int Identity Constraint PK_ArquivoCobrancaDocumento Primary Key
		,ArquivoCobrancaId				Int References Fretter.ArquivoCobranca(ArquivoCobrancaId)
		,FilialEmissora					Varchar(10)
		,Tipo							Int
		,Serie							Varchar(3)
		,Numero							Varchar(10)
		,DataEmissao					Date
		,DataVencimento					Date
		,ValorTotal						Decimal(15,2)
		,TipoCobranca					Varchar(3)
		,CFOP							Varchar(5)
		,CodigoAcessoNFe				Varchar(9)
		,ChaveAcessoNFe					Varchar(45)
		,ProtocoloNFe					Varchar(15)
		,Ativo							Bit
	)
End
Go

If Object_Id('Fretter.ArquivoCobrancaDocumentoItem') Is NULL
Begin
	Create Table Fretter.ArquivoCobrancaDocumentoItem
	(
		 ArquivoCobrancaDocumentoItemId	Int Identity Constraint PK_ArquivoCobrancaDocumentoItem Primary Key
		,ArquivoCobrancaDocumentoId		Int References Fretter.ArquivoCobrancaDocumento(ArquivoCobrancaDocumentoId)
		,Filial							Varchar(10)
		,Serie							Varchar(5)
		,Numero							Varchar(12)
		,ValorFrete						Decimal(15,2)
		,DataEmissao					Date
		,DocumentoRemetente				Varchar(14)
		,DocumentoDestinatario			Varchar(14)
		,DocumentoEmissor				Varchar(14)
		,UfEmbarcadora					Varchar(2)
		,UfDestinataria					Varchar(2)
		,UfEmissora						Varchar(2)
		,CodigoIVA						Varchar(2)
		,Devolucao						Bit
		,Ativo							Bit
	)
End
Go


IF COL_LENGTH('Fretter.ImportacaoCte', 'ChaveComplementar') IS NULL
BEGIN
    ALTER TABLE Fretter.ImportacaoCte  ADD ChaveComplementar Varchar(64)
END
Go