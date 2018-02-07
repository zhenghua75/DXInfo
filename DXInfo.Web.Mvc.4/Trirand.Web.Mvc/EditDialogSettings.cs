using System;
namespace Trirand.Web.Mvc
{
	public class EditDialogSettings
	{
		public int TopOffset
		{
			get;
			set;
		}
		public int LeftOffset
		{
			get;
			set;
		}
		public int Width
		{
			get;
			set;
		}
		public int Height
		{
			get;
			set;
		}
        public int DataHeight { get; set; }
		public bool Modal
		{
			get;
			set;
		}
		public bool Resizable
		{
			get;
			set;
		}
		public bool Draggable
		{
			get;
			set;
		}
		public string Caption
		{
			get;
			set;
		}
		public string SubmitText
		{
			get;
			set;
		}
		public string CancelText
		{
			get;
			set;
		}
		public string LoadingMessageText
		{
			get;
			set;
		}
		public bool CloseAfterEditing
		{
			get;
			set;
		}
		public bool ReloadAfterSubmit
		{
			get;
			set;
		}
        public bool CloseOnEscape { get; set; }
        public string BottomInfo { get; set; }
		public EditDialogSettings()
		{
			this.TopOffset = (this.LeftOffset = 0);
            this.Width = 300;
            this.Height = 300;
            this.Modal = true;// (this.CloseAfterEditing = false);
            this.CloseAfterEditing = false;
			this.Resizable = (this.Draggable = (this.ReloadAfterSubmit = true));
			this.Caption = (this.SubmitText = (this.CancelText = (this.LoadingMessageText = "")));
            this.CloseOnEscape = true;
            this.DataHeight = 0;
            this.BottomInfo = "*为必输入项";
		}
	}
}
