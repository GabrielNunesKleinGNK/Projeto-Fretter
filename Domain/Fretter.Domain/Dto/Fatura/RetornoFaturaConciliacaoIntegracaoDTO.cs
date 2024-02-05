using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fretter.Domain.Dto.Fatura
{
    public class RetornoFaturaConciliacaoIntegracaoDTO
    {
        public RetornoFaturaConciliacaoIntegracaoDTO(List<FaturaConciliacaoIntegracaoDTO> integracoes, decimal valorTotal, DateTime? dataUltimoEnvio)
        {
            Integracoes = integracoes;

            IntegracoesSucesso = Integracoes.Where(x => x.Sucesso && x.Enviado).ToList();
            IntegracoesInsucesso = Integracoes.Where(x => !x.Sucesso && x.Enviado).ToList();
            IntegracoesNaoDefinida = Integracoes.Where(x => !x.Enviado).ToList();

            QtdeTotal = Integracoes?.Count ?? 0;
            QtdeSucesso = IntegracoesSucesso?.Count ?? 0;
            QtdeInsucesso = IntegracoesInsucesso?.Count ?? 0;
            QtdeNaoDefinida = IntegracoesNaoDefinida?.Count ?? 0;

            ValorTotal = valorTotal;
            DataUltimoEnvio = dataUltimoEnvio;
        }

        public int QtdeTotal { get; private set; }
        public int QtdeSucesso { get; private set; }
        public int QtdeInsucesso { get; private set; }
        public int QtdeNaoDefinida { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime? DataUltimoEnvio { get; private set; }

        public List<FaturaConciliacaoIntegracaoDTO> Integracoes { get; private set; }
        public List<FaturaConciliacaoIntegracaoDTO> IntegracoesSucesso { get; private set; }
        public List<FaturaConciliacaoIntegracaoDTO> IntegracoesInsucesso { get; private set; }
        public List<FaturaConciliacaoIntegracaoDTO> IntegracoesNaoDefinida { get; private set; }
    }
}
