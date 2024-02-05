/******** REFERENCES ******************/
-- Stored Procedure: GetPedidoEntrega
/*************************************/
If(TYPE_ID('Fretter.Tp_CdEntregaList') Is Null)
CREATE TYPE [Fretter].[Tp_CdEntregaList] AS TABLE(
	[Entrega] [varchar](20)
)
