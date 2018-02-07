using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Trirand.Web.Mvc
{
    public class JQChosen
    {
        public string ID { get; set; }
        public bool AllowSingleDeselect { get; set; }
        public string NoResultsText { get; set; }
        public bool SearchContains { get; set; }
        public string PlaceholderTextMultiple { get; set; }
        public JQChosen()
        {
            this.AllowSingleDeselect = true;
            this.NoResultsText = "没有找到";
            this.SearchContains = true;
            this.PlaceholderTextMultiple = "请选择......";
        }
    }
    internal class JQChosenRenderer
    {
        private JQChosen _model;
        public JQChosenRenderer(JQChosen chosen)
        {
            this._model = chosen;
        }
        public string RenderHtml()
        {
            Guard.IsNotNullOrEmpty(this._model.ID, "ID", "You need to set ID for this JQDateTimePicker instance.");
            return this.GetControlEditorJavascript();
        }
        private string GetControlEditorJavascript()
        {
            return string.Format("<script type='text/javascript'>var {0}_cnid = {{ {1} }};</script>", this._model.ID, this.GetStartupOptions());
        }
        private string GetStartupOptions()
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("id: '{0}'", this._model.ID);
            stringBuilder.AppendFormatIfTrue(this._model.AllowSingleDeselect, ",allow_single_deselect:true");
            stringBuilder.AppendFormatIfNotNullOrEmpty(this._model.NoResultsText,",no_results_text:'{0}'");
            stringBuilder.AppendFormatIfTrue(this._model.SearchContains, ",search_contains:true");
            stringBuilder.AppendFormatIfNotNullOrEmpty(this._model.PlaceholderTextMultiple,",placeholder_text_multiple:'{0}'");
            return stringBuilder.ToString();
        }
    }
}
