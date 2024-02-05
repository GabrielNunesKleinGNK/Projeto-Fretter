using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities.Fusion;

namespace Fretter.Repository.Maps.Fusion
{
    class TransportadorCnpjMap : IEntityTypeConfiguration<TransportadorCnpj>
    {
        public void Configure(EntityTypeBuilder<TransportadorCnpj> builder)
        {
            builder.ToTable("Tb_Adm_TransportadorCnpj", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int");
            builder.Property(e => e.TransportadorId).HasColumnName("Id_Transportador").HasColumnType("int");
            builder.Property(e => e.CNPJ).HasColumnName("Cd_Cnpj").HasColumnType("char").HasMaxLength(14);
            builder.Property(e => e.Nome).HasColumnName("Ds_Nome").HasColumnType("varchar").HasMaxLength(100);
            builder.Property(e => e.Apelido).HasColumnName("Ds_Apelido").HasColumnType("varchar").HasMaxLength(50);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("bit");

            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.DataAlteracao);
            builder.Ignore(e => e.UsuarioAlteracao);
        }
    }
}
