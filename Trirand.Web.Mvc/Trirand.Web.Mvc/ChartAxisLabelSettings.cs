namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Web.Script.Serialization;

    public class ChartAxisLabelSettings
    {
        [CompilerGenerated]
        private ChartHorizontalAlign _Align_k__BackingField;
        [CompilerGenerated]
        private bool _Enabled_k__BackingField;
        [CompilerGenerated]
        private string _Formatter_k__BackingField;
        [CompilerGenerated]
        private int _Rotation_k__BackingField;
        [CompilerGenerated]
        private int _StaggerLines_k__BackingField;
        [CompilerGenerated]
        private int _Step_k__BackingField;
        [CompilerGenerated]
        private int _X_k__BackingField;
        [CompilerGenerated]
        private int _Y_k__BackingField;

        public ChartAxisLabelSettings()
        {
            this.Align = ChartHorizontalAlign.Center;
            this.Enabled = true;
            this.Formatter = "";
            this.Rotation = 0;
            this.StaggerLines = 0;
            this.Step = 0;
            this.X = 0;
            this.Y = 0;
        }

        internal Hashtable ToHashtable()
        {
            Hashtable hashtable = new Hashtable();
            if (this.Align != ChartHorizontalAlign.Center)
            {
                hashtable.Add("align", this.Align.ToString().ToLower());
            }
            if (!this.Enabled)
            {
                hashtable.Add("enabled", false);
            }
            if (this.Rotation != 0)
            {
                hashtable.Add("rotation", this.Rotation);
            }
            if (this.StaggerLines != 0)
            {
                hashtable.Add("staggerLines", this.StaggerLines);
            }
            if (this.Step != 0)
            {
                hashtable.Add("step", this.Step);
            }
            if (this.X != 0)
            {
                hashtable.Add("x", this.X);
            }
            if (this.Y != 0)
            {
                hashtable.Add("y", this.Y);
            }
            return hashtable;
        }

        internal string ToJSON()
        {
            return new JavaScriptSerializer().Serialize(this.ToHashtable());
        }

        public ChartHorizontalAlign Align
        {
            [CompilerGenerated]
            get
            {
                return this._Align_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Align_k__BackingField = value;
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

        public int Rotation
        {
            [CompilerGenerated]
            get
            {
                return this._Rotation_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Rotation_k__BackingField = value;
            }
        }

        public int StaggerLines
        {
            [CompilerGenerated]
            get
            {
                return this._StaggerLines_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._StaggerLines_k__BackingField = value;
            }
        }

        public int Step
        {
            [CompilerGenerated]
            get
            {
                return this._Step_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Step_k__BackingField = value;
            }
        }

        public int X
        {
            [CompilerGenerated]
            get
            {
                return this._X_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._X_k__BackingField = value;
            }
        }

        public int Y
        {
            [CompilerGenerated]
            get
            {
                return this._Y_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Y_k__BackingField = value;
            }
        }
    }
}

