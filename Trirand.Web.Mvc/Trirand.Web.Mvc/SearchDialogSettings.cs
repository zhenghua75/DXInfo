namespace Trirand.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;

    public class SearchDialogSettings
    {
        [CompilerGenerated]
        private bool _Draggable_k__BackingField;
        [CompilerGenerated]
        private string _FindButtonText_k__BackingField;
        [CompilerGenerated]
        private int _Height_k__BackingField;
        [CompilerGenerated]
        private int _LeftOffset_k__BackingField;
        [CompilerGenerated]
        private bool _Modal_k__BackingField;
        [CompilerGenerated]
        private bool _MultipleSearch_k__BackingField;
        [CompilerGenerated]
        private string _ResetButtonText_k__BackingField;
        [CompilerGenerated]
        private bool _Resizable_k__BackingField;
        [CompilerGenerated]
        private int _TopOffset_k__BackingField;
        [CompilerGenerated]
        private bool _ValidateInput_k__BackingField;
        [CompilerGenerated]
        private int _Width_k__BackingField;

        public SearchDialogSettings()
        {
            bool flag;
            this.TopOffset = this.LeftOffset = 0;
            this.Width = this.Height = 300;
            this.ValidateInput = flag = false;
            this.Modal = this.MultipleSearch = flag;
            this.Draggable = true;
            this.FindButtonText = this.ResetButtonText = "";
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

        public string FindButtonText
        {
            [CompilerGenerated]
            get
            {
                return this._FindButtonText_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._FindButtonText_k__BackingField = value;
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

        public bool MultipleSearch
        {
            [CompilerGenerated]
            get
            {
                return this._MultipleSearch_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._MultipleSearch_k__BackingField = value;
            }
        }

        public string ResetButtonText
        {
            [CompilerGenerated]
            get
            {
                return this._ResetButtonText_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ResetButtonText_k__BackingField = value;
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

        public bool ValidateInput
        {
            [CompilerGenerated]
            get
            {
                return this._ValidateInput_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ValidateInput_k__BackingField = value;
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

