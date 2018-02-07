using System;
using System.Linq;
namespace Trirand.Web.Mvc
{
	public class JQGridDataResolvedEventArgs : EventArgs
	{
		public IQueryable _currentData;
		public IQueryable _allData;
        public IQueryable _filterData;
		public JQGrid _gridModel;
		public JQGrid GridModel
		{
			get
			{
				return this._gridModel;
			}
			set
			{
				this._gridModel = value;
			}
		}
		public IQueryable CurrentData
		{
			get
			{
				return this._currentData;
			}
			set
			{
				this._currentData = value;
			}
		}
		public IQueryable AllData
		{
			get
			{
				return this._allData;
			}
			set
			{
				this._allData = value;
			}
		}
        public IQueryable FilterData
        {
            get { return this._filterData; }
            set { this._filterData = value; }
        }
        public IQueryable IgnoreFilterFieldData { get; set; }
        public JQGridDataResolvedEventArgs(JQGrid gridModel, IQueryable currentData, IQueryable allData, IQueryable filterData, IQueryable ignoreFilterFieldData)
		{
			this._currentData = currentData;
			this._allData = allData;
            this._filterData = filterData;
			this._gridModel = gridModel;
            this.IgnoreFilterFieldData = ignoreFilterFieldData;
		}
	}
}
