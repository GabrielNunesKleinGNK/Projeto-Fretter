using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class EntregaOrigemImportacaoMap : IEntityTypeConfiguration<EntregaOrigemImportacao>
    {
        public void Configure(EntityTypeBuilder<EntregaOrigemImportacao> builder)
        {
            builder.ToTable("Tb_Edi_EntregaOrigemImportacao", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Nome).HasColumnName("Ds_Nome").HasColumnType("varchar").HasMaxLength(100);


			builder.Ignore(e => e.Ativo);
			builder.Ignore(e => e.UsuarioCadastro);
			builder.Ignore(e => e.UsuarioAlteracao);
			builder.Ignore(e => e.DataCadastro);
			builder.Ignore(e => e.DataAlteracao);
		}
    }
}