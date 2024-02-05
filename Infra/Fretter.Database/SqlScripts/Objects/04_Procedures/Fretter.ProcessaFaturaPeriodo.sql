Create Or Alter Procedure Fretter.ProcessaFaturaPeriodo
As Begin
Set Nocount On;	
	
If Exists(Select Top 1 1 From Fretter.FaturaCiclo (Nolock) Where Ativo = 1)
Begin
	Drop Table If Exists #PeriodoTemp

	Select 
		FaturaCicloId	= Fc.FaturaCicloId
		,DiaVencimento	= Fc.DiaVencimento
		,DataInicio		= Case	
							When Fc.FaturaCicloTipoId = 3 Then DateAdd(Day,-1,DATEADD(dd,  0, DATEADD(ww, DATEDIFF(ww, 0, DATEADD(dd, -1, CURRENT_TIMESTAMP)) - 2, 0))) --Quinzenal
							When Fc.FaturaCicloTipoId = 2 Then DateAdd(Day,-1,DATEADD(dd,  0, DATEADD(ww, DATEDIFF(ww, 0, DATEADD(dd, -1, CURRENT_TIMESTAMP)) - 1, 0))) --Semanal
						   Else Cast(dateadd(month,-1,dateadd(day,-1*datepart(Day,Getdate())+1,Getdate())) As Date) End
		,DataFim		= Case	
							When Fc.FaturaCicloTipoId = 3 Then DateAdd(Day,-1,DATEADD(dd,  6, DATEADD(ww, DATEDIFF(ww, 0, DATEADD(dd, -1, CURRENT_TIMESTAMP)) - 1, 0))) --Quinzenal
							When Fc.FaturaCicloTipoId = 2 Then DateAdd(Day,-1,DATEADD(dd,  6, DATEADD(ww, DATEDIFF(ww, 0, DATEADD(dd, -1, CURRENT_TIMESTAMP)) - 1, 0))) --Semanal
						   Else Cast(Dateadd(day,-1*datepart(day,Getdate()),Getdate()) As Date) End
		
		,Vigente		= 1
		,Ativo			= 1
	Into #PeriodoTemp
	From Fretter.FaturaPeriodo	Fp(Nolock) 
	Join Fretter.FaturaCiclo	Fc(Nolock) On Fp.FaturaCicloId = Fc.FaturaCicloId And Fc.Ativo = 1
	Where Fp.Vigente = 1 And DatePart(Day,Getdate()) >= Fc.DiaFechamento

	Delete Pt
	From #PeriodoTemp					Pt
	Left Join Fretter.FaturaPeriodo		Fp(Nolock) On Pt.FaturaCicloId = Fp.FaturaCicloId And Pt.DiaVencimento = Fp.DiaVencimento And Pt.Vigente = Fp.Vigente And Pt.DataInicio = Fp.DataInicio
	Where Fp.FaturaPeriodoId Is Not NULL

	Declare @PeriodoNovo Table (PeriodoId Int,CicloId Int)

	Insert Into Fretter.FaturaPeriodo
	(
		FaturaCicloId
		,DiaVencimento
		,DataInicio
		,DataFim
		,Vigente
		,Ativo		
	)
	OUTPUT inserted.FaturaPeriodoId,inserted.FaturaCicloId 
	INTO @PeriodoNovo(PeriodoId,CicloId)
	Select 
		FaturaCicloId	
		,DiaVencimento	
		,DataInicio																				
		,DataFim																						
		,Vigente		
		,Ativo			
	From #PeriodoTemp	Fp(Nolock) 	

	Update Fp
	Set Vigente = 0
	From Fretter.FaturaPeriodo	Fp
	Where Not Exists(Select Top 1 1 From @PeriodoNovo Pn Where Fp.FaturaPeriodoId = Pn.PeriodoId And Fp.FaturaCicloId = CicloId)		
		And Fp.Vigente = 1
End

End
