/****** Object:  StoredProcedure [Fretter].[GetPedidoPendenteStatus]    Script Date: 02/03/2022 14:00:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER  Procedure [Fretter].[GetPedidoPendenteStatus]
(
	 @PedidoPendenteIntegracaoId int 
)
As
Begin
	SELECT p.Status,p.StatusFusion  
	FROM [Fretter].[PedidoPendenteStatus] p(Nolock)
	where PedidoPendenteIntegracaoId = @PedidoPendenteIntegracaoId and Ativo=1
End
GO


