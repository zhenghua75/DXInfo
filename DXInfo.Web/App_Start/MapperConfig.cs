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
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DXInfo.Models.ScrapVouch, DXInfo.Models.RdRecord>();
                cfg.CreateMap<DXInfo.Models.ScrapVouchs, DXInfo.Models.RdRecords>();

                cfg.CreateMap<DXInfo.Models.TransVouch, DXInfo.Models.RdRecord>();
                cfg.CreateMap<DXInfo.Models.TransVouchs, DXInfo.Models.RdRecords>();

                cfg.CreateMap<DXInfo.Models.CheckVouch, DXInfo.Models.RdRecord>();
                cfg.CreateMap<DXInfo.Models.CheckVouchs, DXInfo.Models.RdRecords>();

                cfg.CreateMap<DXInfo.Models.MixVouch, DXInfo.Models.RdRecord>();
                cfg.CreateMap<DXInfo.Models.MixVouchs, DXInfo.Models.RdRecords>();

                cfg.CreateMap<DXInfo.Models.AdjustLocatorVouch, DXInfo.Models.RdRecord>();
                cfg.CreateMap<DXInfo.Models.AdjustLocatorVouchs, DXInfo.Models.RdRecords>();

                cfg.CreateMap<DXInfo.Models.AdjustLocatorVouch, DXInfo.Models.InvLocator>();
                cfg.CreateMap<DXInfo.Models.AdjustLocatorVouchs, DXInfo.Models.InvLocator>();

                cfg.CreateMap<DXInfo.Models.RdRecord, DXInfo.Models.InvLocator>();
                cfg.CreateMap<DXInfo.Models.RdRecords, DXInfo.Models.InvLocator>();

                cfg.CreateMap<DXInfo.Web.Models.VouchModel, DXInfo.Models.RdRecord>();
                cfg.CreateMap<DXInfo.Web.Models.VouchModel, DXInfo.Models.MixVouch>();
                cfg.CreateMap<DXInfo.Web.Models.VouchModel, DXInfo.Models.TransVouch>();
                cfg.CreateMap<DXInfo.Web.Models.VouchModel, DXInfo.Models.AdjustLocatorVouch>();
                cfg.CreateMap<DXInfo.Web.Models.VouchModel, DXInfo.Models.CheckVouch>();
                cfg.CreateMap<DXInfo.Web.Models.VouchModel, DXInfo.Models.ScrapVouch>();

                cfg.CreateMap<DXInfo.Web.Models.VouchsGridModel, DXInfo.Models.RdRecord>();
                cfg.CreateMap<DXInfo.Web.Models.VouchsGridModel, DXInfo.Models.MixVouch>();
                cfg.CreateMap<DXInfo.Web.Models.VouchsGridModel, DXInfo.Models.TransVouch>();
                cfg.CreateMap<DXInfo.Web.Models.VouchsGridModel, DXInfo.Models.AdjustLocatorVouch>();
                cfg.CreateMap<DXInfo.Web.Models.VouchsGridModel, DXInfo.Models.CheckVouch>();
                cfg.CreateMap<DXInfo.Web.Models.VouchsGridModel, DXInfo.Models.ScrapVouch>();

                cfg.CreateMap<DXInfo.Models.RdRecord, DXInfo.Web.Models.VouchModel>();
                cfg.CreateMap<DXInfo.Models.MixVouch, DXInfo.Web.Models.VouchModel>();
                cfg.CreateMap<DXInfo.Models.TransVouch, DXInfo.Web.Models.VouchModel>();
                cfg.CreateMap<DXInfo.Models.AdjustLocatorVouch, DXInfo.Web.Models.VouchModel>();
                cfg.CreateMap<DXInfo.Models.CheckVouch, DXInfo.Web.Models.VouchModel>();
                cfg.CreateMap<DXInfo.Models.ScrapVouch, DXInfo.Web.Models.VouchModel>();

                cfg.CreateMap<DXInfo.Web.Models.VouchsModel, DXInfo.Models.RdRecords>();
                cfg.CreateMap<DXInfo.Web.Models.VouchsModel, DXInfo.Models.MixVouchs>();
                cfg.CreateMap<DXInfo.Web.Models.VouchsModel, DXInfo.Models.TransVouchs>();
                cfg.CreateMap<DXInfo.Web.Models.VouchsModel, DXInfo.Models.AdjustLocatorVouchs>();
                cfg.CreateMap<DXInfo.Web.Models.VouchsModel, DXInfo.Models.CheckVouchs>();
                cfg.CreateMap<DXInfo.Web.Models.VouchsModel, DXInfo.Models.ScrapVouchs>();

                cfg.CreateMap<DXInfo.Models.MixVouch, DXInfo.Models.TransVouch>();
                cfg.CreateMap<DXInfo.Models.MixVouchs, DXInfo.Models.TransVouchs>();

                cfg.CreateMap<DXInfo.Models.CurrentInvLocator, DXInfo.Models.CheckVouchs>();
                cfg.CreateMap<DXInfo.Models.CurrentStock, DXInfo.Models.CheckVouchs>();

                cfg.CreateMap<DXInfo.Models.Receipts, DXInfo.Models.ReceiptHis>();

                cfg.CreateMap<DXInfo.Models.tbAssociator, DXInfo.Models.tbAssociatorLog>();
                cfg.CreateMap<DXInfo.Models.tbAssociator, DXInfo.Models.tbAssociatorSync>();

                cfg.CreateMap<DXInfo.Models.Cards, DXInfo.Models.CardsLog>();
            });
            #endregion
        }
    }
}