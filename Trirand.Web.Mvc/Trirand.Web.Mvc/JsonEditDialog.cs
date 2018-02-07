namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Text;
    using System.Web.Script.Serialization;

    internal class JsonEditDialog
    {
        private JQGrid _grid;
        private JavaScriptSerializer _json = new JavaScriptSerializer();
        private Hashtable _jsonValues = new Hashtable();

        public JsonEditDialog(JQGrid grid)
        {
            this._grid = grid;
        }

        public string Process()
        {
            EditDialogSettings editDialogSettings = this._grid.EditDialogSettings;
            if (editDialogSettings.TopOffset != 0)
            {
                this._jsonValues["top"] = editDialogSettings.TopOffset;
            }
            if (editDialogSettings.LeftOffset != 0)
            {
                this._jsonValues["left"] = editDialogSettings.LeftOffset;
            }
            if (editDialogSettings.Width != 300)
            {
                this._jsonValues["width"] = editDialogSettings.Width;
            }
            if (editDialogSettings.Height != 300)
            {
                this._jsonValues["height"] = editDialogSettings.Height;
            }
            if (editDialogSettings.Modal)
            {
                this._jsonValues["modal"] = true;
            }
            if (!editDialogSettings.Draggable)
            {
                this._jsonValues["drag"] = false;
            }
            if (!string.IsNullOrEmpty(editDialogSettings.Caption))
            {
                this._jsonValues["editCaption"] = editDialogSettings.Caption;
            }
            if (!string.IsNullOrEmpty(editDialogSettings.SubmitText))
            {
                this._jsonValues["bSubmit"] = editDialogSettings.SubmitText;
            }
            if (!string.IsNullOrEmpty(editDialogSettings.CancelText))
            {
                this._jsonValues["bCancel"] = editDialogSettings.CancelText;
            }
            if (!string.IsNullOrEmpty(editDialogSettings.LoadingMessageText))
            {
                this._jsonValues["processData"] = editDialogSettings.LoadingMessageText;
            }
            if (editDialogSettings.CloseAfterEditing)
            {
                this._jsonValues["closeAfterEdit"] = true;
            }
            if (!editDialogSettings.ReloadAfterSubmit)
            {
                this._jsonValues["reloadAfterSubmit"] = false;
            }
            if (!editDialogSettings.Resizable)
            {
                this._jsonValues["resize"] = false;
            }
            this._jsonValues["recreateForm"] = true;
            string json = new JavaScriptSerializer().Serialize(this._jsonValues);
            StringBuilder sb = new StringBuilder();
            this.RenderClientSideEvent(json, sb, "editData", string.Format("{{ __RequestVerificationToken: jQuery('input[name=__RequestVerificationToken]').val() }}", this._grid.ID));
            this.RenderClientSideEvent(json, sb, "afterShowForm", this._grid.ClientSideEvents.AfterEditDialogShown);
            this.RenderClientSideEvent(json, sb, "afterComplete", this._grid.ClientSideEvents.AfterEditDialogRowInserted);
            this.RenderClientSideEvent(json, sb, "errorTextFormat", "function(data) { return data.responseText }");
            return json.Insert(json.Length - 1, sb.ToString());
        }

        private void RenderClientSideEvent(string json, StringBuilder sb, string jsName, string eventName)
        {
            if (!string.IsNullOrEmpty(eventName))
            {
                sb.AppendFormat(",{0}:{1}", jsName, eventName);
            }
        }
    }
}

