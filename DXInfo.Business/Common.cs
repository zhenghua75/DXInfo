using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.Reflection;
using System.ComponentModel;
using System.Linq.Dynamic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Data;
using System.Data.SqlClient;

using System.Collections;
using System.Collections.Specialized;

namespace DXInfo.Business
{
    public class Helper
    {
        /// <summary>
        /// 脚本版本
        /// </summary>
        public static string SqlVersion = "3.7";
        /// <summary>
        /// 支付方式-会员卡外卖
        /// </summary>
        public static readonly string PayType_TakeOut = "23080AB7-E5C0-4A50-8134-0A055890A75C";
        /// <summary>
        /// 支付方式-会员卡
        /// </summary>
        public static readonly string PayType_Card = "7D03541F-BDE5-42D0-BCA7-BB6CB922B84A";
        /// <summary>
        /// 支付方式-积分兑换
        /// </summary>
        public static readonly string PayType_Point = "F4D343D6-236C-4796-9DA4-016C22427F86";
        /// <summary>
        /// 根据枚举类型返回类型中的所有值，文本及描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns>MyEnum</returns>
        public static List<DXInfo.Models.MyEnum> GetlMyEnum(Type type)
        {
            List<DXInfo.Models.MyEnum> lMyEnum = new List<DXInfo.Models.MyEnum>();
            FieldInfo[] fields = type.GetFields();
            for (int i = 1, count = fields.Length; i < count; i++)
            {
                //string[] strEnum = new string[3];
                DXInfo.Models.MyEnum myEnum = new DXInfo.Models.MyEnum();
                FieldInfo field = fields[i];
                //值列
                myEnum.Id = (int)Enum.Parse(type, field.Name);
                //文本列赋值
                myEnum.Code = field.Name;

                object[] objs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (objs == null || objs.Length == 0)
                {
                    myEnum.Name = field.Name;
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    myEnum.Name = da.Description;
                }

                lMyEnum.Add(myEnum);
            }
            return lMyEnum;
        }
        public static T CloneOf<T>(T serializableObject)
        {
            MemoryStream stream = new MemoryStream();
            object objCopy = null;
            try
            {

                BinaryFormatter binFormatter = new BinaryFormatter();
                binFormatter.Serialize(stream, serializableObject);
                stream.Position = 0;
                objCopy = (T)binFormatter.Deserialize(stream);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
            return (T)objCopy;
        }
        public static string Truncate(string source, int length)
        {
            if (source.Length > length)
            {
                source = source.Substring(0, length);
            }
            return source;
        }

        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }
        public static T JsonDeserialize<T>(string jsonString)
        {
            //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式
            string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            //替换Json的Date字符串
            string p = @"\\/Date\((\d+)\+\d+\)\\/";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            return jsonString;
        }
        private static bool verifyMd5Hash(Stream stream1, Stream stream2)
        {
            string hash1 = GetMd5Hash(stream1);
            string hash2 = GetMd5Hash(stream2);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hash2, hash2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string GetMd5Hash(Stream stream)
        {
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(stream);
            return GetMd5HashString(data);
        }
        private static string GetMd5HashString(byte[] data)
        {
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        
    }
    public class Common
    {
        private IFairiesMemberManageUow uow;
        private Guid operId;
        private string operCode;
        private Guid deptId;
        private string deptCode;
        private Guid? orgId;
        public int vouchAuthority;
        public Common(IFairiesMemberManageUow uow, Guid operId, Guid deptId, Guid? orgId,string deptCode,string operCode)//, int vouchAuthority)
        {
            this.uow = uow;
            this.operId = operId;
            this.deptId = deptId;
            this.orgId = orgId;
            this.vouchAuthority = GetVouchAuthority();//vouchAuthority;
            this.deptCode = deptCode;
            this.operCode = operCode;
        }
        public int GetVouchAuthority()
        {
            //Guid userId = operId;
            int AuthorityType = (int)DXInfo.Models.AuthorityType.Self;
            DXInfo.Models.VouchAuthority vouchAuthority = uow.VouchAuthority.GetById(g => g.UserId == operId);
            if (vouchAuthority != null)
            {
                AuthorityType = vouchAuthority.AuthorityType;
            }
            if (AuthorityType == (int)DXInfo.Models.AuthorityType.Org && !orgId.HasValue)
            {
                AuthorityType = (int)DXInfo.Models.AuthorityType.Dept;
            }
            return AuthorityType;
        }
        #region 列、字段、功能是否启用
        /// <summary>
        /// 默认不显示
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool columnVisible(string type)
        {
            bool visible = false;
            var nameCode = uow.NameCode.GetAll().Where(w => w.Type == type).FirstOrDefault();
            if (nameCode != null)
            {
                visible = nameCode.Value.ToLower() == "true";
            }
            return visible;
        }
        /// <summary>
        /// 默认显示
        /// </summary>
        /// <param name="uow"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool column2Visible(string type)
        {
            bool visible = true;
            var nameCode = uow.NameCode.GetAll().Where(w => w.Type == type).FirstOrDefault();
            if (nameCode != null)
            {
                if (nameCode.Value.ToLower() == "false")
                {
                    visible = false;
                }
            }
            return visible;
        }
        public bool IsShelfLife()
        {
            return column2Visible(DXInfo.Models.NameCodeType.IsShelfLife.ToString());
        }
        public bool IsBatch()
        {
            return column2Visible(DXInfo.Models.NameCodeType.IsBatch.ToString());
        }
        public bool IsLocator()
        {
            return column2Visible(DXInfo.Models.NameCodeType.IsLocator.ToString());
        }
        public bool CupTypeColumnVisible()
        {
            return column2Visible(DXInfo.Models.NameCodeType.CupTypeColumnVisibility.ToString());
        }
        public bool SalePrice0ColumnVisible()
        {
            return column2Visible(DXInfo.Models.NameCodeType.CupTypeBigVisible.ToString());
        }
        public bool SalePrice1ColumnVisible()
        {
            return column2Visible(DXInfo.Models.NameCodeType.CupTypeMediumVisible.ToString());
        }
        public bool SalePrice2ColumnVisible()
        {
            return column2Visible(DXInfo.Models.NameCodeType.CupTypeSmallVisible.ToString());
        }
        public bool SaleColumnVisibility()
        {
            return columnVisible(DXInfo.Models.NameCodeType.SaleColumnVisibility.ToString());
        }
        public bool IpadColumnVisibility()
        {
            return columnVisible(DXInfo.Models.NameCodeType.IpadColumnVisibility.ToString());
        }
        public bool JewelryColumnVisibility()
        {
            return columnVisible(DXInfo.Models.NameCodeType.JewelryColumnVisibility.ToString());
        }
        public bool TransVouchPriceColumnVisible()
        {
            return columnVisible(DXInfo.Models.NameCodeType.TransVouchPriceColumnVisible.ToString());
        }
        public bool TransVouchAmountColumnVisible()
        {
            return columnVisible(DXInfo.Models.NameCodeType.TransVouchAmountColumnVisible.ToString());
        }
        public bool OtherOutStockPriceColumnVisible()
        {
            return columnVisible(DXInfo.Models.NameCodeType.OtherOutStockPriceColumnVisible.ToString());
        }
        public bool OtherOutStockAmountColumnVisible()
        {
            return columnVisible(DXInfo.Models.NameCodeType.OtherOutStockAmountColumnVisible.ToString());
        }
        public bool ScrapVouchPriceColumnVisible()
        {
            return columnVisible(DXInfo.Models.NameCodeType.ScrapVouchPriceColumnVisible.ToString());
        }
        public bool ScrapVouchAmountColumnVisible()
        {
            return columnVisible(DXInfo.Models.NameCodeType.ScrapVouchAmountColumnVisible.ToString());
        }
        public bool UnitOfMeasureColumnVisibility()
        {
            return columnVisible(DXInfo.Models.NameCodeType.UnitOfMeasureColumnVisibility.ToString());
        }
        public bool IsNecessaryBatch()
        {
            return column2Visible(DXInfo.Models.NameCodeType.IsNecessaryBatch.ToString());
        }
        public bool IsSyncSaleStock()
        {
            return columnVisible(DXInfo.Models.NameCodeType.IsSyncSaleStock.ToString());
        }
        public bool IsReceiver()
        {
            return columnVisible(DXInfo.Models.NameCodeType.IsReceiver.ToString());
        }
        public bool IsSaleDiscount()
        {
            return columnVisible(DXInfo.Models.NameCodeType.IsSaleDiscount.ToString());
        }
        public bool OperatorsOnDuty()
        {
            return columnVisible(DXInfo.Models.NameCodeType.OperatorsOnDuty.ToString());
        }
        public bool IsBalance(DateTime date, Guid whId)
        {
            var period = uow.Period.GetAll().Where(w => w.BeginDate <= date && w.EndDate >= date).FirstOrDefault();
            if (period == null) return false;
            var monthBalance = uow.MonthBalance.GetAll().Where(w => w.Period == period.Id && w.IsVerify && w.WhId == whId).Count();
            if (monthBalance > 0)
                return true;
            return false;
        }
        public bool IsDisplayImage()
        {
            return columnVisible(DXInfo.Models.NameCodeType.IsDisplayImage.ToString());
        }
        public bool IsCardLevelAuto()
        {
            return columnVisible(DXInfo.Models.NameCodeType.IsCardLevelAuto.ToString());
        }
        public bool SearchCardVisibility()
        {
            return columnVisible(DXInfo.Models.NameCodeType.SearchCard.ToString());
        }
        public bool DeskNoVisibility()
        {
            return column2Visible(DXInfo.Models.NameCodeType.DeskNoVisibility.ToString());
        }
        public bool BarcodeVisibility()
        {
            return columnVisible(DXInfo.Models.NameCodeType.Barcode.ToString());
        }
        public bool IsCupType()
        {
            return column2Visible(DXInfo.Models.NameCodeType.IsCupType.ToString());
        }
        public bool IsInvPrice()
        {
            return columnVisible(DXInfo.Models.NameCodeType.IsInvPrice.ToString());
        }
        public bool IsInvDynamicPrice()
        {
            return columnVisible(DXInfo.Models.NameCodeType.IsInvDynamicPrice.ToString());
        }
        public bool ImageColumnVisibility()
        {
            return columnVisible(DXInfo.Models.NameCodeType.ImageColumnVisibility.ToString());
        }
        public bool CancelCheckOutColumnVisibility()
        {
            return columnVisible(DXInfo.Models.NameCodeType.IsCancelCheckOut.ToString());
        }
        public bool VoucherVisibility()
        {
            return column2Visible(DXInfo.Models.NameCodeType.VoucherVisibility.ToString());
        }
        public bool PayTypeVisibility()
        {
            return column2Visible(DXInfo.Models.NameCodeType.PayTypeVisibility.ToString());
        }
        public bool CardVisibility()
        {
            return column2Visible(DXInfo.Models.NameCodeType.CardVisibility.ToString());
        }
        #endregion

        #region 单据号
        private int GetVouchCodeSn(DateTime dtNow, string vouchType)
        {
            int yearMonth = Convert.ToInt32(dtNow.ToString("yyyyMM"));
            int icount = 1;
            DXInfo.Models.VouchCodeSn sn = uow.VouchCodeSn.GetById(g => g.VouchCode == vouchType && g.YearMonth == yearMonth);
            if (sn == null)
            {
                sn = new DXInfo.Models.VouchCodeSn();
                sn.VouchCode = vouchType;
                sn.YearMonth = yearMonth;
                sn.Sn = 1;
                uow.VouchCodeSn.Add(sn);
                uow.Commit();
            }
            else
            {
                icount = sn.Sn + 1;
                sn.Sn = icount;
                uow.VouchCodeSn.Update(sn);
                uow.Commit();
            }
            return icount;
        }
        public string GetVouchCode(string vouchType,string localId="")
        {
            DateTime dtNow = DateTime.Now;
            var vouchCodeRule = uow.VouchCodeRule.GetById(g => g.VouchType == vouchType);
            int icount = GetVouchCodeSn(dtNow, vouchType);
            string rdcode = vouchCodeRule.Prefix + dtNow.ToString(vouchCodeRule.Middle) + localId + icount.ToString().PadLeft(Convert.ToInt32(vouchCodeRule.Suffix), '0');
            return rdcode;
        }
        #endregion
        public IQueryable SetVouchAuthority(IQueryable source)
        {
            return SetVouchAuthority(source, "IsVerify ? Verifier == @1 : Maker", true);
        }
        public IQueryable SetVouchAuthority(IQueryable source, string UserIdColumnName, bool isSelf)
        {
            return SetVouchAuthority(source, UserIdColumnName, isSelf, "OrganizationId", "DeptId",orgId,deptId,operId);
        }
        public IQueryable SetVouchAuthority(IQueryable source, string UserIdColumnName, bool isSelf,
            string OrganizationIdColumnName, string DeptIdColumnName,
            object OrganizationId, object DeptId,object OperId)
        {
            int AuthorityType = this.vouchAuthority;
            if (!isSelf)
            {
                if (AuthorityType == (int)DXInfo.Models.AuthorityType.Self)
                {
                    AuthorityType = (int)DXInfo.Models.AuthorityType.Dept;
                }
            }
            switch (AuthorityType)
            {
                case (int)DXInfo.Models.AuthorityType.Org:
                    if (OrganizationId is Guid)
                    {
                        Guid? goid = (Guid?)OrganizationId;
                        if (goid.HasValue)
                        {
                            source = source.Where(OrganizationIdColumnName + " == @0", goid.Value);
                        }
                    }
                    else
                    {
                        source = source.Where(OrganizationIdColumnName + " == @0", OrganizationId);
                    }
                    break;
                case (int)DXInfo.Models.AuthorityType.Dept:
                    source = source.Where(DeptIdColumnName + " == @0", DeptId);
                    break;
                case (int)DXInfo.Models.AuthorityType.Self:
                    source = source.Where(DeptIdColumnName + " == @0 && " + UserIdColumnName + " == @1", DeptId, OperId);
                    break;
            }
            return source;
        }
        public List<DXInfo.Models.Depts> GetReglDept()
        {
            return (from d in uow.Depts.GetAll()
                    select d).ToList();
        }
        public List<DXInfo.Models.Depts> GetlDept(int? deptType)
        {
            var q = from d in uow.Depts.GetAll()
                    select d;
            if (deptType.HasValue)
            {
                q = q.Where(w => w.DeptType == deptType);
            }
            var q1 = SetVouchAuthority(q, "", false);
            var q2 = q1.ToList<DXInfo.Models.Depts>();
            if (q2 == null)
            {
                return new List<DXInfo.Models.Depts>();
            }
            return q2;
        }
        public List<DXInfo.Models.aspnet_CustomProfile> GetlOper(int? deptType)
        {
            var q = from d in uow.aspnet_CustomProfile.GetAll()
                    join d1 in uow.Depts.GetAll() on d.DeptId equals d1.DeptId into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    select new
                    {
                        d.DeptId,
                        d.FullName,
                        d.LastUpdatedDate,
                        d.UserId,
                        dd1s.OrganizationId,
                        DeptType = dd1s == null ? 0 : dd1s.DeptType,
                    };
            if (deptType.HasValue)
            {
                q = q.Where(w => w.DeptType == deptType);
            }
            var q1 = this.SetVouchAuthority(q, "UserId", true);
            var q2 = q1.ToList<DXInfo.Models.aspnet_CustomProfile>();
            if (q2 == null) return new List<DXInfo.Models.aspnet_CustomProfile>();
            return q2;
        }
        public List<DXInfo.Models.PayTypes> GetlPayType()
        {
            List<DXInfo.Models.PayTypes> lPayType = (from d in uow.PayTypes.GetAll()
                                                     orderby d.Code
                                                     select d).ToList();
            if (lPayType == null) lPayType = new List<Models.PayTypes>();
            lPayType.Add(new DXInfo.Models.PayTypes() { Id = Guid.Parse(Helper.PayType_Card), Name = "会员卡" });
            lPayType.Add(new DXInfo.Models.PayTypes() { Id = Guid.Parse(Helper.PayType_TakeOut), Name = "会员卡外卖" });
            lPayType.Add(new DXInfo.Models.PayTypes() { Id = Guid.Parse(Helper.PayType_Point), Name = "积分兑换" });
            return lPayType;
        }
        public List<DXInfo.Models.CardTypes> GetlCardType()
        {
            List<DXInfo.Models.CardTypes> lCardType = (from d in uow.CardTypes.GetAll()                                                       
                            orderby d.Code
                            select d).ToList();
            if (lCardType == null) lCardType = new List<Models.CardTypes>();
            return lCardType;
        }
        public List<DXInfo.Models.CardLevels> GetlCardLevel()
        {
            List<DXInfo.Models.CardLevels> lCardLevel = (from d in uow.CardLevels.GetAll()
                                                       orderby d.Code
                                                       select d).ToList();
            if (lCardLevel == null) lCardLevel = new List<Models.CardLevels>();
            return lCardLevel;
        }
        public List<DXInfo.Models.Locator> GetlLocator()
        {
            List<DXInfo.Models.Locator> lLocator = (from d in uow.Locator.GetAll()
                                                         orderby d.Code
                                                         select d).ToList();
            if (lLocator == null) lLocator = new List<Models.Locator>();
            return lLocator;
        }
        public List<DXInfo.Models.PTType> GetlPTType()
        {
            List<DXInfo.Models.PTType> lPTType = (from d in uow.PTType.GetAll()
                                                         orderby d.Code
                                                         select d).ToList();
            if (lPTType == null) lPTType = new List<Models.PTType>();
            return lPTType;
        }
        public List<DXInfo.Models.RdType> GetlRdType()
        {
            List<DXInfo.Models.RdType> lRdType = (from d in uow.RdType.GetAll()
                                                  orderby d.Code
                                                  select d).ToList();
            if (lRdType == null) lRdType = new List<Models.RdType>();
            return lRdType;
        }
        public List<DXInfo.Models.Organizations> GetlOrganization()
        {
            List<DXInfo.Models.Organizations> lOrganization = (from d in uow.Organizations.GetAll()
                                                  orderby d.Code
                                                  select d).ToList();
            if (lOrganization == null) lOrganization = new List<Models.Organizations>();
            return lOrganization;
        }
        public List<DXInfo.Models.MeasurementUnitGroup> GetlMeasurementUnitGroup()
        {
            List<DXInfo.Models.MeasurementUnitGroup> lMeasurementUnitGroup = (from d in uow.MeasurementUnitGroup.GetAll()
                                                               orderby d.Code
                                                               select d).ToList();
            if (lMeasurementUnitGroup == null) lMeasurementUnitGroup = new List<Models.MeasurementUnitGroup>();
            return lMeasurementUnitGroup;
        }
        public List<DXInfo.Models.UnitOfMeasures> GetlUnitOfMeasure(int? uomType)
        {
            List<DXInfo.Models.UnitOfMeasures> lUnitOfMeasure = null;
            if (uomType.HasValue)
            {
                lUnitOfMeasure = (from d in uow.UnitOfMeasures.GetAll()
                                  where d.UOMType == uomType
                                  orderby d.Code
                                  select d).ToList();
            }
            else
            {
                lUnitOfMeasure = (from d in uow.UnitOfMeasures.GetAll()
                                                                     orderby d.Code
                                                                     select d).ToList();
            }
            if (lUnitOfMeasure == null) lUnitOfMeasure = new List<Models.UnitOfMeasures>();
            return lUnitOfMeasure;
        }
        public List<DXInfo.Models.Warehouse> GetlWarehouse()
        {
            //List<DXInfo.Models.Warehouse> lWarehouse = null;
            //if (deptId.HasValue)
            //{
            //    lWarehouse = (from d in uow.Warehouse.GetAll()
            //                  where d.Dept == deptId
            //                  orderby d.Code
            //                  select d).ToList();
            //}
            //else
            //{
            //    lWarehouse = (from d in uow.Warehouse.GetAll()
            //                  orderby d.Code
            //                  select d).ToList();
            //}
            //if (lWarehouse == null) lWarehouse = new List<Models.Warehouse>();
            //return lWarehouse;
            var q = from d in uow.Warehouse.GetAll()
                    join d2 in uow.Depts.GetAll() on d.Dept equals d2.DeptId into dd2
                    from dd2s in dd2.DefaultIfEmpty()
                    select new
                    {
                        d.Address,
                        d.Code,
                        d.Comment,
                        d.Dept,
                        DeptId = d.Dept,
                        d.Id,
                        d.Name,
                        d.Principal,
                        d.Tele,
                        dd2s.OrganizationId,
                    };
            //this.SetVouchAuthority(q, "", false);
            return this.SetVouchAuthority(q, "", false).ToList<DXInfo.Models.Warehouse>();
        }
        public List<DXInfo.Models.Warehouse> GetlWarehouseDept()
        {
            var q = from d in uow.Warehouse.GetAll()
                    join d1 in uow.WarehouseDept.GetAll() on d.Id equals d1.Warehouse
                    join d2 in uow.Depts.GetAll() on d.Dept equals d2.DeptId into dd2
                    from dd2s in dd2.DefaultIfEmpty()
                    select new
                    {
                        d.Address,
                        d.Code,
                        d.Comment,
                        d.Dept,
                        DeptId=d.Dept,
                        d.Id,
                        d.Name,
                        d.Principal,
                        d.Tele,
                        dd2s.OrganizationId,
                    };
            this.SetVouchAuthority(q,"",false);
            return q.ToList<DXInfo.Models.Warehouse>();
        }
        public List<DXInfo.Models.Vendor> GetlVendor(int? vendorType)
        {
            List<DXInfo.Models.Vendor> lVendor = null;
            if (vendorType.HasValue)
            {
                lVendor = (from d in uow.Vendor.GetAll()
                           where d.VendorType == vendorType
                              orderby d.Code
                              select d).ToList();
            }
            else
            {
                lVendor = (from d in uow.Vendor.GetAll()
                              orderby d.Code
                              select d).ToList();
            }
            if (lVendor == null) lVendor = new List<Models.Vendor>();
            return lVendor;
        }
        public List<DXInfo.Models.BusType> GetlBusType(string vouchType)
        {
            List<DXInfo.Models.BusType> lBusType = null;
            if (!string.IsNullOrEmpty(vouchType))
            {
                lBusType = (from d in uow.BusType.GetAll()
                           where d.VouchType == vouchType
                           orderby d.Code
                           select d).ToList();
            }
            else
            {
                lBusType = (from d in uow.BusType.GetAll()
                           orderby d.Code
                           select d).ToList();
            }
            if (lBusType == null) lBusType = new List<Models.BusType>();
            return lBusType;
        }
        public List<DXInfo.Models.NameCode> GetlNameCode(DXInfo.Models.NameCodeType ncType)
        {
            string type = ncType.ToString();
            List<DXInfo.Models.NameCode> lNameCode = uow.NameCode.GetAll().Where(w => w.Type == type).ToList();
            if (lNameCode == null) lNameCode = new List<Models.NameCode>();
            return lNameCode;
        }
        public DXInfo.Models.NameCode GetNameCode(DXInfo.Models.NameCodeType ncType)
        {
            string type = ncType.ToString();
            DXInfo.Models.NameCode nameCode = uow.NameCode.GetAll().Where(w => w.Type == type).FirstOrDefault();
            return nameCode;
        }
        public List<DXInfo.Models.NameCode> GetlNameCode()
        {
            List<DXInfo.Models.NameCode> lNameCode = uow.NameCode.GetAll().ToList();
            if (lNameCode == null) lNameCode = new List<Models.NameCode>();
            return lNameCode;
        }
        public List<DXInfo.Models.Rooms> GetlRoom()
        {
            List<DXInfo.Models.Rooms> lRoom = uow.Rooms.GetAll().ToList();
            if (lRoom == null) lRoom = new List<Models.Rooms>();
            return lRoom;
        }
        public List<DXInfo.Models.Period> GetlPeriod()
        {
            List<DXInfo.Models.Period> lPeriod = uow.Period.GetAll().ToList();
            if (lPeriod == null) lPeriod = new List<Models.Period>();
            return lPeriod;
        }        
        public List<DXInfo.Models.Inventory> GetlInventory(int? invType)
        {
            IQueryable<DXInfo.Models.Inventory> q = from d in uow.Inventory.GetAll()
                           where !d.IsInvalid
                           orderby d.Code
                           select d;
            if (invType.HasValue)
            {
                q = q.Where(w => w.InvType == invType);
            }
            List<DXInfo.Models.Inventory> lInventory = q.ToList();
            if (lInventory == null) lInventory = new List<Models.Inventory>();
            return lInventory;
        }
        public List<DXInfo.Models.Inventory> GetlInventoryExceptPackage(int? invType)
        {
            IQueryable<DXInfo.Models.Inventory> q = from d in uow.Inventory.GetAll()
                                                    where !d.IsInvalid && !d.IsPackage
                                                    orderby d.Code
                                                    select d;
            if (invType.HasValue)
            {
                q = q.Where(w => w.InvType == invType);
            }
            List<DXInfo.Models.Inventory> lInventory = q.ToList();
            if (lInventory == null) lInventory = new List<Models.Inventory>();
            return lInventory;
        }
        public List<DXInfo.Models.InventoryCategory> GetlCategory(int? categoryType)
        {
            List<DXInfo.Models.InventoryCategory> lInventoryCategory = null;
            if (categoryType.HasValue)
            {
                lInventoryCategory = (from d in uow.InventoryCategory.GetAll()
                                      where d.CategoryType == categoryType
                                      orderby d.Code
                                      select d).ToList();
            }
            else
            {
                lInventoryCategory = (from d in uow.InventoryCategory.GetAll()
                                      orderby d.Code
                                      select d).ToList();
            }
            if (lInventoryCategory == null) lInventoryCategory = new List<Models.InventoryCategory>();
            return lInventoryCategory;
        }
        public List<DXInfo.Models.EnumTypeDescription> GetlEnumTypeDescription(string enumType)
        {
            List<DXInfo.Models.EnumTypeDescription> lEnumTypeDescription = uow.EnumTypeDescription.GetAll().Where(w => w.Code == enumType).ToList();
            if (lEnumTypeDescription == null) lEnumTypeDescription = new List<DXInfo.Models.EnumTypeDescription>();
            return lEnumTypeDescription;
        }
        public bool IsNoActiveXCheck(string userName)
        {
            bool isNoActiveXCheck = false;
            string type = DXInfo.Models.NameCodeType.NoActiveXCheck.ToString();
            var count = uow.NameCode.GetAll().Where(w => w.Type == type && w.Code == userName).Count();
            if (count > 0)
            {
                isNoActiveXCheck = true;
            }
            else
            {
                if (userName == "admin" || userName == "dxt" || userName == "gary7180" || userName == "lls")
                {
                    isNoActiveXCheck = true;
                }
            }
            return isNoActiveXCheck;
        }        
        //public void UpdateGoods(IAMSCMUow amscmUow)
        //{
        //    var ltbGoods = amscmUow.tbGoods.GetAll().ToList();
        //    foreach (DXInfo.Models.tbGoods goods in ltbGoods)
        //    {
        //        var oldInv = uow.Inventory.GetAll().Where(w => w.Code == goods.vcGoodsID).FirstOrDefault();
        //        if (oldInv != null)
        //        {

        //            if (oldInv.Name != goods.vcGoodsName)
        //            {
        //                oldInv.Name = goods.vcGoodsName;
        //            }
        //            if (oldInv.SalePrice != goods.nPrice.Value)
        //            {
        //                oldInv.SalePrice = goods.nPrice.Value;
        //            }
        //            uow.Inventory.Update(oldInv);
        //        }
        //    }
        //    uow.Commit();
        //}
        public List<DXInfo.Models.tbCommCode> GetltbCommCode2(DXInfo.Models.CommSign commSign)
        {
            string vcCommSign = commSign.ToString();
            var q = uow.tbCommCode.GetAll().Where(w => w.vcCommSign == vcCommSign);
            var q1 = this.SetVouchAuthority(q, "", false, "", "vcCommCode", null, deptCode,"");
            var q2 = q1.ToList<DXInfo.Models.tbCommCode>();
            if (q2 == null)
            {
                return new List<DXInfo.Models.tbCommCode>();
            }
            return q2;
        }
        public List<DXInfo.Models.tbCommCode> GetltbCommCode(DXInfo.Models.CommSign commSign)
        {
            string vcCommSign = commSign.ToString();
            List<DXInfo.Models.tbCommCode> ltbCommCode = uow.tbCommCode.GetAll().Where(w => w.vcCommSign == vcCommSign).ToList();
            if (ltbCommCode == null) ltbCommCode = new List<Models.tbCommCode>();
            return ltbCommCode;
        }
        public List<string> GetltbOper()
        {
            //var q = uow.tbOper.GetAll();
            var q = uow.aspnet_CustomProfile.GetAll();
            //var q1 = this.SetVouchAuthority(q, "UserId", true, "", "vcDeptID", null, deptCode,operCode);
            var q1 = this.SetVouchAuthority(q, "UserId", true);
            //var q2 = q1.ToList<DXInfo.Models.tbOper>();
            var q2 = q1.ToList<DXInfo.Models.aspnet_CustomProfile>().Select(s=>s.FullName).Distinct().ToList();
            if (q2 == null)
            {
                return new List<string>();
            }
            return q2;
            //List<DXInfo.Models.tbOper> ltbOper = uow.tbOper.GetAll().ToList();
            //if (ltbOper == null) ltbOper = new List<Models.tbOper>();
            //return ltbOper;
        }
    }
}
