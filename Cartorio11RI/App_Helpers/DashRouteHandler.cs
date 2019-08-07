﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Cartorio11RI
{
    public class DashRouteHandler : MvcRouteHandler
    {
        /// <summary>
        ///     Manipulador de rota personalizado que remove quaisquer traços da rota antes de manipulá-lo.
        ///     Custom route handler that removes any dashes from the route before handling it.
        /// </summary>
        /// <param name="requestContext">The context of the given (current) request.</param>
        /// <returns></returns>
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            var routeValues = requestContext.RouteData.Values;

            routeValues["action"] = routeValues["action"].UnDash();
            routeValues["controller"] = routeValues["controller"].UnDash();

            return base.GetHttpHandler(requestContext);
        }
    }
}