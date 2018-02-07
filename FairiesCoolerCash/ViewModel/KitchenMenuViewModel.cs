using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;

namespace FairiesCoolerCash.ViewModel
{
    public class KitchenMenuViewModel : BarMenuViewModel
    {
        public KitchenMenuViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
            this.SectionType = (int)DXInfo.Models.SectionType.Kitchen;
        }
    }
    public class Kitchen2MenuViewModel : BarMenuViewModel
    {
        public Kitchen2MenuViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
            this.SectionType = (int)DXInfo.Models.SectionType.Houchu2;
        }
    }
}
