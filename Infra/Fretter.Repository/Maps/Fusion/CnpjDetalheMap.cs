using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;

namespace Fretter.Repository.Maps
{
    class CnpjDetalheMap : IEntityTypeConfiguration<CnpjDetalhe>
    {
        public void Configure(EntityTypeBuilder<CnpjDetalhe> builder)
        {
            builder.ToTable("Tb_Adm_CnpjDetalhe", "dbo");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
            builder.Property(e => e.Cnpj).HasColumnName("Cd_Cnpj").HasColumnType("char").HasMaxLength(14);
            builder.Property(e => e.Tipo).HasColumnName("Ds_Tipo").HasColumnType("varchar").HasMaxLength(6);
            builder.Property(e => e.Abertura).HasColumnName("Dt_Abertura").HasColumnType("date").HasMaxLength(3);
            builder.Property(e => e.Logradouro).HasColumnName("Ds_Logradouro").HasColumnType("varchar").HasMaxLength(500);
            builder.Property(e => e.Numero).HasColumnName("Nr_Numero").HasColumnType("varchar").HasMaxLength(10);
            builder.Property(e => e.Complemento).HasColumnName("Ds_Complemento").HasColumnType("varchar").HasMaxLength(500);
            builder.Property(e => e.Cep).HasColumnName("Ds_Cep").HasColumnType("varchar").HasMaxLength(10);
            builder.Property(e => e.Municipio).HasColumnName("Ds_Municipio").HasColumnType("varchar").HasMaxLength(50);
            builder.Property(e => e.UF).HasColumnName("Ds_UF").HasColumnType("char").HasMaxLength(2);
            builder.Property(e => e.Email).HasColumnName("Ds_Email").HasColumnType("varchar").HasMaxLength(200);
            builder.Property(e => e.Telefone).HasColumnName("Ds_Telefone").HasColumnType("varchar").HasMaxLength(50);
            //builder.Property(e => e.CnpjUnico).HasColumnName("Cd_CnpjUnico").HasColumnType("bigint").HasMaxLength(8);

            builder.Ignore(e => e.Ativo);
            builder.Ignore(e => e.UsuarioCadastro);
            builder.Ignore(e => e.UsuarioAlteracao);
            builder.Ignore(e => e.DataCadastro);
            builder.Ignore(e => e.DataAlteracao);
        }
    }
}
