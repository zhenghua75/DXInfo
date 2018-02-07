namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Text;
    using System.Web.Script.Serialization;

    internal class JsonDelDialog
    {
        private JQGrid _grid;
        private Hashtable _jsonValues = new Hashtable();

        public JsonDelDialog(JQGrid grid)
        {
            this._grid = grid;
        }

        public string Process()
        {
            DeleteDialogSettings deleteDialogSettings = this._grid.DeleteDialogSettings;
            if (deleteDialogSettings.TopOffset != 0)
            {
                this._jsonValues["top"] = deleteDialogSettings.TopOffset;
            }
            if (deleteDialogSettings.LeftOffset != 0)
            {
                this._jsonValues["left"] = deleteDialogSettings.LeftOffset;
            }
            if (deleteDialogSettings.Width != 300)
            {
                this._jsonValues["width"] = deleteDialogSettings.Width;
            }
            if (deleteDialogSettings.Height != 300)
            {
                this._jsonValues["height"] = deleteDialogSettings.Height;
            }
            if (deleteDialogSettings.Modal)
            {
                this._jsonValues["modal"] = true;
            }
            if (!deleteDialogSettings.Draggable)
            {
                this._jsonValues["drag"] = false;
            }
            if (!string.IsNullOrEmpty(deleteDialogSettings.SubmitText))
            {
                this._jsonValues["bSubmit"] = deleteDialogSettings.SubmitText;
            }
            if (!string.IsNullOrEmpty(deleteDialogSettings.CancelText))
            {
                this._jsonValues["bCancel"] = deleteDialogSettings.CancelText;
            }
            if (!string.IsNullOrEmpty(deleteDialogSettings.LoadingMessageText))
            {
                this._jsonValues["processData"] = deleteDialogSettings.LoadingMessageText;
            }
            if (!string.IsNullOrEmpty(deleteDialogSettings.Caption))
            {
                this._jsonValues["caption"] = deleteDialogSettings.Caption;
            }
            if (!string.IsNullOrEmpty(deleteDialogSettings.DeleteMessage))
            {
                this._jsonValues["msg"] = deleteDialogSettings.DeleteMessage;
            }
            if (!deleteDialogSettings.ReloadAfterSubmit)
            {
                this._jsonValues["reloadAfterSubmit"] = false;
            }
            if (!deleteDialogSettings.Resizable)
            {
                this._jsonValues["resize"] = false;
            }
            this._jsonValues["recreateForm"] = true;
            string json = new JavaScriptSerializer().Serialize(this._jsonValues);
            StringBuilder sb = new StringBuilder();
            this.RenderClientSideEvent(json, sb, "delData", string.Format("{{ __RequestVerificationToken: jQuery('input[name=__RequestVerificationToken]').val() }}", this._grid.ID));
            this.RenderClientSideEvent(json, sb, "errorTextFormat", "function(data) { return data.responseText }");
            return json.Insert(json.Length - 1, sb.ToString());
        }

        private void RenderClientSideEvent(string json, StringBuilder sb, string jsName, string eventName)
        {
            string str = (json.Length > 2) ? "," : "";
            if (!string.IsNullOrEmpty(eventName))
            {
                sb.AppendFormat("{0}{1}:{2}", str, jsName, eventName);
            }
        }

        public Hashtable JsonValues
        {
            get
            {
                return this._jsonValues;
            }
            set
            {
                this._jsonValues = value;
            }
        }
    }
}

