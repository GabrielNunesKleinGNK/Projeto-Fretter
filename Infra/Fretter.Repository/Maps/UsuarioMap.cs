using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("UsuarioId").HasColumnType("int").HasMaxLength(4);
            //builder.Property(e => e.ClienteId).HasColumnName("ClienteId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Nome).HasColumnName("Nome").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.Login).HasColumnName("Login").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.Email).HasColumnName("Email").HasColumnType("varchar").HasMaxLength(128);
            builder.Property(e => e.Senha).HasColumnName("Senha").HasColumnType("varchar").HasMaxLength(256);
            builder.Property(e => e.Avatar).HasColumnName("Avatar").HasColumnType("varchar").HasMaxLength(4000);
            builder.Property(e => e.ForcarTrocaSenha).HasColumnName("ForcarTrocaSenha").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.Ativo).HasColumnName("Ativo").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.DataCadastro).HasColumnName("DataCadastro").HasColumnType("datetime").HasMaxLength(8).IsRequired();
            builder.Property(e => e.DataAlteracao).HasColumnName("DataAlteracao").HasColumnType("datetime").HasMaxLength(8);
            builder.Property(e => e.UsuarioCadastro).HasColumnName("UsuarioCadastro").HasColumnType("int").HasMaxLength(4).IsRequired();
            builder.Property(e => e.UsuarioAlteracao).HasColumnName("UsuarioAlteracao").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.UsuarioTipoId).HasColumnName("UsuarioTipoId").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Telefone).HasColumnName("Telefone").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.Cargo).HasColumnName("Cargo").HasColumnType("varchar").HasMaxLength(64);
            builder.Property(e => e.Bloqueado).HasColumnName("Bloqueado").HasColumnType("bit").HasMaxLength(1);
            builder.Property(e => e.ApiKey).HasColumnName("ApiKey").HasColumnType("varchar").HasMaxLength(256);

            //builder.HasOne(u => u.GrupoPosVenda).WithMany().HasForeignKey(u => u.GrupoPosVendaId);
            builder.Ignore(e => e.SenhaAlterada);
            //builder.HasQueryFilter(p => p.Ativo);
        }
    }
}
