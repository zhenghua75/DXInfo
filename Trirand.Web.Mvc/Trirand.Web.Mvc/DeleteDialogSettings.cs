namespace Trirand.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;

    public class DeleteDialogSettings
    {
        [CompilerGenerated]
        private string _CancelText_k__BackingField;
        [CompilerGenerated]
        private string _Caption_k__BackingField;
        [CompilerGenerated]
        private string _DeleteMessage_k__BackingField;
        [CompilerGenerated]
        private bool _Draggable_k__BackingField;
        [CompilerGenerated]
        private int _Height_k__BackingField;
        [CompilerGenerated]
        private int _LeftOffset_k__BackingField;
        [CompilerGenerated]
        private string _LoadingMessageText_k__BackingField;
        [CompilerGenerated]
        private bool _Modal_k__BackingField;
        [CompilerGenerated]
        private bool _ReloadAfterSubmit_k__BackingField;
        [CompilerGenerated]
        private bool _Resizable_k__BackingField;
        [CompilerGenerated]
        private string _SubmitText_k__BackingField;
        [CompilerGenerated]
        private int _TopOffset_k__BackingField;
        [CompilerGenerated]
        private int _Width_k__BackingField;

        public DeleteDialogSettings()
        {
            bool flag;
            string str;
            string str2;
            this.TopOffset = this.LeftOffset = 0;
            this.Width = this.Height = 300;
            this.Modal = false;
            this.ReloadAfterSubmit = flag = true;
            this.Resizable = this.Draggable = flag;
            this.LoadingMessageText = str = "";
            this.CancelText = str2 = str;
            this.Caption = this.SubmitText = str2;
        }

        public string CancelText
        {
            [CompilerGenerated]
            get
            {
                return this._CancelText_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._CancelText_k__BackingField = value;
            }
        }

        public string Caption
        {
            [CompilerGenerated]
            get
            {
                return this._Caption_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Caption_k__BackingField = value;
            }
        }

        public string DeleteMessage
        {
            [CompilerGenerated]
            get
            {
                return this._DeleteMessage_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._DeleteMessage_k__BackingField = value;
            }
        }

        public bool Draggable
        {
            [CompilerGenerated]
            get
            {
                return this._Draggable_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Draggable_k__BackingField = value;
            }
        }

        public int Height
        {
            [CompilerGenerated]
            get
            {
                return this._Height_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Height_k__BackingField = value;
            }
        }

        public int LeftOffset
        {
            [CompilerGenerated]
            get
            {
                return this._LeftOffset_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._LeftOffset_k__BackingField = value;
            }
        }

        public string LoadingMessageText
        {
            [CompilerGenerated]
            get
            {
                return this._LoadingMessageText_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._LoadingMessageText_k__BackingField = value;
            }
        }

        public bool Modal
        {
            [CompilerGenerated]
            get
            {
                return this._Modal_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Modal_k__BackingField = value;
            }
        }

        public bool ReloadAfterSubmit
        {
            [CompilerGenerated]
            get
            {
                return this._ReloadAfterSubmit_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ReloadAfterSubmit_k__BackingField = value;
            }
        }

        public bool Resizable
        {
            [CompilerGenerated]
            get
            {
                return this._Resizable_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Resizable_k__BackingField = value;
            }
        }

        public string SubmitText
        {
            [CompilerGenerated]
            get
            {
                return this._SubmitText_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SubmitText_k__BackingField = value;
            }
        }

        public int TopOffset
        {
            [CompilerGenerated]
            get
            {
                return this._TopOffset_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._TopOffset_k__BackingField = value;
            }
        }

        public int Width
        {
            [CompilerGenerated]
            get
            {
                return this._Width_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Width_k__BackingField = value;
            }
        }
    }
}

