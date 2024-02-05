CREATE  OR ALTER   Procedure [Fretter].[ProcessarPedidoPendenteTransportador]	
(
	@Itens	Fretter.Tp_PedidoPendenteTransportador READONLY
)
AS BEGIN   
SET NOCOUNT ON;                                  
SET DATEFORMAT YMD; 

Insert Into Fretter.PedidoPendenteTransportadorHistorico
(	
	EmpresaId,
	EntregaId,
	TransportadorId,
	StatusTransportadora,
	DataStatusTransportadora,
	DataAtualizacaoTransportadora,
	StatusTratado,
	NomeTransportadora,
	Sucesso,
	ListaIntegracaoEnviada,
	DataProcessamento
)
Select 
	EmpresaId =p.EmpresaId,
	EntregaId =p.EntregaId,
	TransportadorId =p.TransportadorId,
	StatusTransportadora =p.StatusTransportadora,
	DataStatusTransportadora = p.DataStatusTransportadora,
	DataAtualizacaoTransportadora = p.DataAtualizacaoTransportadora,
	StatusTratado =p.StatusTratado,
	NomeTransportadora = p.NomeTransportadora,
	Sucesso =p.Sucesso,
	ListaIntegracaoEnviada = p.ListaIntegracaoEnviada,
	DataProcessamento =p.DataProcessamento
From Fretter.PedidoPendenteTransportador p(Nolock)
where p.EntregaId  in (
	select EntregaId from @Itens
)

Update ppt
Set 
	EmpresaId =i.EmpresaId,
	EntregaId =i.EntregaId,
	TransportadorId =i.TransportadorId,
	StatusTransportadora =i.StatusTransportadora,
	DataStatusTransportadora = i.DataStatusTransportadora,
	DataAtualizacaoTransportadora = i.DataAtualizacaoTransportadora,
	StatusTratado =(select top 1 pps.StatusFusion from Fretter.PedidoPendenteStatus pps where  i.StatusTransportadora like ('%'+pps.Status+'%')),
	NomeTransportadora = i.NomeTransportadora,
	Sucesso =i.Sucesso,
	ListaIntegracaoEnviada = i.ListaIntegracaoEnviada,
	DataProcessamento =i.DataProcessamento
From Fretter.PedidoPendenteTransportador 		ppt(Nolock)
Join @Itens i 
	On i.EntregaId = ppt.EntregaId
left join Fretter.PedidoPendenteIntegracao ppi 
	on i.TransportadorId = ppi.TransportadorId

Insert Into Fretter.PedidoPendenteTransportador
(	
	EmpresaId,
	EntregaId,
	TransportadorId,
	StatusTransportadora,
	DataStatusTransportadora,
	DataAtualizacaoTransportadora,
	StatusTratado,
	NomeTransportadora,
	Sucesso,
	ListaIntegracaoEnviada,
	DataProcessamento
)
Select 
	EmpresaId =i.EmpresaId,
	EntregaId =i.EntregaId,
	TransportadorId =i.TransportadorId,
	StatusTransportadora =i.StatusTransportadora,
	DataStatusTransportadora = i.DataStatusTransportadora,
	DataAtualizacaoTransportadora = i.DataAtualizacaoTransportadora,
	StatusTratado =(select top 1 pps.StatusFusion from Fretter.PedidoPendenteStatus pps where  i.StatusTransportadora like ('%'+pps.Status+'%')),
	NomeTransportadora = i.NomeTransportadora,
	Sucesso =i.Sucesso,
	ListaIntegracaoEnviada = i.ListaIntegracaoEnviada,
	DataProcessamento =i.DataProcessamento			
From @Itens i
left join Fretter.PedidoPendenteIntegracao ppi 
	on i.TransportadorId = ppi.TransportadorId

where i.EntregaId not in(
	select EntregaId from Fretter.PedidoPendenteTransportador(Nolock)
)

End
GO


