using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EmpresaTokenTipoMap : IEntityTypeConfiguration<EmpresaTokenTipo>
    {
        public void Configure(EntityTypeBuilder<EmpresaTokenTipo> builder)
        {
            builder.ToTable("Tb_Adm_EmpresaTokenTipo", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("tinyint").HasMaxLength(1);
			builder.Property(e => e.TokenTipo).HasColumnName("Ds_TokenTipo").HasColumnType("varchar").HasMaxLength(30);

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
