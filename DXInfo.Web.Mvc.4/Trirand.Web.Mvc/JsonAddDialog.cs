using System;
using System.Collections;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	internal class JsonAddDialog
	{
		private Hashtable _jsonValues;
		private JQGrid _grid;
		public JsonAddDialog(JQGrid grid)
		{
			this._jsonValues = new Hashtable();
			this._grid = grid;
		}
        public string Process()
        {
            AddDialogSettings addDialogSettings = this._grid.AddDialogSettings;
            if (addDialogSettings.TopOffset != 0)
            {
                this._jsonValues["top"] = addDialogSettings.TopOffset;
            }
            if (addDialogSettings.LeftOffset != 0)
            {
                this._jsonValues["left"] = addDialogSettings.LeftOffset;
            }
            if (addDialogSettings.Width != 300)
            {
                this._jsonValues["width"] = addDialogSettings.Width;
            }
            if (addDialogSettings.Height != 300)
            {
                this._jsonValues["height"] = addDialogSettings.Height;
            }
            if (addDialogSettings.DataHeight != 0)
            {
                this._jsonValues["dataheight"] = addDialogSettings.DataHeight;
            }
            if (addDialogSettings.Modal)
            {
                this._jsonValues["modal"] = true;
            }
            if (!addDialogSettings.Draggable)
            {
                this._jsonValues["drag"] = false;
            }
            if (!string.IsNullOrEmpty(addDialogSettings.Caption))
            {
                this._jsonValues["addCaption"] = addDialogSettings.Caption;
            }
            if (!string.IsNullOrEmpty(addDialogSettings.SubmitText))
            {
                this._jsonValues["bSubmit"] = addDialogSettings.SubmitText;
            }
            if (!string.IsNullOrEmpty(addDialogSettings.CancelText))
            {
                this._jsonValues["bCancel"] = addDialogSettings.CancelText;
            }
            if (!string.IsNullOrEmpty(addDialogSettings.LoadingMessageText))
            {
                this._jsonValues["processData"] = addDialogSettings.LoadingMessageText;
            }
            if (addDialogSettings.CloseAfterAdding)
            {
                this._jsonValues["closeAfterAdd"] = addDialogSettings.CloseAfterAdding;
            }
            if (!addDialogSettings.ClearAfterAdding)
            {
                this._jsonValues["clearAfterAdding"] = false;
            }
            if (!addDialogSettings.ClearAfterAdd)
            {
                this._jsonValues["clearAfterAdd"] = false;
            }
            if (!addDialogSettings.ReloadAfterSubmit)
            {
                this._jsonValues["reloadAfterSubmit"] = false;
            }
            if (!addDialogSettings.Resizable)
            {
                this._jsonValues["resize"] = false;
            }
            //if (!string.IsNullOrEmpty(addDialogSettings.BeforeSubmit))
            //{
            //    this._jsonValues["beforeSubmit"] = addDialogSettings.BeforeSubmit;
            //}
            if (addDialogSettings.CloseOnEscape)
            {
                this._jsonValues["closeOnEscape"] = true;
            }
            if (!string.IsNullOrEmpty(addDialogSettings.BottomInfo))
            {
                this._jsonValues["bottominfo"] = addDialogSettings.BottomInfo;
            }
            this._jsonValues["recreateForm"] = true;
            string json = new JavaScriptSerializer().Serialize(this._jsonValues);
            ClientSideEvents clientSideEvents = this._grid.ClientSideEvents;
            json = JsonUtil.RenderClientSideEvent(json, "beforeShowForm", clientSideEvents.BeforeAddDialogShown);
            json = JsonUtil.RenderClientSideEvent(json, "afterShowForm", clientSideEvents.AfterAddDialogShown);
            json = JsonUtil.RenderClientSideEvent(json, "afterComplete", clientSideEvents.AfterAddDialogRowInserted);
            json = JsonUtil.RenderClientSideEvent(json, "beforeSubmit", clientSideEvents.BeforeAddDialogSubmit);
            json = JsonUtil.RenderClientSideEvent(json, "beforeInitData", clientSideEvents.BeforeAddDialogInitData);
            json = JsonUtil.RenderClientSideEvent(json, "onclickSubmit", clientSideEvents.AddDialogOnClickSubmit);
            json = JsonUtil.RenderClientSideEvent(json, "onInitializeForm", clientSideEvents.AddDialogOnInitializeForm);
            json = JsonUtil.RenderClientSideEvent(json, "serializeEditData", clientSideEvents.AddDialogSerializeEditData);
            json = JsonUtil.RenderClientSideEvent(json, "beforeCheckValues", clientSideEvents.AddDialogBeforeCheckValues);
            json = JsonUtil.RenderClientSideEvent(json, "errorTextFormat", "function(data) { return '´íÎó: ' + data.responseText }");
            return JsonUtil.RenderClientSideEvent(json, "editData", string.Format("{{ __RequestVerificationToken: jQuery('input[name=__RequestVerificationToken]').val() }}", this._grid.ID));
        }
	}
}
