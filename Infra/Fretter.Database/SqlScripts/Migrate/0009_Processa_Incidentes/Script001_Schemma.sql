
If Not Exists
(
    Select Top 1 1 From Sys.indexes I
    Inner Join Sys.Tables T On T.Object_id = I.object_id
    where I.Name = 'IDX_CDPedido' and T.name = 'Tb_Edi_Entrega'
)
Begin
	CREATE NONCLUSTERED INDEX [IDX_CDPedido] ON [dbo].[Tb_Edi_Entrega]
	(
		[Cd_Pedido] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
End
