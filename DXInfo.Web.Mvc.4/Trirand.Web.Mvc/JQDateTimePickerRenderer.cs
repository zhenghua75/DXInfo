using System;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
    internal class JQDateTimePickerRenderer
    {
        private string _dateFormat = "";
        private string _timeFormat="";
        private JQDateTimePicker _model;
        public JQDateTimePickerRenderer(JQDateTimePicker dateTimePicker)
        {
            this._model = dateTimePicker;
            this._model.DateFormat = dateTimePicker.DateFormat;
            this._model.TimeFormat = dateTimePicker.TimeFormat;
            this._dateFormat = "{0:" + dateTimePicker.DateFormat + "}";
            this._timeFormat = "{0:" + dateTimePicker.TimeFormat + "}";
        }
        public string RenderHtml()
        {
            Guard.IsNotNullOrEmpty(this._model.ID, "ID", "You need to set ID for this JQDatePicker instance.");
            return this.GetControlEditorJavascript();
        }        
        private string GetControlEditorJavascript()
        {
            return string.Format("<script type='text/javascript'>var {0}_dtpid = {{ {1} }};</script>", this._model.ID, this.GetStartupOptions());
        }
        private string GetStartupOptions()
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("id: '{0}'", this._model.ID);
            stringBuilder.AppendFormat(",dateFormat:'{0}'", this._model.DateFormat);
            stringBuilder.AppendFormat(",timeFormat:'{0}'", this._model.TimeFormat);
           
            return stringBuilder.ToString();
        }
    }
    internal class JQTimePickerRenderer
    {
        private string _timeFormat = "";
        private JQTimePicker _model;
        public JQTimePickerRenderer(JQTimePicker timePicker)
        {
            this._model = timePicker;
            this._model.TimeFormat = timePicker.TimeFormat;
            this._timeFormat = "{0:" + timePicker.TimeFormat + "}";
        }
        public string RenderHtml()
        {
            Guard.IsNotNullOrEmpty(this._model.ID, "ID", "You need to set ID for this JQDateTimePicker instance.");
            return this.GetControlEditorJavascript();
        }
        private string GetControlEditorJavascript()
        {
            return string.Format("<script type='text/javascript'>var {0}_tpid = {{ {1} }};</script>", this._model.ID, this.GetStartupOptions());
        }
        private string GetStartupOptions()
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("id: '{0}'", this._model.ID);
            stringBuilder.AppendFormat(",timeFormat:'{0}'", this._model.TimeFormat);
            return stringBuilder.ToString();
        }
    }
}
