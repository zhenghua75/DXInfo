namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class JQDatePicker
    {
        [CompilerGenerated]
        private string _AltField_k__BackingField;
        [CompilerGenerated]
        private string _AltFormat_k__BackingField;
        [CompilerGenerated]
        private string _AppendText_k__BackingField;
        [CompilerGenerated]
        private bool _AutoSize_k__BackingField;
        [CompilerGenerated]
        private string _ButtonImage_k__BackingField;
        [CompilerGenerated]
        private bool _ButtonImageOnly_k__BackingField;
        [CompilerGenerated]
        private string _ButtonText_k__BackingField;
        [CompilerGenerated]
        private bool _ChangeMonth_k__BackingField;
        [CompilerGenerated]
        private bool _ChangeYear_k__BackingField;
        [CompilerGenerated]
        private string _CloseText_k__BackingField;
        [CompilerGenerated]
        private bool _ConstrainInput_k__BackingField;
        [CompilerGenerated]
        private string _CurrentText_k__BackingField;
        [CompilerGenerated]
        private string _DateFormat_k__BackingField;
        [CompilerGenerated]
        private List<string> DayNames_k__BackingField;
        [CompilerGenerated]
        private List<string> DayNamesMin_k__BackingField;
        [CompilerGenerated]
        private List<string> DayNamesShort_k__BackingField;
        [CompilerGenerated]
        private DateTime? _DefaultDate_k__BackingField;
        [CompilerGenerated]
        private DatePickerDisplayMode _DisplayMode_k__BackingField;
        [CompilerGenerated]
        private int _Duration_k__BackingField;
        [CompilerGenerated]
        private bool _Enabled_k__BackingField;
        [CompilerGenerated]
        private int _FirstDay_k__BackingField;
        [CompilerGenerated]
        private bool _GotoCurrent_k__BackingField;
        [CompilerGenerated]
        private bool _HideIfNoPrevNext_k__BackingField;
        [CompilerGenerated]
        private string _ID_k__BackingField;
        [CompilerGenerated]
        private bool _IsRTL_k__BackingField;
        [CompilerGenerated]
        private DateTime? _MaxDate_k__BackingField;
        [CompilerGenerated]
        private DateTime? _MinDate_k__BackingField;
        [CompilerGenerated]
        private List<string> MonthNames_k__BackingField;
        [CompilerGenerated]
        private List<string> MonthNamesShort_k__BackingField;
        [CompilerGenerated]
        private bool _NavigationAsDateFormat_k__BackingField;
        [CompilerGenerated]
        private string _NextText_k__BackingField;
        [CompilerGenerated]
        private string _PrevText_k__BackingField;
        [CompilerGenerated]
        private bool _ShowButtonPanel_k__BackingField;
        [CompilerGenerated]
        private bool _ShowMonthAfterYear_k__BackingField;
        [CompilerGenerated]
        private Trirand.Web.Mvc.ShowOn _ShowOn_k__BackingField;

        public JQDatePicker()
        {
            this.Enabled = true;
            this.AltField = "";
            this.AltFormat = "";
            this.AppendText = "";
            this.AutoSize = false;
            this.ButtonImage = "";
            this.ButtonImageOnly = false;
            this.ButtonText = "...";
            this.ChangeMonth = false;
            this.ChangeYear = false;
            this.CloseText = "Done";
            this.ConstrainInput = true;
            this.CurrentText = "Today";
            this.DateFormat = "yyyy/MM/dd";
            this.DayNames = new List<string>();
            this.DayNamesMin = new List<string>();
            this.DayNamesShort = new List<string>();
            this.DisplayMode = DatePickerDisplayMode.Standalone;
            this.DefaultDate = null;
            this.Duration = 500;
            this.FirstDay = 0;
            this.GotoCurrent = false;
            this.HideIfNoPrevNext = false;
            this.IsRTL = false;
            this.MaxDate = null;
            this.MinDate = null;
            this.MonthNames = new List<string>();
            this.MonthNamesShort = new List<string>();
            this.NavigationAsDateFormat = false;
            this.NextText = "Next";
            this.PrevText = "Prev";
            this.ShowButtonPanel = false;
            this.ShowMonthAfterYear = false;
            this.ShowOn = Trirand.Web.Mvc.ShowOn.Both;
        }

        public string AltField
        {
            [CompilerGenerated]
            get
            {
                return this._AltField_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._AltField_k__BackingField = value;
            }
        }

        public string AltFormat
        {
            [CompilerGenerated]
            get
            {
                return this._AltFormat_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._AltFormat_k__BackingField = value;
            }
        }

        public string AppendText
        {
            [CompilerGenerated]
            get
            {
                return this._AppendText_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._AppendText_k__BackingField = value;
            }
        }

        public bool AutoSize
        {
            [CompilerGenerated]
            get
            {
                return this._AutoSize_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._AutoSize_k__BackingField = value;
            }
        }

        public string ButtonImage
        {
            [CompilerGenerated]
            get
            {
                return this._ButtonImage_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ButtonImage_k__BackingField = value;
            }
        }

        public bool ButtonImageOnly
        {
            [CompilerGenerated]
            get
            {
                return this._ButtonImageOnly_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ButtonImageOnly_k__BackingField = value;
            }
        }

        public string ButtonText
        {
            [CompilerGenerated]
            get
            {
                return this._ButtonText_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ButtonText_k__BackingField = value;
            }
        }

        public bool ChangeMonth
        {
            [CompilerGenerated]
            get
            {
                return this._ChangeMonth_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ChangeMonth_k__BackingField = value;
            }
        }

        public bool ChangeYear
        {
            [CompilerGenerated]
            get
            {
                return this._ChangeYear_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ChangeYear_k__BackingField = value;
            }
        }

        public string CloseText
        {
            [CompilerGenerated]
            get
            {
                return this._CloseText_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._CloseText_k__BackingField = value;
            }
        }

        public bool ConstrainInput
        {
            [CompilerGenerated]
            get
            {
                return this._ConstrainInput_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ConstrainInput_k__BackingField = value;
            }
        }

        public string CurrentText
        {
            [CompilerGenerated]
            get
            {
                return this._CurrentText_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._CurrentText_k__BackingField = value;
            }
        }

        public string DateFormat
        {
            [CompilerGenerated]
            get
            {
                return this._DateFormat_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._DateFormat_k__BackingField = value;
            }
        }

        public List<string> DayNames
        {
            [CompilerGenerated]
            get
            {
                return this.DayNames_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.DayNames_k__BackingField = value;
            }
        }

        public List<string> DayNamesMin
        {
            [CompilerGenerated]
            get
            {
                return this.DayNamesMin_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.DayNamesMin_k__BackingField = value;
            }
        }

        public List<string> DayNamesShort
        {
            [CompilerGenerated]
            get
            {
                return this.DayNamesShort_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.DayNamesShort_k__BackingField = value;
            }
        }

        public DateTime? DefaultDate
        {
            [CompilerGenerated]
            get
            {
                return this._DefaultDate_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._DefaultDate_k__BackingField = value;
            }
        }

        public DatePickerDisplayMode DisplayMode
        {
            [CompilerGenerated]
            get
            {
                return this._DisplayMode_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._DisplayMode_k__BackingField = value;
            }
        }

        public int Duration
        {
            [CompilerGenerated]
            get
            {
                return this._Duration_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._Duration_k__BackingField = value;
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

        public int FirstDay
        {
            [CompilerGenerated]
            get
            {
                return this._FirstDay_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._FirstDay_k__BackingField = value;
            }
        }

        public bool GotoCurrent
        {
            [CompilerGenerated]
            get
            {
                return this._GotoCurrent_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._GotoCurrent_k__BackingField = value;
            }
        }

        public bool HideIfNoPrevNext
        {
            [CompilerGenerated]
            get
            {
                return this._HideIfNoPrevNext_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._HideIfNoPrevNext_k__BackingField = value;
            }
        }

        public string ID
        {
            [CompilerGenerated]
            get
            {
                return this._ID_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ID_k__BackingField = value;
            }
        }

        public bool IsRTL
        {
            [CompilerGenerated]
            get
            {
                return this._IsRTL_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._IsRTL_k__BackingField = value;
            }
        }

        public DateTime? MaxDate
        {
            [CompilerGenerated]
            get
            {
                return this._MaxDate_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._MaxDate_k__BackingField = value;
            }
        }

        public DateTime? MinDate
        {
            [CompilerGenerated]
            get
            {
                return this._MinDate_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._MinDate_k__BackingField = value;
            }
        }

        public List<string> MonthNames
        {
            [CompilerGenerated]
            get
            {
                return this.MonthNames_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.MonthNames_k__BackingField = value;
            }
        }

        public List<string> MonthNamesShort
        {
            [CompilerGenerated]
            get
            {
                return this.MonthNamesShort_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this.MonthNamesShort_k__BackingField = value;
            }
        }

        public bool NavigationAsDateFormat
        {
            [CompilerGenerated]
            get
            {
                return this._NavigationAsDateFormat_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._NavigationAsDateFormat_k__BackingField = value;
            }
        }

        public string NextText
        {
            [CompilerGenerated]
            get
            {
                return this._NextText_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._NextText_k__BackingField = value;
            }
        }

        public string PrevText
        {
            [CompilerGenerated]
            get
            {
                return this._PrevText_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._PrevText_k__BackingField = value;
            }
        }

        public bool ShowButtonPanel
        {
            [CompilerGenerated]
            get
            {
                return this._ShowButtonPanel_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowButtonPanel_k__BackingField = value;
            }
        }

        public bool ShowMonthAfterYear
        {
            [CompilerGenerated]
            get
            {
                return this._ShowMonthAfterYear_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowMonthAfterYear_k__BackingField = value;
            }
        }

        public Trirand.Web.Mvc.ShowOn ShowOn
        {
            [CompilerGenerated]
            get
            {
                return this._ShowOn_k__BackingField;
            }
            [CompilerGenerated]
            set
            {
                this._ShowOn_k__BackingField = value;
            }
        }
    }
}

