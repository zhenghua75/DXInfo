namespace Trirand.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;

    public class AppearanceSettings
    {
        [CompilerGenerated]
        private bool _AlternateRowBackground_k__BackingField;
        [CompilerGenerated]
        private string _Caption_k__BackingField;
        [CompilerGenerated]
        private bool _HighlightRowsOnHover_k__BackingField;
        [CompilerGenerated]
        private bool _RightToLeft_k__BackingField;
        [CompilerGenerated]
        private int _RowNumbersColumnWidth_k__BackingField;
        [CompilerGenerated]
        private int _ScrollBarOffset_k__BackingField;
        [CompilerGenerated]
        private bool _ShowFooter_k__BackingField;
        [CompilerGenerated]
        private bool _ShowRowNumbers_k__BackingField;
        [CompilerGenerated]
        private bool _ShrinkToFit_k__BackingField;

        public AppearanceSettings()
        {
            bool flag;
            bool flag2;
            bool flag3;
            this.ShowFooter = flag = false;
            this.RightToLeft = flag2 = flag;
            this.HighlightRowsOnHover = flag3 = flag2;
            this.ShowRowNumbers = this.AlternateRowBackground = flag3;
            this.RowNumbersColumnWidth = 0x19;
            this.Caption = "";
            this.ScrollBarOffset = 0x12;
            this.ShrinkToFit = true;
        }

        public bool AlternateRowBackground
        {
            [CompilerGenerated]
            get
            {
                return this._AlternateRowBackground_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._AlternateRowBackground_k__BackingField = value;
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

        public bool HighlightRowsOnHover
        {
            [CompilerGenerated]
            get
            {
                return this._HighlightRowsOnHover_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._HighlightRowsOnHover_k__BackingField = value;
            }
        }

        public bool RightToLeft
        {
            [CompilerGenerated]
            get
            {
                return this._RightToLeft_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._RightToLeft_k__BackingField = value;
            }
        }

        public int RowNumbersColumnWidth
        {
            [CompilerGenerated]
            get
            {
                return this._RowNumbersColumnWidth_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._RowNumbersColumnWidth_k__BackingField = value;
            }
        }

        public int ScrollBarOffset
        {
            [CompilerGenerated]
            get
            {
                return this._ScrollBarOffset_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ScrollBarOffset_k__BackingField = value;
            }
        }

        public bool ShowFooter
        {
            [CompilerGenerated]
            get
            {
                return this._ShowFooter_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowFooter_k__BackingField = value;
            }
        }

        public bool ShowRowNumbers
        {
            [CompilerGenerated]
            get
            {
                return this._ShowRowNumbers_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowRowNumbers_k__BackingField = value;
            }
        }

        public bool ShrinkToFit
        {
            [CompilerGenerated]
            get
            {
                return this._ShrinkToFit_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShrinkToFit_k__BackingField = value;
            }
        }
    }
}

