using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace DXInfo.Models
{
    [NotMapped]
    public class aspnet_CustomProfileEx : aspnet_CustomProfile
    {
        protected virtual void AfterSelect() { }
        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                OnPropertyChanged("IsSelected");
                this.AfterSelect();
            }
        }
    }
}
