using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class ConfiguracaoCteTransportadorMap : IEntityTypeConfiguration<ConfiguracaoCteTransportador>
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoCteTransportador> builder)
        {
            builder.ToTable(nameof(ConfiguracaoCteTransportador), "Fretter");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName($"{nameof(ConfiguracaoCteTransportador)}Id").HasColumnType("int");
			builder.Property(e => e.Alias).HasColumnType("varchar").HasMaxLength(14);
            builder.Property(e => e.TransportadorCnpjId).HasColumnType("int");
            builder.Property(e => e.ConfiguracaoCteTipoId).HasColumnType("int").IsRequired();
            builder.Property(e => e.EmpresaId).HasColumnType("int").IsRequired();

            builder.HasOne(x => x.TransportadorCnpj).WithMany().HasForeignKey(x => x.TransportadorCnpjId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ConfiguracaoCteTipo).WithMany().HasForeignKey(x => x.ConfiguracaoCteTipoId).OnDelete(DeleteBehavior.Restrict);

            //BaseMapping
            builder.Property(e => e.DataCadastro).HasColumnType("DateTime");
            builder.Property(e => e.DataAlteracao).HasColumnType("DateTime");
            builder.Property(e => e.Ativo).HasColumnType("bit");
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);

        }
    }
}
