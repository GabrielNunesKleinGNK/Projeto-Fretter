/****** Object:  StoredProcedure [Fretter].[ProcessarPedidoPendenteBSeller]    Script Date: 02/03/2022 13:45:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE OR ALTER     Procedure [Fretter].[ProcessarPedidoPendenteBSeller]	
(
	@Itens	Fretter.Tp_PedidoPendenteBSeller READONLY
)
AS BEGIN   
SET NOCOUNT ON;                                  
SET DATEFORMAT YMD; 

Insert Into Fretter.PedidoPendenteProcessamento
(EmpresaId,
QuantidadePedidoBSeller,
DataProcessamento
)
select 
	EmpresaId = i.EmpresaId, 
	QuantidadePedidoBSeller =count(i.EntregaBSeller), 
	DataProcessamento = Max(i.DataProcessamento)
from @Itens i
group by EmpresaId


Insert Into Fretter.PedidoPendenteBSellerHistorico
(	
	ContratoExternoBSeller
	,DtEmissaoBSeller
	,TranspNomeBSeller
	,NotaBSeller
	,SerieBSeller
	,IdTransportadoraBSeller
	,UltPontoBSeller
	,FilialBSeller
	,DataPrometidaBSeller
	,DataETRBSeller
	,StatusBSeller
	,NomePontoBSeller
	,EntregaBSeller
	,IdContratoBSeller
	,DataAjustadaBSeller
	,DtUltPontoBSeller
	,EntregaId
	,TransportadorId
	,EmpresaId
	,StatusTratado
	,DescricaoMicroServico
	,NotaFiscal
	,NotaFiscalDRS
	,CnpjCanal
	,CnpjCanalDRS
	,Danfe
	,DanfeDRS
	,Sro
	,StatusOcorrenciaFusion
	,DataOcorrenciaFusion
	,DataProcessamento
	,AcaoProcessamento
)
Select 
	 ContratoExternoBSeller   =p.ContratoExternoBSeller
	,DtEmissaoBSeller         =p.DtEmissaoBSeller
	,TranspNomeBSeller        =p.TranspNomeBSeller
	,NotaBSeller              =p.NotaBSeller
	,SerieBSeller             =p.SerieBSeller
	,IdTransportadoraBSeller  =p.IdTransportadoraBSeller
	,UltPontoBSeller          =p.UltPontoBSeller
	,FilialBSeller            =p.FilialBSeller
	,DataPrometidaBSeller     =p.DataPrometidaBSeller
	,DataETRBSeller           =p.DataETRBSeller
	,StatusBSeller            =p.StatusBSeller
	,NomePontoBSeller         =p.NomePontoBSeller
	,EntregaBSeller           =p.EntregaBSeller
	,IdContratoBSeller        =p.IdContratoBSeller
	,DataAjustadaBSeller      =p.DataAjustadaBSeller
	,DtUltPontoBSeller        =p.DtUltPontoBSeller
	,EntregaId                =p.EntregaId
	,TransportadorId          =p.TransportadorId
	,EmpresaId                =p.EmpresaId
	,StatusTratado             =p.StatusTratado
	,DescricaoMicroServico    =p.DescricaoMicroServico 
	,NotaFiscal               =p.NotaFiscal
	,NotaFiscalDRS            =p.NotaFiscalDRS
	,CnpjCanal                =p.CnpjCanal
	,CnpjCanalDRS             =p.CnpjCanalDRS
	,Danfe                    =p.Danfe
	,DanfeDRS                 =p.DanfeDRS
	,Sro                      =p.Sro
	,StatusOcorrenciaFusion   =p.StatusOcorrenciaFusion
	,DataOcorrenciaFusion     =p.DataOcorrenciaFusion
	,DataProcessamento        =p.DataProcessamento		
	,AcaoProcessamento = 'UPDATE'
From Fretter.PedidoPendenteBSeller p(Nolock)
where p.EmpresaId  in (
	select distinct EmpresaId from @Itens
)


Update Pe
Set 
	ContratoExternoBSeller    =It.ContratoExternoBSeller
	,DtEmissaoBSeller         =It.DtEmissaoBSeller
	,TranspNomeBSeller        =It.TranspNomeBSeller
	,NotaBSeller              =It.NotaBSeller
	,SerieBSeller             =It.SerieBSeller
	,IdTransportadoraBSeller  =It.IdTransportadoraBSeller
	,UltPontoBSeller          =It.UltPontoBSeller
	,FilialBSeller            =It.FilialBSeller
	,DataPrometidaBSeller     =It.DataPrometidaBSeller
	,DataETRBSeller           =It.DataETRBSeller
	,StatusBSeller            =It.StatusBSeller
	,NomePontoBSeller         =It.NomePontoBSeller
	,EntregaBSeller           =It.EntregaBSeller
	,IdContratoBSeller        =It.IdContratoBSeller
	,DataAjustadaBSeller      =It.DataAjustadaBSeller
	,DtUltPontoBSeller        =It.DtUltPontoBSeller
	,EntregaId                =It.EntregaId
	,TransportadorId          =It.TransportadorId
	,EmpresaId                =It.EmpresaId
	,StatusTratado             =It.StatusTratado
	,DescricaoMicroServico    =It.DescricaoMicroServico 
	,NotaFiscal               =It.NotaFiscal
	,NotaFiscalDRS            =It.NotaFiscalDRS
	,CnpjCanal                =It.CnpjCanal
	,CnpjCanalDRS             =It.CnpjCanalDRS
	,Danfe                    =It.Danfe
	,DanfeDRS                 =It.DanfeDRS
	,Sro                      =It.Sro
	,StatusOcorrenciaFusion   =It.StatusOcorrenciaFusion
	,DataOcorrenciaFusion     =It.DataOcorrenciaFusion
	,DataProcessamento        =It.DataProcessamento			
From Fretter.PedidoPendenteBSeller 		Pe(Nolock)
Join @Itens It	On It.EntregaBSeller = Pe.EntregaBSeller


Insert Into Fretter.PedidoPendenteBSeller
(	
	ContratoExternoBSeller
	,DtEmissaoBSeller
	,TranspNomeBSeller
	,NotaBSeller
	,SerieBSeller
	,IdTransportadoraBSeller
	,UltPontoBSeller
	,FilialBSeller
	,DataPrometidaBSeller
	,DataETRBSeller
	,StatusBSeller
	,NomePontoBSeller
	,EntregaBSeller
	,IdContratoBSeller
	,DataAjustadaBSeller
	,DtUltPontoBSeller
	,EntregaId
	,TransportadorId
	,EmpresaId
	,StatusTratado
	,DescricaoMicroServico
	,NotaFiscal
	,NotaFiscalDRS
	,CnpjCanal
	,CnpjCanalDRS
	,Danfe
	,DanfeDRS
	,Sro
	,StatusOcorrenciaFusion
	,DataOcorrenciaFusion
	,DataProcessamento
)
Select 
	ContratoExternoBSeller   =i.ContratoExternoBSeller
	,DtEmissaoBSeller         =i.DtEmissaoBSeller
	,TranspNomeBSeller        =i.TranspNomeBSeller
	,NotaBSeller              =i.NotaBSeller
	,SerieBSeller             =i.SerieBSeller
	,IdTransportadoraBSeller  =i.IdTransportadoraBSeller
	,UltPontoBSeller          =i.UltPontoBSeller
	,FilialBSeller            =i.FilialBSeller
	,DataPrometidaBSeller     =i.DataPrometidaBSeller
	,DataETRBSeller           =i.DataETRBSeller
	,StatusBSeller            =i.StatusBSeller
	,NomePontoBSeller         =i.NomePontoBSeller
	,EntregaBSeller           =i.EntregaBSeller
	,IdContratoBSeller        =i.IdContratoBSeller
	,DataAjustadaBSeller      =i.DataAjustadaBSeller
	,DtUltPontoBSeller        =i.DtUltPontoBSeller
	,EntregaId                =i.EntregaId
	,TransportadorId          =i.TransportadorId
	,EmpresaId                =i.EmpresaId
	,StatusTratado             =i.StatusTratado
	,DescricaoMicroServico    =i.DescricaoMicroServico 
	,NotaFiscal               =i.NotaFiscal
	,NotaFiscalDRS            =i.NotaFiscalDRS
	,CnpjCanal                =i.CnpjCanal
	,CnpjCanalDRS             =i.CnpjCanalDRS
	,Danfe                    =i.Danfe
	,DanfeDRS                 =i.DanfeDRS
	,Sro                      =i.Sro
	,StatusOcorrenciaFusion   =i.StatusOcorrenciaFusion
	,DataOcorrenciaFusion     =i.DataOcorrenciaFusion
	,DataProcessamento        =i.DataProcessamento				
From @Itens i
where i.EntregaBSeller not in(
	select EntregaBSeller from Fretter.PedidoPendenteBSeller(Nolock)
)


delete from Fretter.PedidoPendenteBSeller 
where EntregaBSeller not in (
	select i.EntregaBSeller from @Itens i
) and EmpresaId in(
	select i.EmpresaId from @Itens i where i.EmpresaId is not null
)


End