using Fretter.Domain.Dto.ArquivoImportacaoLog;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fretter.Domain.Services
{
    public class ArquivoImportacaoService<TContext> : ServiceBase<TContext, ArquivoImportacao>, IArquivoImportacaoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IArquivoImportacaoRepository<TContext> _Repository;

        public ArquivoImportacaoService(IArquivoImportacaoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork,IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }

        public List<ArquivoImportacaoLogDTO> GetLista(ArquivoImportacaoLogFiltro arquivoImportacaoLogFiltro)
        {
            try
            {
                string tipoArquivo = GetTipoArquivo(arquivoImportacaoLogFiltro);

                var retorno = _Repository.GetAll(arquivo =>
                arquivo.DtImportacao >= arquivoImportacaoLogFiltro.DataInicio.Value.ToLocalTime()
                && arquivo.DtImportacao <= arquivoImportacaoLogFiltro.DataTermino.Value.ToLocalTime()
                && arquivo.DsNome.Contains(tipoArquivo)
                ).ToList();

                var result = retorno.Select(s => new ArquivoImportacaoLogDTO(s)).ToList();

                if (!string.IsNullOrEmpty(arquivoImportacaoLogFiltro.RequestNumber))
                    result = result.Where(w =>
                        (w.IsBatch && w.NumeroPedido.Contains(arquivoImportacaoLogFiltro.RequestNumber + ";"))
                        || (!w.IsBatch && w.NumeroPedido == arquivoImportacaoLogFiltro.RequestNumber))
                        .ToList();

                return result;
            }
            catch (Exception e)
            {
                var a = e;
                return null;
            }
        }

        private static string GetTipoArquivo(ArquivoImportacaoLogFiltro arquivoImportacaoLogFiltro)
        {
            if (arquivoImportacaoLogFiltro.DataInicio.Value.Date == arquivoImportacaoLogFiltro.DataTermino.Value.Date)
                arquivoImportacaoLogFiltro.DataInicio = new DateTime(arquivoImportacaoLogFiltro.DataInicio.Value.Year,
                    arquivoImportacaoLogFiltro.DataInicio.Value.Month, arquivoImportacaoLogFiltro.DataInicio.Value.Day);

            var tipoArquivo = "bseller";

            if (arquivoImportacaoLogFiltro.ProcessType == "1")
                tipoArquivo += "_n_lote";
            if (arquivoImportacaoLogFiltro.ProcessType == "2")
                tipoArquivo += "_n_entrega";
            return tipoArquivo;
        }
    }
}	
