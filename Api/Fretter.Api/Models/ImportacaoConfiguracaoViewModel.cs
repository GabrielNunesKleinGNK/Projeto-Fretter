using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ImportacaoConfiguracaoViewModel : IViewModel<ImportacaoConfiguracao>
    {
        public int Id { get; set; }
        public int ImportacaoConfiguracaoTipoId { get; set; }
        public int? EmpresaId { get; set; }
        public int? TransportadorId { get; set; }
        public int ImportacaoArquivoTipoId { get; set; }
        public string Diretorio { get; set; }
        public string DiretorioSucesso { get; set; }
        public string DiretorioErro { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Outro { get; set; }
        public DateTime? UltimaExecucao { get; set; }
        public string UltimoRetorno { get; set; }
        public bool? Sucesso { get; set; }
        public int? UsuarioCadastro { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool? Compactado { get; set; }
        public bool Ativo { get; set; }

        public Fusion.EmpresaViewModel Empresa { get; set; }
        public Fusion.TransportadorViewModel Transportador { get; set; }
        public ImportacaoArquivoTipoViewModel ArquivoTipo { get; set; }
        public ImportacaoConfiguracaoTipoViewModel ConfiguracaoTipo { get; set; }

        public ImportacaoConfiguracao Model()
        {
            return new ImportacaoConfiguracao(Id, (Domain.Enum.EnumImportacaoConfiguracaoTipo)ImportacaoConfiguracaoTipoId, EmpresaId, TransportadorId, (Domain.Enum.EnumImportacaoArquivoTipo)ImportacaoArquivoTipoId, Diretorio, Usuario, Senha, Outro, UltimaExecucao, UltimoRetorno, Sucesso, Compactado, DiretorioSucesso, DiretorioErro);
        }
    }
}
