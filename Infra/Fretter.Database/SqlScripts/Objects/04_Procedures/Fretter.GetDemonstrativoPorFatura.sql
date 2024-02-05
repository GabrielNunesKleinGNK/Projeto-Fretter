Create Or Alter Procedure Fretter.GetDemonstrativoPorFatura
(
	@Itens [dbo].[Tp_Int] READONLY     
)
As Begin
Set Nocount On;

		
SELECT
	c.FaturaId,
	e.Cd_NotaFiscal Notafiscal,
	e.Cd_Serie SerieNotaFiscal,
	e.Cd_Pedido Pedido,
	e.Cd_Danfe ChaveNFE,
	'' NomeRegiao,
	e.Cd_Uf UF,
	'' TipoRegiao,
	e.Cd_CepDestino Cep,
	e.Nm_PrazoDias Prazo,
	e.Dt_Importacao DataImportacao,
	e.Ds_Ocorrencia UltimaOcorrencia,
	e.Dt_Ocorrencia DataOcorrencia,
	ed.Vl_Peso PesoBruto,
	ed.Nr_PesoCubico PesoCubico,
	ed.Vl_Declarado ValorDeclarado,
	ed.Vl_FreteCusto FreteCusto,
	ed.Ds_JsonComposicaoValoresCotacao JsonComposicaoValoresCotacao,
	i.ImportacaoCteId,
	i.Numero CTE_Numero,
	i.Serie CTE_Serie,
	iNf.Chave CTE_Chave,
	(select 
		ico.Nome ,
		ico.Valor, 
		(select top 1 ct.Chave from fretter.configuracaoctetipo ct
			where ico.ConfiguracaoCteTipoId = ct.ConfiguracaoCteTipoId) Chave 
		from  Fretter.ImportacaoCteComposicao ico   (Nolock)
		where ico.ImportacaoCteId =i.ImportacaoCteId for json auto) CTE_JsonComposicao,
	(select 
		ica.Tipo Nome,
		ica.Quantidade Valor,
		(select top 1 ct.Chave from fretter.configuracaoctetipo ct
			where ica.ConfiguracaoCteTipoId = ct.ConfiguracaoCteTipoId) Chave 
		from  Fretter.ImportacaoCteCarga ica     (Nolock)
		where ica.ImportacaoCteId =i.ImportacaoCteId for json auto) CTE_JsonCarga
from fretter.Conciliacao		c (Nolock)
left join Tb_Edi_Entrega		e (Nolock)on e.cd_id = c.EntregaId
left join Tb_Edi_EntregaDetalhe ed(Nolock)on e.cd_id = ed.Id_Entrega
left join Fretter.ImportacaoCte i(Nolock)on c.ImportacaoCteId = i.ImportacaoCteId
left join Fretter.ImportacaoCteNotaFiscal iNF(Nolock) on i.ImportacaoCteId =iNF.ImportacaoCteId
where c.FaturaId in (Select Id from @Itens)
and c.EntregaId is not null
and c.ImportacaoCteId is not null

End
