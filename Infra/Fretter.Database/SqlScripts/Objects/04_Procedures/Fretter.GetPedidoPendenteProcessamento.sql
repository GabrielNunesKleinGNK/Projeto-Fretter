
/****** Object:  StoredProcedure [Fretter].[GetPedidoPendenteProcessamento]    Script Date: 02/03/2022 14:01:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER  Procedure [Fretter].[GetPedidoPendenteProcessamento]
As
Begin
	SELECT c.EmpresaId,
	c.EstabelecimentoBSellerId,
	c.HoraExecucao,
	c.PeriodoEmDias,
	c.TimeoutRequest
		from Fretter.PedidoPendenteEmpresaConfig c(Nolock)
	Where Ativo=1 and
		c.EmpresaId not in (select p.EmpresaId from Fretter.PedidoPendenteProcessamento p (Nolock)
							where p.EmpresaId = c.EmpresaId and p.DataProcessamento > dateadd(day, datediff(day, 0, getdate()), 0))
End
GO

