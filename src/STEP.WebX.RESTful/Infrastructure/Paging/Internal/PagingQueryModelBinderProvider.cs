using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace STEP.WebX.RESTful.Paging
{
    internal class PagingQueryModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType != typeof(PagingQueryModel))
                return null;

            return new BinderTypeModelBinder(typeof(PagingQueryModelBinder));
        }
    }
}
