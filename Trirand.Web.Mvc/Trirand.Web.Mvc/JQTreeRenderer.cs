namespace Trirand.Web.Mvc
{
    using System;
    using System.Text;
    using System.Web.Script.Serialization;

    internal class JQTreeRenderer
    {
        private JQTree _model;

        public JQTreeRenderer(JQTree model)
        {
            this._model = model;
        }

        private string GetStandaloneJavascript()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<ul id='{0}'></ul>", this._model.ID);
            builder.Append("<script type='text/javascript'>\n");
            builder.Append("jQuery(document).ready(function() {");
            builder.AppendFormat("jQuery('#{0}').jqTree({{", this._model.ID);
            builder.Append(this.GetStartupOptions());
            builder.Append("});");
            builder.Append("});");
            builder.Append("</script>");
            return builder.ToString();
        }

        private string GetStartupOptions()
        {
            new JavaScriptSerializer();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("id: '{0}'", this._model.ID);
            builder.AppendFormat("url: '{0}'", this._model.DataUrl);
            return builder.ToString();
        }

        public string RenderHtml()
        {
            //if (DateTime.Now > CompiledOn.CompilationDate.AddDays(45.0))
            //{
            //    return "This is a 30-day trial version of jqSuite for ASP.NET MVC which has expired.<br> Please, contact sales@trirand.net for purchasing the product or for trial extension.";
            //}
            Guard.IsNotNullOrEmpty(this._model.ID, "ID", "You need to set ID for this JQTree instance.");
            Guard.IsNotNullOrEmpty(this._model.DataUrl, "DataUrl", "You need to set DataUrl to the Action of the tree returning nodes.");
            return this.GetStandaloneJavascript();
        }
    }
}

