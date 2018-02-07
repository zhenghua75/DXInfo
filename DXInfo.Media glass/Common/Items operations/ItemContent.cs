#region License

//Media Glass - my simple WPF player
//Copyright (C) 2008-2010 Denis Yakimenko <denyakimenko@yandex.ru>

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.

//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.

//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Controls;
using Media_glass.Common.Enums;

namespace Media_glass.Common.Items_operations
{
    /// <summary>
    /// ListView stored object.
    /// </summary>
    public class ItemContent : ICloneable, INotifyPropertyChanged
    {
        private string code;
        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }
        /// <summary>
        /// Media name.
        /// </summary>
        private string name;

        /// <summary>
        /// Media name.
        /// </summary>
        public string Name
        {
            get { return name; }
            set 
            { 
                name = value;
                OnPropertyChanged("Name");
            }
        }
        //private string beginTime;
        //public string BeginTime
        //{
        //    get { return beginTime; }
        //    set
        //    {
        //        beginTime = value;
        //        OnPropertyChanged("BeginTime");
        //    }
        //}
        //private string endTime;
        //public string EndTime
        //{
        //    get { return endTime; }
        //    set
        //    {
        //        endTime = value;
        //        OnPropertyChanged("EndTime");
        //    }
        //}
        /// <summary>
        /// Time spent.
        /// </summary>
        string time;

        /// <summary>
        /// Time spent.
        /// </summary>
        public string Time
        {
            get { return time; }
            set 
            { 
                time = value;
                OnPropertyChanged("Time");
            }
        }

        /// <summary>
        /// Type of media.
        /// </summary>
        MediaType mediaType = MediaType.NotPlayed;

        /// <summary>
        /// Type of media.
        /// </summary>
        public MediaType MediaType
        {
            get { return mediaType; }
            set { mediaType = value; }
        }        

        /// <summary>
        /// Video resolution.
        /// </summary>
        string resolution;

        /// <summary>
        /// Video resolution.
        /// </summary>
        public string Resolution
        {
            get { return resolution; }
            set { resolution = value; }
        }

        #region ICloneable Members

        /// <summary>
        /// Make a copy.
        /// </summary>        
        public object Clone()
        {
            return new ItemContent()
            {
                Name = this.Name,
                Time = this.Time,
                MediaType = this.MediaType,                
                Resolution = this.Resolution
            };
        }
        
        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Indicates that property is changed, otherwise data will not be redrawed in list view.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Indicates that property is changed, otherwise data will not be redrawed in list view.
        /// </summary>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion        
    }
}
