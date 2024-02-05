using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using Fretter.Domain.Entities;
using ITask.Api.Filters;

namespace Fretter.Api.Filters
{
    public class ProviderModelBinder : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(QueryFilter))
                return new BinderTypeModelBinder(typeof(QueryFilterModelBinder));

            return null;
        }
    }
}
