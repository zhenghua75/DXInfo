namespace Trirand.Web.Mvc
{
    using System;
    using System.Text;
    using System.Web.Script.Serialization;
    
    internal class JQAutoCompleteRenderer
    {
        private JQAutoComplete _model;

        public JQAutoCompleteRenderer(JQAutoComplete autoComplete)
        {
            this._model = autoComplete;
        }

        private string GetControlEditorJavascript()
        {
            return string.Format("<script type='text/javascript'>var {0}_acid = {{ {1} }};</script>", this._model.ID, this.GetStartupOptions());
        }

        private string GetStandaloneJavascript()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<input type='text' id='{0}' name='{0}' />", this._model.ID);
            builder.Append("<script type='text/javascript'>\n");
            builder.Append("jQuery(document).ready(function() {");
            builder.AppendFormat("jQuery('#{0}').autocomplete({{", this._model.ID);
            builder.Append(this.GetStartupOptions());
            builder.Append("});");
            builder.Append("});");
            builder.Append("</script>");
            return builder.ToString();
        }

        private string GetStartupOptions()
        {
            new JavaScriptSerializer();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("id: '{0}'", this._model.ID);
            sb.AppendFormat(",source: '{0}'", this._model.DataUrl);
            sb.AppendFormatIfTrue(this._model.Delay != 300, ",delay: {0}", new object[] { this._model.Delay });
            sb.AppendIfFalse(this._model.Enabled, ",disabled: true");
            sb.AppendFormatIfTrue(this._model.MinLength != 1, ",minLength: {0}", new object[] { this._model.MinLength });
            return sb.ToString();
        }

        public string RenderHtml()
        {
            if (DateTime.Now > CompiledOn.CompilationDate.AddDays(45.0))
            {
                return "This is a trial version of jqSuite for ASP.NET MVC which has expired.<br> Please, contact sales@trirand.net for purchasing the product or for trial extension.";
            }
            Guard.IsNotNullOrEmpty(this._model.ID, "ID", "You need to set ID for this JQAutoComplete instance.");
            if (this._model.DisplayMode == AutoCompleteDisplayMode.Standalone)
            {
                return this.GetStandaloneJavascript();
            }
            return this.GetControlEditorJavascript();
        }
    }
}

