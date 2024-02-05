Create Or Alter Procedure Fretter.ProcessaEmpresaPermissao
(
	@Email				Varchar(256)
	,@Tipo_permissao	Int				= 0 
	,@Cd_Cnpj			Varchar(14)	
)
AS BEGIN                    
Set Nocount ON;

Declare @role_id			Varchar(128)    = NULL
		,@marketplace		Int				= 1

If(@Tipo_permissao = 0 Or @Tipo_permissao Is NULL)
	Set @Tipo_permissao = 2;

If(@Tipo_permissao = 2)
	Set @role_id = 'c6dbed73-4169-45f0-a37a-efda11f19bff';
Else Set @role_id = '1C0FDBF7-7EC8-4D7B-BEE6-9140E18604CF';

If(@marketplace = 0 Or @marketplace Is NULL)
	Set @marketplace = 1;

/*********************************************************************************************************************************
ADICIONA USUARIO SE NAO EXISTIR (ROLE ID)
VERIFICA PERMISSAO DE ACESSO DO USUARIO E INCLUI TELAS SE NAO EXISTIR NO PERFIL
INCLUI ACESSO A EMPRESA SE NAO EXISTIR
INCLUI USUARIOS ADM MARKETPLACE

Roleid:
MF MKP Seller    (Kabum): 1C0FDBF7-7EC8-4D7B-BEE6-9140E18604CF
MF MKP Seller II (Kabum): c6dbed73-4169-45f0-a37a-efda11f19bff

MARKETPLACE: Usuarios complementares administradores do marketlace
Ser� adicionado acesso desses usu�rios a empresa escolhida
0 - SEM ACESSOS ADICIONAIS
1 - KABUM parametrizacao_mkp@kabum.com.br (18618), treinamento_mkp@kabum.com.br(18619)
,parametrizacao_mkp2@kabum.com.br(21342), parametrizacao_mkp3@kabum.com.br(21343), 
parametrizacao_mkp4@kabum.com.br(21344), parametrizacao_mkp5@kabum.com.br(21345)

TIPOS (acesso a telas):
1 - MF MKP Seller (Kabum) - Tabela de Frete (124), Acompanhe (10, 32, 138) e Inserir Ocorrencia (77)
2 - MF MKP Seller + Politicas (Kabum) - Tipo 1 + Politicas (127) + Grupo Regras (193)
**********************************************************************************************************************************/
DECLARE @Id_Empresa int

DECLARE @Msg varchar(max)
SET @Msg = 'INICIO'

Begin Try

SELECT @Id_Empresa = E.Cd_Id FROM [dbo].[Tb_Adm_Empresa] E
		JOIN [dbo].[Tb_Adm_Canal] C ON E.Cd_Id = C.Id_Empresa
		WHERE C.[Cd_Cnpj] = @Cd_Cnpj

IF @Id_Empresa IS NOT NULL
	BEGIN

	-- CLAIMS ADICIONAIS MARKETPLACE
	IF @marketplace = 1
		BEGIN

			INSERT INTO [dbo].[AspNetUserClaims] (UserId, ClaimType, ClaimValue) 
            SELECT PE.UserId, 'Empresa',  cast(@Id_Empresa as nvarchar)
            FROM Fretter.ProcessaEmpresaPermissaoUsuarios PE 
                Left Join [dbo].[AspNetUserClaims] cla On cla.[UserId] = PE.[UserId] and cla.[ClaimType] = 'Empresa' AND cla.[ClaimValue] = cast(@Id_Empresa as nvarchar)
            WHERE cla.[Id] IS NULL
			
			END
		
		
		DECLARE @UserId int
		SELECT  @UserId = Cd_Id FROM [dbo].[AspNetUsers] WHERE [Email] = @Email

		IF @UserId IS NOT NULL
			BEGIN
		
				DECLARE @Id_UserClaims_Empresa int
				SELECT @Id_UserClaims_Empresa = Id FROM [dbo].[AspNetUserClaims] 
					WHERE [UserId] = @UserId AND [ClaimType] = 'Empresa' AND [ClaimValue] = cast(@Id_Empresa as nvarchar)

				IF @Id_UserClaims_Empresa IS NULL
					BEGIN
						INSERT INTO [dbo].[AspNetUserClaims] (UserId, ClaimType, ClaimValue) 
							VALUES (@UserId, 'Empresa', cast(@Id_Empresa as nvarchar))
						SET @Msg = CONCAT(@Msg, ', nova claim usr x empresa')
					END
		
				DROP TABLE IF EXISTS #Sis_RoleMenu
				DROP TABLE IF EXISTS #Menus_permissao
				DROP TABLE IF EXISTS #Adicionar

				DECLARE @RoleId nvarchar(128)
				SELECT TOP 1 @RoleId = RoleId FROM [dbo].[AspNetUserRoles] WHERE UserId = @UserId

				CREATE TABLE #Sis_RoleMenu (Id_Menu int)
				INSERT INTO #Sis_RoleMenu
					SELECT Id_Menu FROM [dbo].[Tb_Sis_RoleMenu] WHERE Id_Role = @RoleId

				CREATE TABLE #UserClaimsMenu (Id_Menu int)
				INSERT INTO #UserClaimsMenu
					SELECT ClaimValue FROM [dbo].[AspNetUserClaims] WHERE UserId = @UserId AND ClaimType = 'Menu'

				IF @Tipo_permissao in (1,2)
					BEGIN
						CREATE TABLE #Menus_permissao (Id_Menu int)
						
						IF @Tipo_permissao = 1
							BEGIN
								INSERT INTO #Menus_permissao (Id_Menu) VALUES (124),(10),(32),(138),(77)
							END

						IF @Tipo_permissao = 2
							BEGIN
								INSERT INTO #Menus_permissao (Id_Menu) VALUES (124),(10),(32),(138),(77),(127),(193)
							END

						CREATE TABLE #Adicionar (Id_Menu int)
						INSERT INTO #Adicionar
							SELECT A.Id_Menu FROM #Menus_permissao A
							LEFT JOIN #Sis_RoleMenu B ON A.Id_Menu = B.Id_Menu
							LEFT JOIN #UserClaimsMenu C ON A.Id_Menu = C.Id_Menu
							WHERE B.Id_Menu IS NULL AND C.Id_Menu IS NULL

						DECLARE @Qnt_Add int
						SELECT @Qnt_Add = COUNT(*) FROM #Adicionar

						--Print CONCAT('Menus a adicionar: ', @Qnt_Add)

						IF @Qnt_Add > 0
							BEGIN

								DECLARE cursor_adicionar CURSOR
									FOR SELECT Id_Menu FROM #Adicionar;
						
								OPEN cursor_adicionar;

								DECLARE @Id_Menu_Add int
								FETCH NEXT FROM cursor_adicionar INTO @Id_Menu_Add;

								WHILE @@FETCH_STATUS = 0
									BEGIN
										--Print CONCAT('Adicionando menu: ', @Id_Menu_Add)
										INSERT INTO AspNetUserClaims (UserId, ClaimType, ClaimValue) VALUES (@UserId, 'Menu', @Id_Menu_Add)
										FETCH NEXT FROM cursor_adicionar INTO @Id_Menu_Add;
									END

								CLOSE cursor_adicionar;
								DEALLOCATE cursor_adicionar;

								--Print CONCAT('Permissao de acesso ajustada: ', @Email)
								SET @Msg = CONCAT(@Msg, ', permissao de acesso ajustada: ', @Email)

							END
						ELSE
							BEGIN
								--Print CONCAT('Sem menus a adicionar: ', @Email)
								SET @Msg = CONCAT(@Msg, ', sem menus a adicionar: ', @Email)
							END

					END

				ELSE
					BEGIN
						--Print CONCAT('Permissao nao disponivel: ', @Tipo_permissao)
						SET @Msg = CONCAT(@Msg, ', permissao nao disponivel: ', @Tipo_permissao)
					END
			END
		ELSE
			BEGIN
		
				INSERT INTO [dbo].[ASpNetUsers]
					( [Id]
					, [Email]
					, [EmailConfirmed]
					, [PasswordHash]
					, [SecurityStamp]
					, [PhoneNumberConfirmed]
					, [TwoFactorEnabled]
					, [LockoutEnabled]
					, [AccessFailedCount]
					, [UserName]
					, [Flg_Ativo] )
				VALUES ( newid()
					, @Email
					, 0
					, 'ALgrseGv9uI2+LGcEQUBGNsAboOGFOqGV8VT00asFONo+DwO0vsjaPQEg4C3j+FSGw=='
					, newid()
					, 0
					, 0
					, 0
					, 0
					, @Email
					, 1 )

				DECLARE @user_id INT = @@IDENTITY

				INSERT INTO [dbo].[AspNetUserClaims]
					( [UserId]
					, [ClaimType]
					, [ClaimValue] )
				VALUES ( @user_id
					, 'Empresa'
					, @Id_Empresa )

				INSERT INTO [dbo].[AspNetUserClaims]
					( [UserId]
					, [ClaimType]
					, [ClaimValue] )
				VALUES ( @user_id
					, 'Ativo'
					, 'true' )

				INSERT INTO [dbo].[AspNetUserRoles]
					( [UserId]
					, [RoleId] )
				VALUES ( @user_id
					, @role_id )

				--Print CONCAT('Usu�rio criado: ', @Email)
				SET @Msg = CONCAT(@Msg, ', usuario criado: ', @Email)

			END

		DROP TABLE IF EXISTS #Sis_RoleMenu
		DROP TABLE IF EXISTS #Menus_permissao
		DROP TABLE IF EXISTS #Adicionar
	END
ELSE
	BEGIN
		-- Print CONCAT('Canal (Filial) n�o localizado: ', @Cd_Cnpj)
		SET @Msg = CONCAT(@Msg, ' canal (filial) nao localizado: ', @Cd_Cnpj)
	END

SET @Msg = CONCAT(@Msg, ', FIM')

INSERT INTO [dbo].[Tb_Sis_RotinasImplantacao]
	(Ds_NomeProcesso
	, Dt_Inclusao
	, In_Cd_Cnpj
	, In_Email
	, In_TipoPermissao
	, In_RoleID
	, In_Marketplace
	, Out_Id_Empresa
	, Out_UserId
	, Out_Acoes)
VALUES
	('Pr_Imp_FixPermissaoUsr'
	, getdate()
	, @Cd_Cnpj
	, @Email
	, @Tipo_permissao
	, @RoleId
	, @marketplace
	, @Id_Empresa
	, ISNULL(@UserId, @user_id)
	, @Msg)

	Select 1;
End Try
Begin Catch

	Declare @MessageError Varchar(2048) = 'Houve um erro ao processar a Permissao. Erro : ' + ERROR_MESSAGE ();
	--RaisError(@MessageError,18,1)
	Select 0;

	Return;
End Catch

END