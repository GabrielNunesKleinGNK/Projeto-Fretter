using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EmpresaTokenMap : IEntityTypeConfiguration<EmpresaToken>
    {
        public void Configure(EntityTypeBuilder<EmpresaToken> builder)
        {
            builder.ToTable("Tb_Adm_EmpresaToken", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EmpresaTokenTipoId).HasColumnName("Id_EmpresaTokenTipo").HasColumnType("tinyint").HasMaxLength(1);
			builder.Property(e => e.Nome).HasColumnName("Ds_Nome").HasColumnType("varchar").HasMaxLength(50);
			builder.Property(e => e.Token).HasColumnName("Id_Token").HasColumnType("uniqueidentifier").HasMaxLength(16);
			builder.Property(e => e.Padrao).HasColumnName("Fl_Padrao").HasColumnType("bit").HasMaxLength(1);
			builder.Property(e => e.Ativo).HasColumnName("Fl_Ativo").HasColumnType("bit").HasMaxLength(1);
			//builder.Property(e => e.PadraoUnico).HasColumnName("Fl_PadraoUnico").HasColumnType("varchar").HasMaxLength(30);
			builder.Property(e => e.AspNetUsers).HasColumnName("Id_AspNetUsers").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Inclusao).HasColumnName("Dt_Inclusao").HasColumnType("smalldatetime").HasMaxLength(4);
			builder.Property(e => e.OrigemImportacao).HasColumnName("Id_OrigemImportacao").HasColumnType("tinyint").HasMaxLength(1);

			builder.Ignore(e => e.UsuarioCadastro);
			builder.Ignore(e => e.UsuarioAlteracao);
			builder.Ignore(e => e.DataCadastro);
			builder.Ignore(e => e.DataAlteracao);
		}
    }
}
