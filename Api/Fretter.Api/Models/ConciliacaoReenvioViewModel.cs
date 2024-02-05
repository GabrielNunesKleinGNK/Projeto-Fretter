using Fretter.Api.Helpers;
using Fretter.Domain.Entities.Fretter;

namespace Fretter.Api.Models
{
    public class ConciliacaoReenvioViewModel : IViewModel<ConciliacaoReenvio>
    {
        public long FaturaConciliacaoId { get; set; }
        public int FaturaId { get; set; }
        public long ConciliacaoId { get; set; }

        public ConciliacaoReenvio Model()
        {
            return new ConciliacaoReenvio(FaturaConciliacaoId, FaturaId, ConciliacaoId);
        }
    }
}
