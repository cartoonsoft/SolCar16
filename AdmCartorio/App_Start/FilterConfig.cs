﻿#region Using

using System.Web.Mvc;

#endregion

namespace AdmCartorio
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}