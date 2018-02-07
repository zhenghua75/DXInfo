namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ChartSeriesSettings
    {
        [CompilerGenerated]
        private IEnumerable<ChartPoint> _Data_k__BackingField;
        [CompilerGenerated]
        private string _Name_k__BackingField;

        public ChartSeriesSettings()
        {
            this.Name = "";
            this.Data = null;
        }

        public IEnumerable<ChartPoint> Data
        {
            [CompilerGenerated]
            get
            {
                return this._Data_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Data_k__BackingField = value;
            }
        }

        public string Name
        {
            [CompilerGenerated]
            get
            {
                return this._Name_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Name_k__BackingField = value;
            }
        }
    }
}

