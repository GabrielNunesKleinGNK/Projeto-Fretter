using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities.Fusion;

namespace Fretter.Repository.Maps
{
    class AspNetUsersMap : IEntityTypeConfiguration<AspNetUsers>
    {
        public void Configure(EntityTypeBuilder<AspNetUsers> builder)
        {
            builder.ToTable("AspNetUsers", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.UserName).HasColumnName("UserName").HasColumnType("Varchar").HasMaxLength(512);
            builder.Property(e => e.Ativo).HasColumnName("Flg_Ativo").HasColumnType("Bit");

            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
