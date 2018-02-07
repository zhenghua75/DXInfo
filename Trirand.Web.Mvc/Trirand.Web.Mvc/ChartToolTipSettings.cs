namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Web.Script.Serialization;

    public class ChartToolTipSettings
    {
        [CompilerGenerated]
        private string _BackgroundColor_k__BackingField;
        [CompilerGenerated]
        private string _BorderColor_k__BackingField;
        [CompilerGenerated]
        private int _BorderRadius_k__BackingField;
        [CompilerGenerated]
        private int _BorderWidth_k__BackingField;
        [CompilerGenerated]
        private bool _Enabled_k__BackingField;
        [CompilerGenerated]
        private string _Formatter_k__BackingField;
        [CompilerGenerated]
        private ChartCrossHairSettings _XAxisCrossHair_k__BackingField;
        [CompilerGenerated]
        private ChartCrossHairSettings _YAxisCrossHair_k__BackingField;

        public ChartToolTipSettings()
        {
            this.BackgroundColor = "rgba(255, 255, 255, .85)";
            this.BorderColor = "auto";
            this.BorderRadius = 5;
            this.BorderWidth = 2;
            this.Formatter = "";
            this.Enabled = true;
            this.XAxisCrossHair = new ChartCrossHairSettings();
            this.YAxisCrossHair = new ChartCrossHairSettings();
        }

        internal string ToJSON()
        {
            Hashtable hashtable = new Hashtable();
            if (this.BackgroundColor != "rgba(255, 255, 255, .85)")
            {
                hashtable.Add("backgroundColor", this.BackgroundColor);
            }
            if (this.BorderColor != "auto")
            {
                hashtable.Add("borderColor", this.BorderColor);
            }
            if (this.BorderRadius != 5)
            {
                hashtable.Add("borderRadius", this.BorderRadius);
            }
            if (this.BorderWidth != 2)
            {
                hashtable.Add("borderWidth", this.BorderWidth);
            }
            ChartCrossHairSettings[] settingsArray = new ChartCrossHairSettings[] { this.XAxisCrossHair, this.YAxisCrossHair };
            hashtable.Add("crosshairs", settingsArray);
            if (!this.Enabled)
            {
                hashtable.Add("enabled", false);
            }
            return new JavaScriptSerializer().Serialize(hashtable);
        }

        public string BackgroundColor
        {
            [CompilerGenerated]
            get
            {
                return this._BackgroundColor_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._BackgroundColor_k__BackingField = value;
            }
        }

        public string BorderColor
        {
            [CompilerGenerated]
            get
            {
                return this._BorderColor_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._BorderColor_k__BackingField = value;
            }
        }

        public int BorderRadius
        {
            [CompilerGenerated]
            get
            {
                return this._BorderRadius_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._BorderRadius_k__BackingField = value;
            }
        }

        public int BorderWidth
        {
            [CompilerGenerated]
            get
            {
                return this._BorderWidth_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._BorderWidth_k__BackingField = value;
            }
        }

        public bool Enabled
        {
            [CompilerGenerated]
            get
            {
                return this._Enabled_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Enabled_k__BackingField = value;
            }
        }

        public string Formatter
        {
            [CompilerGenerated]
            get
            {
                return this._Formatter_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Formatter_k__BackingField = value;
            }
        }

        public ChartCrossHairSettings XAxisCrossHair
        {
            [CompilerGenerated]
            get
            {
                return this._XAxisCrossHair_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._XAxisCrossHair_k__BackingField = value;
            }
        }

        public ChartCrossHairSettings YAxisCrossHair
        {
            [CompilerGenerated]
            get
            {
                return this._YAxisCrossHair_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._YAxisCrossHair_k__BackingField = value;
            }
        }
    }
}

