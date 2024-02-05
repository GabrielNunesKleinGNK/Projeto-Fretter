if COL_LENGTH('Tb_SKU_ProdutoCanal','Id_CanalOrigem') IS NULL
begin
	ALTER TABLE Tb_SKU_ProdutoCanal ADD Id_CanalOrigem Int NULL 

	ALTER TABLE Tb_SKU_ProdutoCanal 
	ADD CONSTRAINT FK_Tb_SKU_ProdutoCanal_Tb_Adm_CanalOrigem
	FOREIGN KEY (Id_CanalOrigem) REFERENCES Tb_Adm_Canal(Cd_Id)
end

if COL_LENGTH('Tb_SKU_ProdutoCanal','Fl_Virtual') IS NULL
	ALTER TABLE Tb_SKU_ProdutoCanal ADD Fl_Virtual BIT DEFAULT(0) NULL