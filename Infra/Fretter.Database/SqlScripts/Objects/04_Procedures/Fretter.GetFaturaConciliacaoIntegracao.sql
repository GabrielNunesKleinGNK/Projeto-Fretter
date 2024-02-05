CREATE OR ALTER PROCEDURE Fretter.GetFaturaConciliacaoIntegracao
(
	@FaturaId INT,
	@EmpresaId INT
)
AS BEGIN
SET NOCOUNT OFF;

Drop Table If Exists #TempConciliacao
Drop Table If Exists #FaturaConciliacaoTmp
Drop Table If Exists #FaturaConciliacaoEnviadoTemp

Create Table #FaturaConciliacaoTmp
(
	Id								Int Identity(1,1) Primary Key
	,FaturaId						Int 
	,FaturaConciliacaoId			Int	
	,FaturaConciliacaoString		Varchar(256)
	,DataConciliacao				Datetime
	,Enviado						Bit Default(0)	
)


Create Table #TempConciliacao
(
	EmpresaIntegracaoItemDetalheId	Int,
	FaturaConciliacaoId				Bigint,	
	FaturaId						Int,
	ConciliacaoId					Bigint,
	Cnpj							Varchar(36),
	NotaFiscal						Varchar(16),
	Serie							Varchar(16),
	ValorFrete						Decimal,
	ValorAdicional					Decimal,
	DataEnvio						Datetime,
	DataProcessamento				Datetime,
	HttpStatus						Varchar(128),
	Sucesso							Bit,			
	Enviado							Bit			
)

Insert Into #FaturaConciliacaoTmp
(
	FaturaId				
	,FaturaConciliacaoId	
	,FaturaConciliacaoString
	,DataConciliacao
)
Select 
	FaturaId				
	,FaturaConciliacaoId	
	,FaturaConciliacaoString = CAST(FaturaConciliacaoId As varchar(256))
	,DataCadastro
From Fretter.FaturaConciliacao (Nolock)
Where FaturaId = @FaturaId

Declare @DataControlePerformance Datetime

Select @DataControlePerformance = DateAdd(Day,-1,MIN(DataConciliacao))
From #FaturaConciliacaoTmp

Select	FaturaConciliacaoId					= Ft.FaturaConciliacaoId
		,EmpresaIntegracaoItemDetalheId		= Ed.Cd_Id
		,FaturaConciliacaoString			= Ft.FaturaConciliacaoString
Into #FaturaConciliacaoEnviadoTemp
FROM #FaturaConciliacaoTmp						Ft (Nolock)
INNER JOIN Tb_Adm_EmpresaIntegracaoItemDetalhe	Ed (Nolock) ON Ft.FaturaConciliacaoString = Ed.Cd_Chave 
Where Ft.FaturaId = @FaturaId
	And Ed.Dt_Cadastro > @DataControlePerformance
	And Ed.Id_EmpresaIntegracaoItem In 
	(
		Select Et.Cd_Id
		From Tb_Adm_EmpresaIntegracao		 Ei(nolock)
		Join Tb_Adm_EmpresaIntegracaoItem	 Et(Nolock) ON Ei.Cd_Id = Et.Id_EmpresaIntegracao
		Where Id_EmpresaIntegracaoConfiguracao = 3 --Conciliacao
			And Ei.Id_Empresa = @EmpresaId
	)

Update Ct
Set Enviado = 1
From #FaturaConciliacaoTmp			Ct 
Join #FaturaConciliacaoEnviadoTemp	Ee On Ct.FaturaConciliacaoId = Ee.FaturaConciliacaoId

;With FaturaConciliacao As
(
	SELECT
		Id									= ROW_NUMBER() OVER (PARTITION BY Fc.FaturaConciliacaoId ORDER BY Ed.Flg_Sucesso Desc, Ed.Dt_Cadastro Desc)
		,EmpresaIntegracaoItemDetalheId		= Ed.Cd_Id
		,FaturaConciliacaoId				= Fc.FaturaConciliacaoId
		,FaturaId							= Fc.FaturaId
		,ConciliacaoId						= Fc.ConciliacaoId
		,NotaFiscal							= Fc.NotaFiscal
		,Serie								= Fc.Serie
		,ValorFrete							= Fc.ValorFrete
		,ValorAdicional						= Fc.ValorAdicional
		,DataEnvio							= ISNULL(Fc.DataAlteracao,(Select Top 1 DataAlteracao From Fretter.FaturaHistorico Fh(Nolock) Where Fh.FaturaStatusIdAnterior = 6 And Fh.FaturaId = Fc.FaturaId  Order By DataAlteracao Desc  )) 
		,DataProcessamento					= Ed.Dt_Cadastro
		,HttpStatus							= Ed.Ds_HttpStatus 
		,Sucesso							= Ed.Flg_Sucesso 
		,Enviado							= 1
	FROM Fretter.FaturaConciliacao					Fc (Nolock)
	Inner Join #FaturaConciliacaoEnviadoTemp		Ft (Nolock) On Fc.FaturaConciliacaoId = Ft.FaturaConciliacaoId		
	Inner Join Tb_Adm_EmpresaIntegracaoItemDetalhe	Ed (Nolock) ON Ft.EmpresaIntegracaoItemDetalheId = Ed.Cd_Id 
	Where Fc.FaturaId = @FaturaId	
)

Insert Into #TempConciliacao
(
	EmpresaIntegracaoItemDetalheId		
	,FaturaConciliacaoId				
	,FaturaId							
	,ConciliacaoId						
	,NotaFiscal							
	,Serie								
	,ValorFrete							
	,ValorAdicional						
	,DataEnvio							
	,DataProcessamento						
	,HttpStatus							
	,Sucesso
	,Enviado 
)
SELECT
	EmpresaIntegracaoItemDetalheId		
	,FaturaConciliacaoId				
	,FaturaId							
	,ConciliacaoId						
	,NotaFiscal							
	,Serie								
	,ValorFrete							
	,ValorAdicional						
	,DataEnvio							
	,DataProcessamento						
	,HttpStatus							
	,Sucesso
	,Enviado 
From FaturaConciliacao
Where Id = 1


Insert Into #TempConciliacao
(
	FaturaConciliacaoId				
	,FaturaId							
	,ConciliacaoId						
	,NotaFiscal							
	,Serie								
	,ValorFrete							
	,ValorAdicional
	,Enviado
)
Select
	FC.FaturaConciliacaoId				
	,FC.FaturaId							
	,FC.ConciliacaoId						
	,FC.NotaFiscal							
	,FC.Serie								
	,FC.ValorFrete							
	,FC.ValorAdicional
	,TMP.Enviado
From #FaturaConciliacaoTmp				TMP
Inner Join Fretter.FaturaConciliacao	FC (NoLock) On TMP.FaturaConciliacaoId = FC.FaturaConciliacaoId
Where Enviado = 0


Select
	EmpresaIntegracaoItemDetalheId		
	,FaturaConciliacaoId				
	,FaturaId							
	,ConciliacaoId						
	,NotaFiscal							
	,Serie								
	,ValorFrete							
	,ValorAdicional						
	,DataEnvio							
	,DataProcessamento						
	,HttpStatus							
	,Sucesso
	,Enviado 
From #TempConciliacao

End