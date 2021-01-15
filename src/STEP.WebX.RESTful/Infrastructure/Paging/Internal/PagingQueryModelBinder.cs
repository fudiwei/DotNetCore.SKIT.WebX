using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;

namespace STEP.WebX.RESTful.Paging
{
    using Exceptions;

    internal class PagingQueryModelBinder : IModelBinder
    {
        const string QUERY_KEY_OFFSET = "offset";
        const string QUERY_KEY_PAGE = "page";
        const string QUERY_KEY_LIMIT = "limit";
        const string QUERY_KEY_ORDERBY = "order_by";
        const string QUERY_KEY_REQUIRECOUNT = "require_count";

        const string ORDER_BY_ASC = "asc";
        const string ORDER_BY_DESC = "desc";

        readonly int MAX_PAGE = (int)Math.Pow(2, 32) - 1;
        readonly int MAX_LIMIT = (int)Math.Pow(2, 16) - 1;

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            HttpRequest request = bindingContext.HttpContext.Request;

            if (!TryGetQueryValueOfOffset(request, out string offset))
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"The value of {QUERY_KEY_OFFSET} is invalid.");
                throw new BadRequest400InvalidQueryException(QUERY_KEY_OFFSET);
            }

            if (!TryGetQueryValueOfPage(request, out int page))
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"The value of {QUERY_KEY_PAGE} is invalid.");
                throw new BadRequest400InvalidQueryException(QUERY_KEY_PAGE);
            }

            if (!TryGetQueryValueOfLimit(request, out int limit))
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"The value of {QUERY_KEY_LIMIT} is invalid.");
                throw new BadRequest400InvalidQueryException(QUERY_KEY_LIMIT);
            }

            if (!TryGetQueryValueOfOrderBy(request, out IReadOnlyList<PagingSortField> orderbyFields))
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"The value of {QUERY_KEY_ORDERBY} is invalid.");
                throw new BadRequest400InvalidQueryException(QUERY_KEY_ORDERBY);
            }

            if (!TryGetQueryValueOfRequireCount(request, out bool requireCount))
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, $"The value of {QUERY_KEY_REQUIRECOUNT} is invalid.");
                throw new BadRequest400InvalidQueryException(QUERY_KEY_REQUIRECOUNT);
            }

            PagingQueryModel model = new PagingQueryModel(
                offset: offset,
                page: page,
                limit: limit,
                orderbyFields: orderbyFields,
                requireCount: requireCount
            );
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, model, null);
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }

        private bool TryGetQueryValueOfOffset(HttpRequest request, out string result)
        {
            result = null;

            if (request.Query.TryGetValue(QUERY_KEY_OFFSET, out StringValues values))
            {
                if (!StringValues.IsNullOrEmpty(values))
                {
                    result = values[0];
                }
            }

            return true;
        }

        private bool TryGetQueryValueOfPage(HttpRequest request, out int result)
        {
            result = 1;

            if (request.HttpContext.Request.Query.TryGetValue(QUERY_KEY_PAGE, out StringValues values))
            {
                if (!StringValues.IsNullOrEmpty(values))
                {
                    if (!int.TryParse(values[0], out result) || result <= 0 || result > MAX_PAGE)
                    {
                        result = 0;
                        return false;
                    }
                }
            }

            return true;
        }

        private bool TryGetQueryValueOfLimit(HttpRequest request, out int result)
        {
            result = 10;

            if (request.HttpContext.Request.Query.TryGetValue(QUERY_KEY_LIMIT, out StringValues values))
            {
                if (!StringValues.IsNullOrEmpty(values))
                {
                    if (!int.TryParse(values[0], out result) || result <= 0 || result > MAX_LIMIT)
                    {
                        result = 0;
                        return false;
                    }
                }
            }

            return true;
        }

        private bool TryGetQueryValueOfOrderBy(HttpRequest request, out IReadOnlyList<PagingSortField> result)
        {
            var list = new List<PagingSortField>();
            result = null;

            PagingSortField? CreateSortField(string exp)
            {
                string[] arr = exp.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length == 1)
                {
                    string sortMode = arr[0];
                    if (ORDER_BY_DESC.Equals(sortMode))
                        return PagingSortField.DefaultDesc;
                    else if (ORDER_BY_ASC.Equals(sortMode))
                        return PagingSortField.DefaultAsc;
                }
                else if (arr.Length == 2)
                {
                    string fieldName = arr[0];
                    string sortMode = arr[1];
                    if (ORDER_BY_DESC.Equals(sortMode))
                        return new PagingSortField(fieldName, PagingSortMode.Desc);
                    else if (ORDER_BY_ASC.Equals(sortMode))
                        return new PagingSortField(fieldName, PagingSortMode.Asc);
                }

                return null;
            }

            if (request.HttpContext.Request.Query.TryGetValue(QUERY_KEY_ORDERBY, out StringValues values))
            {
                /*
                 * Supported format samples:
                 *      1 field: ?order_by=id%20asc
                 *      2 fields: ?order_by=id%20asc&order_by=name%20desc
                 *      2 fields: ?order_by=id%20asc%2Cname%20desc
                 * (%20 is a space char, %2C is a comma char)
                 */

                if (!StringValues.IsNullOrEmpty(values))
                {
                    foreach (string value in values)
                    {
                        string[] exps = value.Split(',', StringSplitOptions.RemoveEmptyEntries);
                        foreach (string exp in exps)
                        {
                            PagingSortField? sortField = CreateSortField(exp);
                            if (!sortField.HasValue)
                            {
                                list.Clear();
                                return false;
                            }

                            list.Add(sortField.Value);
                        }
                    }
                }
            }

            result = list.AsReadOnly();
            return true;
        }

        private bool TryGetQueryValueOfRequireCount(HttpRequest request, out bool result)
        {
            result = false;

            if (request.HttpContext.Request.Query.TryGetValue(QUERY_KEY_REQUIRECOUNT, out StringValues values))
            {
                if (!StringValues.IsNullOrEmpty(values))
                {
                    result = "true".Equals(values[0], StringComparison.CurrentCultureIgnoreCase) ||
                        "yes".Equals(values[0], StringComparison.CurrentCultureIgnoreCase) || 
                        (int.TryParse(values[0], out int i) && i == 1);
                }
            }

            return true;
        }
    }
}
