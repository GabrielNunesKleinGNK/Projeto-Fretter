/****** Object:  UserDefinedTableType [Fretter].[Tp_PedidoPendenteBSeller]    Script Date: 30/03/2022 12:09:33 ******/
If(TYPE_ID('[Fretter].[Tp_PedidoPendenteBSeller]') Is Null)
CREATE TYPE [Fretter].[Tp_PedidoPendenteBSeller] AS TABLE(
	[ContratoExternoBSeller] [varchar](70) NULL,
	[DtEmissaoBSeller] [varchar](70) NULL,
	[TranspNomeBSeller] [varchar](70) NULL,
	[NotaBSeller] [varchar](70) NULL,
	[SerieBSeller] [varchar](70) NULL,
	[IdTransportadoraBSeller] [varchar](70) NULL,
	[UltPontoBSeller] [varchar](70) NULL,
	[FilialBSeller] [varchar](70) NULL,
	[DataPrometidaBSeller] [varchar](70) NULL,
	[DataETRBSeller] [varchar](70) NULL,
	[StatusBSeller] [varchar](100) NULL,
	[NomePontoBSeller] [varchar](70) NULL,
	[EntregaBSeller] [varchar](70) NULL,
	[IdContratoBSeller] [smallint] NOT NULL,
	[DataAjustadaBSeller] [varchar](70) NULL,
	[DtUltPontoBSeller] [varchar](70) NULL,
	[EntregaId] [int] NULL,
	[TransportadorId] [int] NULL,
	[EmpresaId] [int] NULL,
	[StatusTratado] [varchar](200) NULL,
	[NotaFiscal] [varchar](100) NULL,
	[NotaFiscalDRS] [varchar](100) NULL,
	[CnpjCanal] [varchar](100) NULL,
	[CnpjCanalDRS] [varchar](100) NULL,
	[Danfe] [varchar](70) NULL,
	[DanfeDRS] [varchar](70) NULL,
	[DescricaoMicroServico] [varchar](100) NULL,
	[DataProcessamento] [smalldatetime] NOT NULL,
	[Sro] [varchar](70) NULL,
	[StatusOcorrenciaFusion] [varchar](200) NULL,
	[DataOcorrenciaFusion] [datetime] NULL
)
GO


