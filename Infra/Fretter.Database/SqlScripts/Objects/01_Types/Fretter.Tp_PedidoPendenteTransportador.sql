/******** REFERENCES ******************/
-- Stored Procedure: Pr_ProcessarPedidoPendenteTransportador
/*************************************/
If(TYPE_ID('Fretter.Tp_PedidoPendenteTransportador') Is Null)
CREATE TYPE [Fretter].[Tp_PedidoPendenteTransportador] AS TABLE(
	[EmpresaId] [int] NOT NULL,
	[EntregaId] [int] NOT NULL,
	[TransportadorId] [int] NOT NULL,
	[StatusTransportadora] [varchar](200),
	[NomeTransportadora] [varchar](200) ,
	[DataStatusTransportadora] [smalldatetime] NULL,
	[DataAtualizacaoTransportadora] [smalldatetime] NULL,
	[Sucesso] [bit] NOT NULL,
	[ListaIntegracaoEnviada] [varchar](200),
	[DataProcessamento] [smalldatetime] NULL
)
