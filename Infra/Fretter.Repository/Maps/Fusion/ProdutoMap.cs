using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fusion.EDI;

namespace Fretter.Repository.Maps
{
    class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Tb_SKU_Produto", "dbo");
            builder.HasKey(e => e.Id);
			builder.Property(e => e.Id).HasColumnName("Cd_Id").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.EmpresaId).HasColumnName("Id_Empresa").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Codigo).HasColumnName("Cd_Codigo").HasColumnType("varchar").HasMaxLength(50);
			builder.Property(e => e.CodigoPai).HasColumnName("Cd_CodigoPai").HasColumnType("varchar").HasMaxLength(50);
			builder.Property(e => e.Nome).HasColumnName("Ds_Nome").HasColumnType("varchar").HasMaxLength(200);
			builder.Property(e => e.Complementar).HasColumnName("Ds_Complementar").HasColumnType("varchar").HasMaxLength(maxLength:int.MaxValue);
			builder.Property(e => e.Unidade).HasColumnName("Id_Unidade").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Preco).HasColumnName("Nr_Preco").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.PrecoCusto).HasColumnName("Nr_PrecoCusto").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.PesoBruto).HasColumnName("Nr_PesoBruto").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.PesoLiq).HasColumnName("Nr_PesoLiq").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Gtin).HasColumnName("Ds_Gtin").HasColumnType("varchar").HasMaxLength(50);
			builder.Property(e => e.NomeFornecedor).HasColumnName("Ds_NomeFornecedor").HasColumnType("varchar").HasMaxLength(100);
			builder.Property(e => e.Marca).HasColumnName("Ds_Marca").HasColumnType("varchar").HasMaxLength(100);
			builder.Property(e => e.ClassificacaoFiscal).HasColumnName("Cd_ClassFiscal").HasColumnType("varchar").HasMaxLength(50);
			builder.Property(e => e.Cest).HasColumnName("Cd_Cest").HasColumnType("varchar").HasMaxLength(50);
			builder.Property(e => e.ItensPorCaixa).HasColumnName("Nr_ItensPorCaixa").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Crossdocking).HasColumnName("Nr_Crossdocking").HasColumnType("int").HasMaxLength(4);
			builder.Property(e => e.Largura).HasColumnName("Nr_Largura").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Altura).HasColumnName("Nr_Altura").HasColumnType("decimal").HasMaxLength(9);
			builder.Property(e => e.Comprimento).HasColumnName("Nr_Comprimento").HasColumnType("decimal").HasMaxLength(9);


			builder.Ignore(e => e.UsuarioCadastro);
			builder.Ignore(e => e.UsuarioAlteracao);
			builder.Ignore(e => e.DataCadastro);
			builder.Ignore(e => e.DataAlteracao);
			builder.Ignore(e => e.Ativo);
		}
    }
}