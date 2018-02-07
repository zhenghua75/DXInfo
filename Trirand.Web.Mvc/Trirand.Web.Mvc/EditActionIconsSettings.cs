namespace Trirand.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;

    public class EditActionIconsSettings
    {
        [CompilerGenerated]
        private bool _SaveOnEnterKeyPress_k__BackingField;
        [CompilerGenerated]
        private bool _ShowDeleteIcon_k__BackingField;
        [CompilerGenerated]
        private bool _ShowEditIcon_k__BackingField;

        public EditActionIconsSettings()
        {
            this.ShowEditIcon = true;
            this.ShowDeleteIcon = true;
            this.SaveOnEnterKeyPress = false;
        }

        public bool SaveOnEnterKeyPress
        {
            [CompilerGenerated]
            get
            {
                return this._SaveOnEnterKeyPress_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._SaveOnEnterKeyPress_k__BackingField = value;
            }
        }

        public bool ShowDeleteIcon
        {
            [CompilerGenerated]
            get
            {
                return this._ShowDeleteIcon_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowDeleteIcon_k__BackingField = value;
            }
        }

        public bool ShowEditIcon
        {
            [CompilerGenerated]
            get
            {
                return this._ShowEditIcon_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowEditIcon_k__BackingField = value;
            }
        }
    }
}

