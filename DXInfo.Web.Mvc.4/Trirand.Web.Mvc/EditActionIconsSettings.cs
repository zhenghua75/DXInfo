using System;
namespace Trirand.Web.Mvc
{
	public class EditActionIconsSettings
	{
		public bool ShowEditIcon
		{
			get;
			set;
		}
		public bool ShowDeleteIcon
		{
			get;
			set;
		}
		public bool SaveOnEnterKeyPress
		{
			get;
			set;
		}
        public string DelErrorTextFormat { get; set; }
        public string onEdit { get; set; }
		public EditActionIconsSettings()
		{
			this.ShowEditIcon = true;
			this.ShowDeleteIcon = true;
			this.SaveOnEnterKeyPress = false;
            this.DelErrorTextFormat = "errorTextFormat";
            this.onEdit = "";
		}
	}
}
