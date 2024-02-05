using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fretter.Domain.Entities
{
    public class Empresa : EntityBase
    {
        #region "Construtores"       
        public Empresa(int Id, DateTime Inclusao, string RazaoSocial, string NomeFantasia, string Cnpj, Guid TokenId, Guid TokenConsultaId, bool Marketplace, Guid TokenWebHookId, bool DesconsideraTransportadorNoRatreio, bool? TrocaTransportadorNoRoteiro, int? OrigemImportacaoId, bool CalculaPrazoEntregaMicroServico, bool? NaoUsaEndpointExterno)
        {
            this.Ativar();
            this.Id = Id;
            this.NaoUsaEndpointExterno = NaoUsaEndpointExterno;
            this.Inclusao = Inclusao;
            this.Ativo = Ativo;
            this.RazaoSocial = RazaoSocial;
            this.NomeFantasia = NomeFantasia;
            this.Cnpj = Cnpj;
            this.TokenId = TokenId;
            this.TokenConsultaId = TokenConsultaId;
            this.Marketplace = Marketplace;
            this.TokenWebHookId = TokenWebHookId;
            this.DesconsideraTransportadorNoRatreio = DesconsideraTransportadorNoRatreio;
            this.TrocaTransportadorNoRoteiro = TrocaTransportadorNoRoteiro;
            this.OrigemImportacaoId = OrigemImportacaoId;
            this.CalculaPrazoEntregaMicroServico = CalculaPrazoEntregaMicroServico;
            this.EmpresaTransportadorConfigs = new List<EmpresaTransportadorConfig>();
            //this.CanalConfigs = new List<CanalConfig>();
            //this.Canais = new List<Canal>();
            //this.EmpresaSegmentos = new List<EmpresaSegmento>();
            this.EmpresaConfigs = new List<EmpresaConfig>();
            //this.EmpresaImportacaoDetalhes = new List<EmpresaImportacaoDetalhe>();
        }
        #endregion

        #region "Propriedades"		
        public DateTime Inclusao { get; protected set; }
        public string RazaoSocial { get; protected set; }
        public string NomeFantasia { get; protected set; }
        public string Cnpj { get; protected set; }
        public Guid TokenId { get; protected set; }
        public Guid TokenConsultaId { get; protected set; }
        public string TomticketId { get; protected set; }
        public bool Marketplace { get; protected set; }
        [NotMapped]
        public long? CnpjUnico { get; protected set; }
        [NotMapped]
        public string NomeFantasiaUnico { get; protected set; }
        public Guid TokenWebHookId { get; protected set; }
        [NotMapped]
        public string Formatado { get; protected set; }
        public bool DesconsideraTransportadorNoRatreio { get; protected set; }
        public bool? TrocaTransportadorNoRoteiro { get; protected set; }
        public int? OrigemImportacaoId { get; protected set; }
        public bool? ControlaSaldoProdutos { get; protected set; }
        public bool CalculaPrazoEntregaMicroServico { get; protected set; }
        public bool? NaoUsaEndpointExterno { get; protected set; }
        #endregion

        #region "Referencias"
        public ICollection<EmpresaTransportadorConfig> EmpresaTransportadorConfigs { get; private set; }
        public ICollection<CanalConfig> CanalConfigs { get; private set; }
        //public ICollection<Canal> Canais { get; private set; }
        //public ICollection<EmpresaSegmento> EmpresaSegmentos { get; private set; }
        public EmpresaSegmento EmpresaSegmento { get; private set; }
        public Canal Canal { get; private set; }
        public ICollection<EmpresaConfig> EmpresaConfigs { get; private set; }
        public ICollection<EmpresaImportacaoDetalhe> EmpresaImportacaoDetalhes { get; private set; }
        #endregion

        #region "Métodos"
        public void AtualizarNaoUsaEndpointExterno(bool NaoUsaEndpointExterno) => this.NaoUsaEndpointExterno = NaoUsaEndpointExterno;
        public void AtualizarRazaoSocial(string razaoSocial) => this.RazaoSocial = razaoSocial;
        public void AtualizarNomeFantasia(string nomeFantasia) => this.NomeFantasia = nomeFantasia;
        public void AtualizarCnpj(string cnpj) => this.Cnpj = cnpj;
        public void AtualizarToken(Guid token) => this.TokenId = token;
        public void AtualizarTokenConsulta(Guid tokenConsulta) => this.TokenConsultaId = tokenConsulta;
        public void AtualizarMarketPlace(bool markePlace) => this.Marketplace = markePlace;
        public void AtualizarTokenWebhook(Guid tokenWebhook) => this.TokenWebHookId = tokenWebhook;
        public void AtualizarOrigemImportacao(int origemImportacao) => this.OrigemImportacaoId = origemImportacao;
        public void AtualizarSegmento(EmpresaSegmento segmento) => this.EmpresaSegmento = segmento;
        public void AtualizarCanal(Canal canal) => this.Canal = canal;      
        #endregion
    }
}
