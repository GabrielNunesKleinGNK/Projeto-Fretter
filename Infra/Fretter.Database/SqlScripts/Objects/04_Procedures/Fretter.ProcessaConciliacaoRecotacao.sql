Create Or Alter Procedure Fretter.ProcessaConciliacaoRecotacao
(
	@Itens Fretter.Tp_ConciliacaoRecotacaoRetorno ReadOnly
)
As Begin
Set Nocount On;

	Update Co
	Set ValorCustoFrete				= Case When It.Sucesso = 1 Then It.ValorCustoFrete Else Co.ValorCustoFrete End
		,ValorCustoAdicional		= Case When It.Sucesso = 1 Then It.ValorCustoAdicional Else Co.ValorCustoAdicional End
		,JsonValoresRecotacao		= Case When It.Sucesso = 1 Then It.JsonValoresRecotacao Else Co.JsonValoresRecotacao End
		,DataAlteracao				= GETDATE()
		,ConciliacaoStatusId		= Case										
											When Ct.PermiteTolerancia = 1 And Ct.ToleranciaTipoId = 2 And (Case When It.Sucesso = 1 Then It.ValorCustoFrete Else Co.ValorCustoFrete End) Between (Co.ValorCustoReal - Ct.ToleranciaInferior) And (Co.ValorCustoReal + Ct.ToleranciaSuperior) Then 2 
											When Ct.PermiteTolerancia = 1 And Ct.ToleranciaTipoId = 1 And (Case When It.Sucesso = 1 Then It.ValorCustoFrete Else Co.ValorCustoFrete End) Between (Co.ValorCustoReal - (Co.ValorCustoReal * (Co.ValorCustoReal/100))) And (Co.ValorCustoReal + (Co.ValorCustoReal * (Ct.ToleranciaSuperior/100))) Then 2 
											When Co.ValorCustoReal - (Case When It.Sucesso = 1 Then It.ValorCustoFrete Else Co.ValorCustoFrete End) = 0 Then 2
										Else 3 End		
		,PossuiDivergenciaTarifa	= Case When It.ValorCustoFrete - Co.ValorCustoReal <> 0 Then 1 Else 0 End --validar
	From Fretter.Conciliacao				Co(Nolock)
	Join @Itens								It		   On Co.ConciliacaoId = It.ConciliacaoId
	Join dbo.Tb_Edi_Entrega					Et(Nolock) On Co.EntregaId = Et.Cd_Id
	Join Fretter.ContratoTransportador		Ct(Nolock) On Co.TransportadorCnpjId = Ct.TransportadorCnpjId And Co.EmpresaId = Ct.EmpresaId And Co.DataEmissao Between Ct.VigenciaInicial And Ct.VigenciaFinal
	Where (Ct.MicroServicoId = Et.Id_MicroServico Or Ct.MicroServicoId Is NULL)
		And Ct.Ativo = 1

	Update Re
	Set Protocolo				= It.Protocolo
		,Processado				= 1
		,DataProcessamento		= GETDATE()		
		,JsonRetornoRecotacao	= It.JsonRetornoRecotacao
		,Sucesso				= It.Sucesso
		,TabelaId               = It.TabelaId
	From Fretter.ConciliacaoRecotacao	Re
	Join @Itens							It ON Re.ConciliacaoRecotacaoId = It.ConciliacaoRecotacaoId

	If Exists(Select Top 1 1 From Fretter.Conciliacao Co(Nolock) Join @Itens It On Co.ConciliacaoId = It.ConciliacaoId Where Co.ConciliacaoStatusId = 4)
	Begin
		Update Co
		Set ValorCustoFrete				= Case When It.Sucesso = 1 Then It.ValorCustoFrete Else Co.ValorCustoFrete End
			,ValorCustoAdicional		= Case When It.Sucesso = 1 Then It.ValorCustoAdicional Else Co.ValorCustoAdicional End
			,JsonValoresRecotacao		= Case When It.Sucesso = 1 Then It.JsonValoresRecotacao Else Co.JsonValoresRecotacao End
			,DataAlteracao				= GETDATE()
			,ConciliacaoStatusId		= Case When Co.ValorCustoReal - (Case When It.Sucesso = 1 Then It.ValorCustoFrete Else Co.ValorCustoFrete End) = 0 Then 2 Else 3 End		
			,PossuiDivergenciaTarifa	= Case When It.ValorCustoFrete - Co.ValorCustoReal <> 0 Then 1 Else 0 End --validar
		From Fretter.Conciliacao				Co(Nolock)
		Join @Itens								It		   On Co.ConciliacaoId = It.ConciliacaoId
		Join dbo.Tb_Edi_Entrega					Et(Nolock) On Co.EntregaId = Et.Cd_Id	
		Where Co.ConciliacaoStatusId	= 4
	End

End