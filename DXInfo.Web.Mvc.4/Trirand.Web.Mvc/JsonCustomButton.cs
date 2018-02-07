using System;
using System.Collections;
using System.Text;
using System.Web.Script.Serialization;
namespace Trirand.Web.Mvc
{
	internal class JsonCustomButton
	{
		private Hashtable _jsonValues;
		private JQGridToolBarButton _button;
		private JavaScriptSerializer _jsonSerializer;
		public JsonCustomButton(JQGridToolBarButton button)
		{
			this._jsonSerializer = new JavaScriptSerializer();
			this._jsonValues = new Hashtable();
			this._button = button;
		}
		public string Process()
		{
			string value = string.IsNullOrEmpty(this._button.Text) ? " " : this._button.Text;
			if (!string.IsNullOrEmpty(this._button.Text))
			{
				this._jsonValues["caption"] = value;
			}
			if (!string.IsNullOrEmpty(this._button.ButtonIcon))
			{
				this._jsonValues["buttonicon"] = this._button.ButtonIcon;
			}
			this._jsonValues["position"] = this._button.Position.ToString().ToLower();
			if (!string.IsNullOrEmpty(this._button.ToolTip))
			{
				this._jsonValues["title"] = this._button.ToolTip;
			}
			string text = this._jsonSerializer.Serialize(this._jsonValues);
			StringBuilder stringBuilder = new StringBuilder();
			this.RenderClientSideEvent(text, stringBuilder, "onClickButton", this._button.OnClick);
			return text.Insert(text.Length - 1, stringBuilder.ToString());
		}
		private void RenderClientSideEvent(string json, StringBuilder sb, string jsName, string eventName)
		{
			if (!string.IsNullOrEmpty(eventName))
			{
				sb.AppendFormat(",{0}:function() {{ {1}(); }}", jsName, eventName);
			}
		}
	}
}
