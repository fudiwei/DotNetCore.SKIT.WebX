using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace STEP.WebX.RESTful.Paging
{
    /// <summary>
    /// Represents the query information in paging.
    /// </summary>
    [ModelBinder(BinderType = typeof(PagingQueryModelBinder))]
    public struct PagingQueryModel
    {
        /// <summary>
        /// Gets the offset of paging.
        /// </summary>
        public string Offset { get; }

        /// <summary>
        /// Gets the page number of paging.
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// Gets the data count per page of paging.
        /// </summary>
        public int Limit { get; }

        /// <summary>
        /// Gets the sorting fields.
        /// </summary>
        public IReadOnlyList<PagingSortField> OrderByFields { get; }

        /// <summary>
        /// Gets a value indicating whether count the total number of data.
        /// </summary>
        public bool RequireCount { get; }

        internal PagingQueryModel(string offset, int page, int limit, IReadOnlyList<PagingSortField> orderbyFields, bool requireCount)
        {
            Offset = !string.IsNullOrEmpty(offset) ? offset : null;
            Page = page;
            Limit = limit;
            OrderByFields = orderbyFields;
            RequireCount = requireCount;
        }
    }
}