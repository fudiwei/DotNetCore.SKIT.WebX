using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace STEP.WebX.RESTful
{
    /// <summary>
    /// 
    /// </summary>
    public static class MvcOptionsRouteExtensions
    {
        class RoutePrefixConvention : IApplicationModelConvention
        {
            private readonly AttributeRouteModel _routePrefix;

            public RoutePrefixConvention(IRouteTemplateProvider routeTemplateProvider)
            {
                _routePrefix = new AttributeRouteModel(routeTemplateProvider);
            }

            public void Apply(ApplicationModel application)
            {
                // 遍历所有的 Controller
                foreach (var controller in application.Controllers)
                {
                    // 已经标记了 RouteAttribute 的 Controller
                    var matchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
                    if (matchedSelectors.Any())
                    {
                        foreach (var selectorModel in matchedSelectors)
                        {
                            // 在当前路由上再添加一个路由前缀
                            selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_routePrefix,
                                selectorModel.AttributeRouteModel);
                        }
                    }

                    // 没有标记 RouteAttribute 的 Controller
                    var unmatchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel == null).ToList();
                    if (unmatchedSelectors.Any())
                    {
                        foreach (var selectorModel in unmatchedSelectors)
                        {
                            // 添加一个路由前缀
                            selectorModel.AttributeRouteModel = _routePrefix;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Annotates all controllers with a route prefix that applies to all actions within the controller.
        /// </summary>
        /// <param name="opts"></param>
        /// <param name="routeTemplateProvider"></param>
        public static MvcOptions UseCentralRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeTemplateProvider)
        {
            opts.Conventions.Insert(0, new RoutePrefixConvention(routeTemplateProvider));
            return opts;
        }

        /// <summary>
        /// Annotates all controllers with a route prefix that applies to all actions within the controller.
        /// </summary>
        /// <param name="opts"></param>
        /// <param name="routeTemplate"></param>
        public static MvcOptions UseCentralRoutePrefix(this MvcOptions opts, string routeTemplate)
        {
            return UseCentralRoutePrefix(opts, new RouteAttribute(routeTemplate));
        }
    }
}
