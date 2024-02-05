
using Fretter.Domain.Dto.RegraEstoque;
using Fretter.Domain.Entities.Fusion.SKU;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Repository.Fusion;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Domain.Services
{
    public class RegraEstoqueService<TContext> : ServiceBase<TContext, RegraEstoque>, IRegraEstoqueService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IRegraEstoqueRepository<TContext> _Repository;

        public RegraEstoqueService( IRegraEstoqueRepository<TContext> Repository,
                                    IUnitOfWork<TContext> unitOfWork,
                                    IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }

        public override RegraEstoque Save(RegraEstoque entidade)
        {
            entidade.AtualizarEmpresaId(_user.UsuarioLogado.EmpresaId);
            entidade.AtualizarDataCriacao();

            if (_Repository.GetByCanalDestino(entidade.CanalIdDestino)?.Id > 0)
                throw new System.Exception("Já existe uma regra cadastrada para este canal de destino");

            RegraEstoqueDTO regraEstoqueDTO = PreencherDto(entidade);
            regraEstoqueDTO = _Repository.SaveWithProcedure(regraEstoqueDTO);

            AtualizarIdRegraEstoque(regraEstoqueDTO, entidade);
            return entidade;
        }

        public override RegraEstoque Update(RegraEstoque entidade)
        {
            entidade.AtualizarUsuarioAlteracao(_user.UsuarioLogado.Id);
            entidade.AtualizarDataAlteracao();

            RegraEstoqueDTO regraEstoqueDTO = PreencherDto(entidade);
            regraEstoqueDTO = _Repository.SaveWithProcedure(regraEstoqueDTO);

            AtualizarIdRegraEstoque(regraEstoqueDTO, entidade);
            return entidade;
        }

        public override void Delete(int chave)
        {
            RegraEstoque entidade = _Repository.Get(chave);
            entidade.Inativar();
            entidade.AtualizarUsuarioAlteracao(_user.UsuarioLogado.Id);
            entidade.AtualizarDataAlteracao();

            RegraEstoqueDTO regraEstoqueDTO = PreencherDto(entidade);
            regraEstoqueDTO = _Repository.SaveWithProcedure(regraEstoqueDTO); 
        }

        private RegraEstoqueDTO PreencherDto(RegraEstoque entidade)
        {
            RegraEstoqueDTO regraEstoqueDTO = new RegraEstoqueDTO();
            regraEstoqueDTO.id               = entidade.Id;
            regraEstoqueDTO.empresaId = entidade.EmpresaId;
            regraEstoqueDTO.canalIdOrigem = entidade.CanalIdOrigem;
            regraEstoqueDTO.canalIdDestino = entidade.CanalIdDestino;
            regraEstoqueDTO.grupoId = entidade.GrupoId;
            regraEstoqueDTO.skus = entidade.Skus;
            regraEstoqueDTO.UsuarioCadastro = entidade.UsuarioCadastro;
            regraEstoqueDTO.UsuarioAlteracao = entidade.UsuarioAlteracao;
            regraEstoqueDTO.DataCadastro = entidade.DataCadastro;
            regraEstoqueDTO.DataAlteracao = entidade.DataAlteracao;
            regraEstoqueDTO.Ativo = entidade.Ativo;

            return regraEstoqueDTO;
        }

        private void AtualizarIdRegraEstoque(RegraEstoqueDTO regraEstoqueDTO, RegraEstoque entidade) => entidade.Id = entidade.Id == 0 ? regraEstoqueDTO.id : entidade.Id;
    }
}	
