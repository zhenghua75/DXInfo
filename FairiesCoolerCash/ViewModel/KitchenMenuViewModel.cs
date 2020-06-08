using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using DXInfo.Data.Contracts;

namespace FairiesCoolerCash.ViewModel
{
    public class KitchenMenuViewModel : BarMenuViewModel
    {
        private readonly IMapper mapper;

        public KitchenMenuViewModel(IFairiesMemberManageUow uow, IMapper mapper)
            : base(uow,mapper)
        {
            this.mapper = mapper;
            this.SectionType = (int)DXInfo.Models.SectionType.Kitchen;
        }
    }
    public class Kitchen2MenuViewModel : BarMenuViewModel
    {
        private readonly IMapper mapper;

        public Kitchen2MenuViewModel(IFairiesMemberManageUow uow, IMapper mapper)
            : base(uow, mapper)
        {
            this.mapper = mapper;
            this.SectionType = (int)DXInfo.Models.SectionType.Houchu2;
        }
    }
    public class CodeDishViewModel : BarMenuViewModel
    {
        private readonly IMapper mapper;

        public CodeDishViewModel(IFairiesMemberManageUow uow, IMapper mapper)
            : base(uow, mapper)
        {
            this.mapper = mapper;
            this.SectionType = (int)DXInfo.Models.SectionType.CodeDish;
        }
    }
}
