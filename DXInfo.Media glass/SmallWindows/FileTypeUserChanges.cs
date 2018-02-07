#region License

//Media glass - my simple WPF player
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
using FormatRegistrClassLibrary;
using System.ComponentModel;
using System.Windows.Documents;

namespace Media_glass
{
    /// <summary>
    /// User changes with file type settings.
    /// </summary>
    public class FileTypeUserChanges : INotifyPropertyChanged
    {
        /// <summary>
        /// Constructor -  initializes the instance of user changes class
        /// </summary>
        public FileTypeUserChanges(MediaFormatGroup formatGroup,string Name)
        {
            this.formatGroup = formatGroup;
            this.Restore();
            this.Name = Name;
        }

        /// <summary>
        /// Format name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Is Format assigned to our application ?
        /// </summary>
        public bool isAssigned = false;

        /// <summary>
        /// Is Format assigned to our application ?
        /// </summary>
        public bool IsAssigned 
        {
            get
            {
                return this.isAssigned;
            }

            set
            {
                this.isAssigned = value;
                this.OnPropertyChanged("IsAssigned");
            }
        }

        /// <summary>
        /// Is user change settings ?
        /// </summary>
        public bool IsChanged
        {
            get
            {
                return this.IsAssigned != this.formatGroup.IsAssigned;
            }
        }

        /// <summary>
        /// Current registry format settings.
        /// </summary>
        MediaFormatGroup formatGroup { get; set; }

        /// <summary>
        /// All format extensions.
        /// </summary>
        public List<Inline> Description
        {
            get
            {
                //useful url
                //http://msdn.microsoft.com/ru-ru/library/system.windows.controls.textblock.aspx

                List<Inline> inlines = new List<Inline>();

                inlines.Add(new Run(String.Format("{0} 格式包括", this.Name)));
                inlines.Add(new Bold(new Run(String.Join(" , ", this.formatGroup.Formats).Replace(".", ""))));
                inlines.Add(new Run(" 等扩展。"));

                return inlines;
            }
        }

        /// <summary>
        /// Save changes to registry.
        /// </summary>
        public void Save()
        {
            this.formatGroup.IsAssigned = this.IsAssigned;
        }

        /// <summary>
        /// Rollback user changes.
        /// </summary>
        public void Restore()
        {
            this.IsAssigned = this.formatGroup.IsAssigned;
        }        

        /// <summary>
        /// Show name in the list box.
        /// </summary>        
        public override string ToString()
        {
            return this.Name;
        }

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
