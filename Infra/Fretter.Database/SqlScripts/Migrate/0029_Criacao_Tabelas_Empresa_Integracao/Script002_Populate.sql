If Not Exists(Select Top 1 1 From Tb_Sis_Menu Where Ds_Link = '/empresaIntegracao')
	Begin
		Insert Into Tb_Sis_Menu Values ('Config. Integração', 218, 1, '/empresaIntegracao', 3, null, 1);
		Insert Into Tb_Sis_Menu_Ico Values (@@IDENTITY, 'flaticon-cogwheel');
	End