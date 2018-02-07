namespace Trirand.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public static class HtmlHelperExtensions
    {
        public static TrirandNamespace Trirand(this HtmlHelper helper)
        {
            return new TrirandNamespace();
        }
    }
}

