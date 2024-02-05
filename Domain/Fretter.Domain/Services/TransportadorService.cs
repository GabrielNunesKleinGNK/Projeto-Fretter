using System;
using System.Collections.Generic;
using Fretter.Domain.Dto.Dashboard;
using Fretter.Domain.Dto.Relatorio;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fusion;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Repository.Fusion;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class TransportadorService<TContext> : ITransportadorService<TContext>
         where TContext : IUnitOfWork<TContext>
    {
        private readonly IGenericRepository<TContext> _repository;
        private readonly ITransportadorCnpjRepository<TContext> _repositoryTransportadorCnpj;
        private readonly ITransportadorRepository<TContext> _repositoryTransportador;
        private readonly IUsuarioHelper _user;

        public TransportadorService(IGenericRepository<TContext> repository, 
                                    IUsuarioHelper user,
                                    ITransportadorCnpjRepository<TContext> repositoryTransportadorCnpj,
                                    ITransportadorRepository<TContext> repositoryTransportador)
        {
            _repositoryTransportador = repositoryTransportador;
            _repositoryTransportadorCnpj = repositoryTransportadorCnpj;
            _repository = repository;
            _user = user;
        }

        public List<Entities.Fusion.Transportador> GetTransportador()
        {
            var list = _repositoryTransportador.ObterTransportadores();
            return list;
        }

        public List<TransportadorCnpj> GetTransportadorCNPJ(int transportadorId)
        {
            var list = _repositoryTransportadorCnpj.ObterTransportadorCnpj(transportadorId);
            return list;
        }

        public List<Dto.Dashboard.Transportador> GetTransportadorPorEmpresa()
        {
            var filtro = new Dto.Dashboard.Transportador() { EmpresaId = _user.UsuarioLogado.EmpresaId};
            return _repository.ExecuteStoredProcedure<Dto.Dashboard.Transportador, Dto.Dashboard.Transportador>(filtro, "[Fretter].[GetTransportadoresDaPorEmpresa]");
        }
    }
}
