IF NOT EXISTS(SELECT TOP 1 1 FROM tb_sis_menu WHERE Ds_Menu = 'Agendamento' AND Flg_Fretter = 1)
BEGIN
    DECLARE
        @Id_MenuAgendamento INT,
        @Id_MenuExpedicao INT,
        @Max_OrdemMenuPaiFretter INT

    --Pega a ultima ordem de menu e adiciona mais um
    SELECT
        @Max_OrdemMenuPaiFretter = MAX(Nr_Ordem) + 1 
    FROM
        tb_sis_menu 
    WHERE
        Flg_Fretter = 1 AND 
        Id_Pai IS NULL

    --Menu PAI
	INSERT INTO tb_sis_menu
    (
        Ds_Menu,
        Id_Pai,
        Flg_Ativo,
        Ds_Link,
        Nr_Ordem,
        Tp_Perfil,
        Flg_Fretter
    )
	VALUES
    (
		'Agendamento',
        NULL,
        1,
        '',
        @Max_OrdemMenuPaiFretter,
        NULL,
        1
    )

    SELECT 
        @Id_MenuAgendamento = cd_id 
    FROM
        tb_sis_menu
    WHERE
        Ds_Menu = 'Agendamento' AND 
        Flg_Fretter = 1

    --Menu Filho
    INSERT INTO tb_sis_menu
    (
        Ds_Menu,
        Id_Pai,
        Flg_Ativo,
        Ds_Link,
        Nr_Ordem,
        Tp_Perfil,
        Flg_Fretter
    )
	VALUES
    (
		'Expedição',
        @Id_MenuAgendamento,
        1,
        '/agendamentoExpedicao',
        1,
        NULL,
        1
    )

    SELECT 
        @Id_MenuExpedicao = cd_id 
    FROM
        tb_sis_menu
    WHERE
        Ds_Menu = 'Expedição' AND
        Id_Pai = @Id_MenuAgendamento AND
        Flg_Fretter = 1

    --Icone Menu Pai
    INSERT INTO tb_sis_menu_ico
	(
        Id_Menu, 
        Ds_Icone
    )
	values
    (
        @Id_MenuAgendamento,
        'fa fa-calendar'
    )

    --Icone Menu Pai
    INSERT INTO tb_sis_menu_ico
	(
        Id_Menu, 
        Ds_Icone
    )
	values
    (
        @Id_MenuExpedicao,
        'flaticon-cogwheel'
    )
END
GO