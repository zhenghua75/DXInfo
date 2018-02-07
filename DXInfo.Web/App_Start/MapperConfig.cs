using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace DXInfo.Web.App_Start
{
    public class MapperConfig
    {
        public static void RegisterMapper()
        {
            #region 映射对象
            Mapper.CreateMap<DXInfo.Models.ScrapVouch, DXInfo.Models.RdRecord>();
            Mapper.CreateMap<DXInfo.Models.ScrapVouchs, DXInfo.Models.RdRecords>();

            Mapper.CreateMap<DXInfo.Models.TransVouch, DXInfo.Models.RdRecord>();
            Mapper.CreateMap<DXInfo.Models.TransVouchs, DXInfo.Models.RdRecords>();

            Mapper.CreateMap<DXInfo.Models.CheckVouch, DXInfo.Models.RdRecord>();
            Mapper.CreateMap<DXInfo.Models.CheckVouchs, DXInfo.Models.RdRecords>();

            Mapper.CreateMap<DXInfo.Models.MixVouch, DXInfo.Models.RdRecord>();
            Mapper.CreateMap<DXInfo.Models.MixVouchs, DXInfo.Models.RdRecords>();

            Mapper.CreateMap<DXInfo.Models.AdjustLocatorVouch, DXInfo.Models.RdRecord>();
            Mapper.CreateMap<DXInfo.Models.AdjustLocatorVouchs, DXInfo.Models.RdRecords>();

            Mapper.CreateMap<DXInfo.Models.AdjustLocatorVouch, DXInfo.Models.InvLocator>();
            Mapper.CreateMap<DXInfo.Models.AdjustLocatorVouchs, DXInfo.Models.InvLocator>();

            Mapper.CreateMap<DXInfo.Models.RdRecord, DXInfo.Models.InvLocator>();
            Mapper.CreateMap<DXInfo.Models.RdRecords, DXInfo.Models.InvLocator>();

            Mapper.CreateMap<DXInfo.Web.Models.VouchModel, DXInfo.Models.RdRecord>();
            Mapper.CreateMap<DXInfo.Web.Models.VouchModel, DXInfo.Models.MixVouch>();
            Mapper.CreateMap<DXInfo.Web.Models.VouchModel, DXInfo.Models.TransVouch>();
            Mapper.CreateMap<DXInfo.Web.Models.VouchModel, DXInfo.Models.AdjustLocatorVouch>();
            Mapper.CreateMap<DXInfo.Web.Models.VouchModel, DXInfo.Models.CheckVouch>();
            Mapper.CreateMap<DXInfo.Web.Models.VouchModel, DXInfo.Models.ScrapVouch>();

            Mapper.CreateMap<DXInfo.Web.Models.VouchsGridModel, DXInfo.Models.RdRecord>();
            Mapper.CreateMap<DXInfo.Web.Models.VouchsGridModel, DXInfo.Models.MixVouch>();
            Mapper.CreateMap<DXInfo.Web.Models.VouchsGridModel, DXInfo.Models.TransVouch>();
            Mapper.CreateMap<DXInfo.Web.Models.VouchsGridModel, DXInfo.Models.AdjustLocatorVouch>();
            Mapper.CreateMap<DXInfo.Web.Models.VouchsGridModel, DXInfo.Models.CheckVouch>();
            Mapper.CreateMap<DXInfo.Web.Models.VouchsGridModel, DXInfo.Models.ScrapVouch>();

            Mapper.CreateMap<DXInfo.Models.RdRecord, DXInfo.Web.Models.VouchModel>();
            Mapper.CreateMap<DXInfo.Models.MixVouch, DXInfo.Web.Models.VouchModel>();
            Mapper.CreateMap<DXInfo.Models.TransVouch, DXInfo.Web.Models.VouchModel>();
            Mapper.CreateMap<DXInfo.Models.AdjustLocatorVouch, DXInfo.Web.Models.VouchModel>();
            Mapper.CreateMap<DXInfo.Models.CheckVouch, DXInfo.Web.Models.VouchModel>();
            Mapper.CreateMap<DXInfo.Models.ScrapVouch, DXInfo.Web.Models.VouchModel>();

            Mapper.CreateMap<DXInfo.Web.Models.VouchsModel, DXInfo.Models.RdRecords>();
            Mapper.CreateMap<DXInfo.Web.Models.VouchsModel, DXInfo.Models.MixVouchs>();
            Mapper.CreateMap<DXInfo.Web.Models.VouchsModel, DXInfo.Models.TransVouchs>();
            Mapper.CreateMap<DXInfo.Web.Models.VouchsModel, DXInfo.Models.AdjustLocatorVouchs>();
            Mapper.CreateMap<DXInfo.Web.Models.VouchsModel, DXInfo.Models.CheckVouchs>();
            Mapper.CreateMap<DXInfo.Web.Models.VouchsModel, DXInfo.Models.ScrapVouchs>();

            Mapper.CreateMap<DXInfo.Models.MixVouch, DXInfo.Models.TransVouch>();
            Mapper.CreateMap<DXInfo.Models.MixVouchs, DXInfo.Models.TransVouchs>();

            Mapper.CreateMap<DXInfo.Models.CurrentInvLocator, DXInfo.Models.CheckVouchs>();
            Mapper.CreateMap<DXInfo.Models.CurrentStock, DXInfo.Models.CheckVouchs>();

            Mapper.CreateMap<DXInfo.Models.Receipts, DXInfo.Models.ReceiptHis>();

            Mapper.CreateMap<DXInfo.Models.tbAssociator, DXInfo.Models.tbAssociatorLog>();
            Mapper.CreateMap<DXInfo.Models.tbAssociator, DXInfo.Models.tbAssociatorSync>();

            Mapper.CreateMap<DXInfo.Models.Cards, DXInfo.Models.CardsLog>();
            #endregion
        }
    }
}