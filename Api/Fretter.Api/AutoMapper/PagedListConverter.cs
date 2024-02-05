using AutoMapper;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Api.AutoMapper
{
    public class PagedListConverter<TEntity, TViewModel> //: ITypeConverter<PagedList<TEntity>, PagedList<TViewModel>>
    {
        public readonly IMapper _mapper = ServiceLocator.Resolve<IMapper>();

        public PagedList<TViewModel> Convert(IPagedList<TEntity> source)
        {
            var vm = source.Data.Select(item => _mapper.Map<TViewModel>(item)).ToList();
            return new PagedList<TViewModel>(vm, source.Total);
        }
    }
}
