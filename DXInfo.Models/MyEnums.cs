using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DXInfo.Models
{
    #region MyEnum
    //[Serializable]
    //public class MyEnumList : List<MyEnum>, ICloneable
    //{
    //    public MyEnumList() : base() { }
    //    public MyEnumList(int capacity) : base(capacity) { }
    //    public MyEnumList(IEnumerable<MyEnum> collection)
    //        : base(collection)
    //    {
    //    }
    //    public object Clone()
    //    {
    //        MemoryStream ms = new MemoryStream();
    //        object obj;
    //        try
    //        {
    //            BinaryFormatter bf = new BinaryFormatter();
    //            bf.Serialize(ms, this);
    //            ms.Seek(0, SeekOrigin.Begin);
    //            obj = bf.Deserialize(ms);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //        finally
    //        {
    //            ms.Close();
    //        }

    //        return obj;
    //    }
    //}
    //[Serializable]
    public class MyEnum : INotifyPropertyChanged//, ICloneable
    {
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Id"));
            }
        }

        private string _Code;
        public string Code
        {
            get { return _Code; }
            set
            {
                _Code = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Code"));
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }

        //private bool _IsSelected;
        //public bool IsSelected
        //{
        //    get { return _IsSelected; }
        //    set
        //    {
        //        _IsSelected = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("IsSelected"));
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
        //public object Clone()
        //{
        //    MemoryStream ms = new MemoryStream();
        //    object obj;
        //    try
        //    {
        //        BinaryFormatter bf = new BinaryFormatter();
        //        bf.Serialize(ms, this);
        //        ms.Seek(0, SeekOrigin.Begin);
        //        obj = bf.Deserialize(ms);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        ms.Close();
        //    }

        //    return obj;
        //}
    }
    public static class EnumEx
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                        as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
        public static T StrToEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }
        public static string EnumToStr<T>(this Enum value)
        {
            return Enum.GetName(typeof(T), value);
        }
    }
    #endregion

    /// <summary>
    /// 单据类型
    /// </summary>
    public enum VouchTypeEnum
    {
        /// <summary>
        /// 采购入库单
        /// </summary>
        [Description("采购入库单")]
        PurchaseInStock=1,
    }
    /// <summary>
    /// 计价方式
    /// </summary>
    public enum ValueType
    {
        /// <summary>
        /// 个别计价
        /// </summary>
        [Description("个别计价")]
        Separately = 0
    }

    /// <summary>
    /// 保质期单位
    /// </summary>
    public enum ShelfLifeType
    {
        /// <summary>
        /// 天
        /// </summary>
        [Description("天")]
        Day = 0,
        /// <summary>
        /// 周
        /// </summary>
        [Description("周")]
        Week = 1,
        /// <summary>
        /// 月
        /// </summary>
        [Description("月")]
        Month = 2,
        /// <summary>
        /// 年
        /// </summary>
        [Description("年")]
        Year = 3
    }

    /// <summary>
    /// 盘点周期单位
    /// </summary>
    public enum CheckCycle
    {
        /// <summary>
        /// 天
        /// </summary>
        [Description("天")]
        Day = 0,
        /// <summary>
        /// 周
        /// </summary>
        [Description("周")]
        Week = 1,
        /// <summary>
        /// 月
        /// </summary>
        [Description("月")]
        Month = 2
    }
    /// <summary>
    /// 收发类型
    /// </summary>
    public enum RdFlag
    {
        /// <summary>
        /// 收
        /// </summary>
        [Description("收")]
        Receive = 0,
        /// <summary>
        /// 发
        /// </summary>
        [Description("发")]
        Deliver = 1
    }

    /// <summary>
    /// 房间状态
    /// </summary>
    public enum RoomStatus
    {
        /// <summary>
        /// 不用
        /// </summary>
        [Description("不用")]
        NoUse = 0,
        /// <summary>
        /// 在用
        /// </summary>
        [Description("在用")]
        InUse = 1,
    }
    

    ///// <summary>
    ///// 库存管理，异常类型
    ///// </summary>
    //public enum BusinessExceptionType
    //{
    //    /// <summary>
    //    /// 采购入库单号重复
    //    /// </summary>
    //    [Description("采购入库单号重复")]
    //    CodeDup = 0,
    //    /// <summary>
    //    /// 已审核不能修改
    //    /// </summary>
    //    [Description("已审核不能修改")]
    //    IsVerify = 1,
    //    [Description("空记录")]
    //    IsNull = 2,
    //    [Description("删除已审核")]
    //    DeleteIsVerify = 3
    //}
    /// <summary>
    /// 存货类别类型
    /// </summary>
    public enum CategoryType
    {
        /// <summary>
        /// 冷饮店
        /// </summary>
        [Description("冷饮店")]
        ColdDrinkShop = 0,
        /// <summary>
        /// 西餐厅
        /// </summary>
        [Description("西餐厅")]
        WesternRestaurant = 1,
        /// <summary>
        /// 库存管理
        /// </summary>
        [Description("库存管理")]
        StockManage = 2
    }
    /// <summary>
    /// 库存管理，单据授权类型
    /// </summary>
    public enum AuthorityType
    {
        /// <summary>
        /// 所有
        /// </summary>
        [Description("所有")]
        All = 0,
        /// <summary>
        /// 部门
        /// </summary>
        [Description("部门")]
        Org = 1,
        /// <summary>
        /// 门店
        /// </summary>
        [Description("门店")]
        Dept = 2,
        /// <summary>
        /// 本人
        /// </summary>
        [Description("本人")]
        Self = 3,
    }
    /// <summary>
    /// 卡状态，使用id
    /// </summary>
    public enum CardStatus
    {
        /// <summary>
        /// 正常在用
        /// </summary>
        [Description("正常在用")]
        InUser = 0,
        /// <summary>
        /// 已挂失
        /// </summary>
        [Description("已挂失")]
        Losed = 1,
        /// <summary>
        /// 已补卡
        /// </summary>
        [Description("已补卡")]
        Added = 2,
        /// <summary>
        /// 已停用
        /// </summary>
        [Description("已停用")]
        Stoped = 3,
    }
    /// <summary>
    /// 小票类型，使用code
    /// </summary>
    public enum BillType
    {
        /// <summary>
        /// 会员卡消费
        /// </summary>
        [Description("会员卡消费")]
        CardConsumeWindow = 0,
        /// <summary>
        /// 打折卡消费
        /// </summary>
        [Description("打折卡消费")]
        CardConsume3Window = 1,
        /// <summary>
        /// 会员卡充值
        /// </summary>
        [Description("会员卡充值")]
        CardInMoneyWindow = 2,
        /// <summary>
        /// 非会员消费
        /// </summary>
        [Description("非会员消费")]
        NoMemberConsumeWindow = 3,
        /// <summary>
        /// 会员积分兑换
        /// </summary>
        [Description("会员积分兑换")]
        PointsExchangeWindow = 4,
        /// <summary>
        /// 西餐厅会员卡消费
        /// </summary>
        [Description("西餐厅会员卡消费")]
        WRCardConsumeWindow = 5,
        /// <summary>
        /// 西餐厅非会员消费
        /// </summary>
        [Description("西餐厅非会员消费")]
        WRNoMemberConsumeWindow = 6,
        /// <summary>
        /// 西餐厅打折卡消费
        /// </summary>
        [Description("西餐厅打折卡消费")]
        WRCardConsume3Window = 7,
        /// <summary>
        /// 不干胶
        /// </summary>
        [Description("不干胶")]
        Sticker=8,
        /// <summary>
        /// 会员卡消费撤销
        /// </summary>
        [Description("会员卡消费撤销")]
        CardCancelCheckOut = 9,
        /// <summary>
        /// 会员卡消费撤销
        /// </summary>
        [Description("打折卡消费撤销")]
        CardNoMoneyCancelCheckOut = 10,
        /// <summary>
        /// 会员卡消费撤销
        /// </summary>
        [Description("非会员消费撤销")]
        NoMemberCancelCheckOut = 11,
    }
    /// <summary>
    /// 预定桌台状态，使用id
    /// </summary>
    public enum OrderBookDeskStatus
    {
        /// <summary>
        /// 已预订
        /// </summary>
        [Description("已预订")]
        Booked = 0,
        /// <summary>
        /// 已撤销或已开台
        /// </summary>
        [Description("已撤销或已开台")]
        Canceled = 1,
    }
    /// <summary>
    /// 预定状态，使用id
    /// </summary>
    public enum OrderBookStatus
    {
        /// <summary>
        /// 已预订
        /// </summary>
        [Description("已预订")]
        Booked = 0,
        /// <summary>
        /// 已取消预订
        /// </summary>
        [Description("已取消预订")]
        Canceled = 1,
        /// <summary>
        /// 已开台
        /// </summary>
        [Description("已开台")]
        Opened = 2,
    }
    /// <summary>
    /// 杯子类型
    /// </summary>
    public enum CupType
    {
        /// <summary>
        /// 标准杯
        /// </summary>
        [Description("标准杯")]
        Standard = -1,
        /// <summary>
        /// 大杯
        /// </summary>
        [Description("大杯")]
        Big = 0,
        /// <summary>
        /// 中杯
        /// </summary>
        [Description("中杯")]
        Medium = 1,
        /// <summary>
        /// 小杯
        /// </summary>
        [Description("小杯")]
        Small = 2
    }
    /// <summary>
    /// 部门类型
    /// </summary>
    public enum SectionType
    {
        /// <summary>
        /// 后厨
        /// </summary>
        [Description("后厨")]
        Kitchen = 0,
        /// <summary>
        /// 吧台
        /// </summary>
        [Description("收银台")]
        Bar = 1,
        /// <summary>
        /// 前厅
        /// </summary>
        [Description("服务台")]
        Lobby = 2,

        /// <summary>
        /// 后厨2
        /// </summary>
        [Description("后厨2")]
        Houchu2 = 3,
        /// <summary>
        /// 凉菜
        /// </summary>
        CodeDish=4,

    }

    ///// <summary>
    ///// 部门类型
    ///// </summary>
    //public enum SectionType
    //{
    //    /// <summary>
    //    /// 后厨
    //    /// </summary>
    //    [Description("后厨")]
    //    Kitchen = 0,
    //    /// <summary>
    //    /// 吧台
    //    /// </summary>
    //    [Description("吧台")]
    //    Bar = 1,
    //    /// <summary>
    //    /// 其它
    //    /// </summary>
    //    [Description("其它")]
    //    Other = 2
    //}
    /// <summary>
    /// 部门-部门类型
    /// </summary>
    public enum DeptType
    {
        /// <summary>
        /// 零售型
        /// </summary>
        [Description("零售型")]
        Sale = 0,
        /// <summary>
        /// 坐店服务型
        /// </summary>
        [Description("坐店服务型")]
        Shop = 1,
    }
    /// <summary>
    /// 存货类型
    /// </summary>
    public enum InvType
    {
        /// <summary>
        /// 冷饮店
        /// </summary>
        [Description("冷饮店")]
        ColdDrinkShop = 0,
        /// <summary>
        /// 西餐厅
        /// </summary>
        [Description("西餐厅")]
        WesternRestaurant = 1,
        /// <summary>
        /// 库存管理
        /// </summary>
        [Description("库存管理")]
        StockManage = 2
    }
    
    /// <summary>
    ///订单状态 0开台 1结账 2撤销 3确认下单
    /// </summary>
    public enum OrderDishStatus
    {
        /// <summary>
        /// 已开台
        /// </summary>
        [Description("已开台")]
        Opened = 0,
        /// <summary>
        /// 已结账
        /// </summary>
        [Description("已结账")]
        Checkouted = 1,
        /// <summary>
        /// 已撤销
        /// </summary>
        [Description("已撤销")]
        Canceled = 2,
        /// <summary>
        /// 已下单
        /// </summary>
        [Description("已下单")]
        Ordered = 3
    }
    public enum OrderDeskStatus
    {
        /// <summary>
        /// 在用
        /// </summary>
        [Description("在用")]
        InUse = 0,
        /// <summary>
        /// 空闲
        /// </summary>
        [Description("空闲")]
        Idle = 1
    }
    /// <summary>
    /// 订单菜品状态
    /// </summary>
    public enum OrderMenuStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 0,
        /// <summary>
        /// 退菜
        /// </summary>
        [Description("退菜")]
        Withdraw = 1,
        /// <summary>
        /// 下单
        /// </summary>
        [Description("下单")]
        Order = 2,
        /// <summary>
        /// 缺菜
        /// </summary>
        [Description("缺菜")]
        Lack = 3,
        /// <summary>
        /// 催菜
        /// </summary>
        [Description("催菜")]
        Hurry = 4,
        /// <summary>
        /// 制作
        /// </summary>
        [Description("制作")]
        Make = 5,
        /// <summary>
        /// 出菜
        /// </summary>
        [Description("出菜")]
        Out = 6,
        /// <summary>
        /// 出菜后退菜
        /// </summary>
        [Description("出菜后退菜")]
        ReturnAfterOut = 7,
        /// <summary>
        /// 结账
        /// </summary>
        [Description("结账")]
        Checkout = 8,
    }
    /// <summary>
    /// 产品类型
    /// </summary>
    public enum ProductType
    {
        /// <summary>
        /// 原材料
        /// </summary>
        [Description("原材料")]
        Material = 0,
        /// <summary>
        /// 产成品
        /// </summary>
        [Description("产成品")]
        Product = 1
    }
    /// <summary>
    /// 充值类型
    /// </summary>
    public enum RechargeType
    {
        /// <summary>
        /// 普通充值
        /// </summary>
        [Description("普通充值")]
        CommonInMoney = 0,
        /// <summary>
        /// 补卡充值
        /// </summary>
        [Description("补卡充值")]
        CardAddInMoney = 1,
        /// <summary>
        /// 发卡充值
        /// </summary>
        [Description("发卡充值")]
        PutCardInMoney = 2,
        /// <summary>
        /// 补卡费
        /// </summary>
        [Description("补卡费")]
        CardAddConst = 3
    }
    /// <summary>
    /// 计量单位组类型
    /// </summary>
    public enum UnitGroupCategory
    {
        /// <summary>
        /// 无换算
        /// </summary>
        [Description("无换算")]
        No = 0,
        /// <summary>
        /// 固定
        /// </summary>
        [Description("固定换算")]
        Fixed = 1,
        /// <summary>
        /// 浮动
        /// </summary>
        [Description("浮动换算")]
        Float = 2
    }
    /// <summary>
    /// 计量单位类型
    /// </summary>
    public enum UOMType
    {
        /// <summary>
        /// 零售
        /// </summary>
        [Description("零售")]
        Retail = 0,
        /// <summary>
        /// 库存管理
        /// </summary>
        [Description("库存管理")]
        StockManage = 1
    }
    /// <summary>
    /// 后厨、吧台、前厅
    /// </summary>
    
    public enum ConsumeType
    {
        /// <summary>
        /// 会员卡消费
        /// </summary>
        [Description("会员卡消费")]
        Card=0,
        /// <summary>
        /// 非会员消费
        /// </summary>
        [Description("非会员消费")]
        NoMember=1,
        /// <summary>
        /// 会员积分兑换
        /// </summary>
        [Description("会员积分兑换")]
        Points=2,
        /// <summary>
        /// 打折卡消费
        /// </summary>
        [Description("打折卡消费")]
        CardNoMoney=3
    }
    public enum SourceType
    {
        /// <summary>
        /// 冷饮店
        /// </summary>
        [Description("冷饮店")]
        ColdDrinkShop = 0,
        /// <summary>
        /// 西餐厅
        /// </summary>
        [Description("西餐厅")]
        WesternRestaurant = 1,
    }
    /// <summary>
    /// 桌台状态
    /// </summary>
    public enum DeskStatus
    {
        /// <summary>
        /// 不用
        /// </summary>
        [Description("不用")]
        NoUse = 0,
        /// <summary>
        /// 在用
        /// </summary>
        [Description("在用")]
        InUse = 1,
    }
    /// <summary>
    /// IPad状态
    /// </summary>
    public enum IPadStatus
    {
        /// <summary>
        /// 不用
        /// </summary>
        [Description("不用")]
        NoUse = 0,
        /// <summary>
        /// 在用
        /// </summary>
        [Description("在用")]
        InUse = 1,
    }
    public enum DeskExStatus
    {
        /// <summary>
        /// 已开台
        /// </summary>
        [Description("已开台")]
        Opened = 0,
        /// <summary>
        /// 已预订
        /// </summary>
        [Description("已预订")]
        Booked = 1,
        /// <summary>
        /// 已预订
        /// </summary>
        [Description("空台")]
        Idle = 2,
        /// <summary>
        /// 已下单
        /// </summary>
        [Description("已下单")]
        Ordered = 3        
    }
    public enum PrintType
    {
        /// <summary>
        /// 加单
        /// </summary>
        [Description("加单")]
        Add = 0,
        /// <summary>
        /// 减单
        /// </summary>
        [Description("减单")]
        Sub = 1,
        /// <summary>
        /// 加菜
        /// </summary>
        [Description("加菜")]
        Menu = 2,
        /// <summary>
        /// 口味变化
        /// </summary>
        [Description("口味变化")]
        Taste = 3,
        /// <summary>
        /// 退单
        /// </summary>
        [Description("退单")]
        CancelOrder=4,
        /// <summary>
        /// 撤销
        /// </summary>
        [Description("撤销")]
        CancelOpen=5,
        /// <summary>
        /// 转
        /// </summary>
        [Description("转")]
        Exchange=6,
        /// <summary>
        /// 重打
        /// </summary>
        [Description("重打")]
        Repeat=7,
        /// <summary>
        /// 下单
        /// </summary>
        [Description("下单")]
        Order=8,
    }
    /// <summary>
    /// 参数表类型NameCode,Type
    /// </summary>
    public enum NameCodeType
    {
        /// <summary>
        /// 工本费
        /// </summary>
        [Description("工本费")]
        CostFee=1,
        /// <summary>
        /// 卡号规则
        /// </summary>
        [Description("卡号规则")]
        CardNoRule=2,
        // 微信号：6-20个字母、数字、下划线，必须以字母开头 
        //^[a-zA-Z][a-zA-Z0-9_]{5,19}$
        /// <summary>
        /// 中心标题
        /// </summary>
        [Description("中心标题")]
        DataCenterTitle=3,
        /// <summary>
        /// 中心登陆界面
        /// </summary>
        [Description("中心登陆界面")]
        LogonCss=4,
        /// <summary>
        /// 手机登陆用户
        /// </summary>
        [Description("手机登陆用户")]
        NoActiveXCheck=5,
        /// <summary>
        /// 会员打印小票标题
        /// </summary>
        [Description("会员打印小票标题")]
        PrintTicketTitleOfMember=6,
        /// <summary>
        /// 零售打印小票标题
        /// </summary>
        [Description("零售打印小票标题")]
        PrintTicketTitle1OfCold = 7,
        /// <summary>
        /// 餐厅打印小票标题
        /// </summary>
        [Description("餐厅打印小票标题")]
        PrintTicketTitle1OfWR = 8,
        /// <summary>
        /// 客户端标题
        /// </summary>
        [Description("客户端标题")]
        ClientSideTitle=9,
        /// <summary>
        /// 客户端启动画面
        /// </summary>
        [Description("客户端启动画面")]
        ClientSideSplashScreenImgPath= 10,
        /// <summary>
        /// 客户端登录窗口
        /// </summary>
        [Description("客户端登录窗口")]
        ClientSideLoginWin=11,
        /// <summary>
        /// 客户端背景
        /// </summary>
        [Description("客户端背景")]
        ClientSideBackgroundImgPath=12,
        /// <summary>
        /// 客户端是否动态菜单
        /// </summary>
        [Description("客户端是否动态菜单")]
        ClientSideRibbonMenu=13,
        /// <summary>
        /// 文件下载服务器地址
        /// </summary>
        [Description("文件下载服务器地址")]
        RemoteDownloadServer=14,
        /// <summary>
        /// 调拨单单价列是否显示
        /// </summary>
        [Description("调拨单单价列是否显示")]
        TransVouchPriceColumnVisible=15,
        /// <summary>
        /// 调拨单金额列是否显示
        /// </summary>
        [Description("调拨单金额列是否显示")]
        TransVouchAmountColumnVisible=16,
        /// <summary>
        /// 其它出库单单价列是否显示
        /// </summary>
        [Description("其它出库单单价列是否显示")]
        OtherOutStockPriceColumnVisible=17,
        /// <summary>
        /// 其它出库单金额列是否显示
        /// </summary>
        [Description("其它出库单金额列是否显示")]
        OtherOutStockAmountColumnVisible=18,
        /// <summary>
        /// 不合格品记录单单价列是否显示
        /// </summary>
        [Description("不合格品记录单单价列是否显示")]
        ScrapVouchPriceColumnVisible=19,
        /// <summary>
        /// 不合格品记录单金额列是否显示
        /// </summary>
        [Description("不合格品记录单金额列是否显示")]
        ScrapVouchAmountColumnVisible=20,
        /// <summary>
        /// 小票类型
        /// </summary>
        [Description("小票类型")]
        BillType = 21,
        /// <summary>
        /// 卡状态
        /// </summary>
        [Description("卡状态")]
        CardStatus = 22,
        /// <summary>
        /// 预定状态
        /// </summary>
        [Description("预定状态")]
        OrderBookStatus = 23,
        /// <summary>
        /// 预定桌台状态
        /// </summary>
        [Description("预定桌台状态")]
        OrderBookDeskStatus = 24,
        /// <summary>
        /// 杯型
        /// </summary>
        [Description("杯型")]
        CupType = 25,
        /// <summary>
        /// 订单状态
        /// </summary>
        [Description("订单状态")]
        OrderDishStatus = 26,
        /// <summary>
        /// 订单菜品状态
        /// </summary>
        [Description("订单菜品状态")]
        OrderMenuStatus = 27,

        /// <summary>
        /// 冷饮杯型-标准杯-是否显示
        /// </summary>
        [Description("冷饮杯型-标准杯-是否显示")]
        CupTypeStandardVisible = 28,
        /// <summary>
        /// 冷饮杯型-大杯-是否显示
        /// </summary>
        [Description("冷饮杯型-大杯-是否显示")]
        CupTypeBigVisible = 29,
        /// <summary>
        /// 冷饮杯型-中杯-是否显示
        /// </summary>
        [Description("冷饮杯型-中杯-是否显示")]
        CupTypeMediumVisible = 30,
        /// <summary>
        /// 冷饮杯型-小杯-是否显示
        /// </summary>
        [Description("冷饮杯型-小杯-是否显示")]
        CupTypeSmallVisible = 31,

        /// <summary>
        /// 冷饮杯型-标准杯-标题
        /// </summary>
        [Description("冷饮杯型-标准杯-标题")]
        CupTypeStandardTitle = 32,
        /// <summary>
        /// 冷饮杯型-大杯-标题
        /// </summary>
        [Description("冷饮杯型-大杯-标题")]
        CupTypeBigTitle = 33,
        /// <summary>
        /// 冷饮杯型-中杯-标题
        /// </summary>
        [Description("冷饮杯型-中杯-标题")]
        CupTypeMediumTitle = 34,
        /// <summary>
        /// 冷饮杯型-小杯-标题
        /// </summary>
        [Description("冷饮杯型-小杯-标题")]
        CupTypeSmallTitle = 35,
        /// <summary>
        /// 三联单打印落款
        /// </summary>
        [Description("三联单打印落款")]
        PrintTicketButtomTitleOfWR = 36,
        /// <summary>
        /// 三联单非会员打印文件名
        /// </summary>
        [Description("三联单非会员打印文件名")]
        ThreePrintNoMemmber = 37,
        /// <summary>
        /// 三联单会员打印文件名
        /// </summary>
        [Description("三联单打折卡打印文件名")]
        ThreePrintMemmberNoMoney = 38,
        /// <summary>
        /// 三联单会员打印文件名
        /// </summary>
        [Description("三联单会员打印文件名")]
        ThreePrintMemmber = 39,
        /// <summary>
        /// 是否录入会员
        /// </summary>
        [Description("是否录入会员")]
        SearchCard = 40,
        /// <summary>
        /// 是否显示杯型选择
        /// </summary>
        [Description("是否显示杯型选择")]
        IsCupType = 41,
        /// <summary>
        /// 是否显示条码
        /// </summary>
        [Description("是否显示条码")]
        Barcode = 42,
        /// <summary>
        /// 销售三联单非会员打印文件名
        /// </summary>
        [Description("销售三联单非会员打印文件名")]
        SaleThreePrintNoMemmber = 43,
        /// <summary>
        /// 销售三联单会员打印文件名
        /// </summary>
        [Description("销售三联单打折卡打印文件名")]
        SaleThreePrintMemmberNoMoney = 44,
        /// <summary>
        /// 销售三联单会员打印文件名
        /// </summary>
        [Description("销售三联单会员打印文件名")]
        SaleThreePrintMemmber = 45,
        /// <summary>
        /// 是否显示号牌
        /// </summary>
        [Description("是否显示号牌")]
        DeskNoVisibility = 46,
        /// <summary>
        /// 销售三联单落款
        /// </summary>
        [Description("销售三联单落款")]
        PrintTicketButtomTitle = 47,
        /// <summary>
        /// 是否显示杯型列
        /// </summary>
        [Description("是否显示杯型列")]
        CupTypeColumnVisibility = 48,
        /// <summary>
        /// 是否显示单位列
        /// </summary>
        [Description("是否显示单位列")]
        UnitOfMeasureColumnVisibility = 49,
        /// <summary>
        /// 门店类型
        /// </summary>
        [Description("门店类型")]
        DeptType = 50,
        /// <summary>
        /// 充值三联单打印文件名
        /// </summary>
        [Description("充值三联单打印文件名")]
        ThreePrintInMoney = 51,
        /// <summary>
        /// 充值三联单落款
        /// </summary>
        [Description("充值三联单落款")]
        ThreeButtomTitleInMoney = 52,
        /// <summary>
        /// 是否显示零售字段
        /// </summary>
        [Description("是否显示零售字段")]
        SaleColumnVisibility = 53,
        /// <summary>
        /// IPad销售存货档案类型
        /// </summary>
        [Description("IPad销售存货档案类型")]
        IPadInvType = 54,
        /// <summary>
        /// IPad销售存货分类类型
        /// </summary>
        [Description("IPad销售存货分类类型")]
        IPadCategoryType = 55,
        /// <summary>
        /// 是否显示存货单价选择
        /// </summary>
        [Description("是否显示存货单价选择")]
        IsInvPrice = 56,
        /// <summary>
        /// 是否启用保质期
        /// </summary>
        [Description("是否启用保质期")]
        IsShelfLife = 57,
        /// <summary>
        /// 是否启用批号
        /// </summary>
        [Description("是否启用批号")]
        IsBatch = 58,
        /// <summary>
        /// 是否启用货位
        /// </summary>
        [Description("是否启用货位")]
        IsLocator = 59,
        /// <summary>
        /// 是否显示ipad字段
        /// </summary>
        [Description("是否显示ipad字段")]
        IpadColumnVisibility = 60,
        /// <summary>
        /// 是否显示Jewelry字段
        /// </summary>
        [Description("是否显示Jewelry字段")]
        JewelryColumnVisibility = 61,
        /// <summary>
        /// 批号是否必须
        /// </summary>
        [Description("批号是否必须")]
        IsNecessaryBatch = 62,
        /// <summary>
        /// 是否自动同步零售数据入库存
        /// </summary>
        [Description("是否自动同步零售数据入库存")]
        IsSyncSaleStock=63,
        /// <summary>
        /// 零售是否显示图片
        /// </summary>
        [Description("零售是否显示图片")]
        ImageColumnVisibility = 64,
        /// <summary>
        /// 是否显示撤销结账
        /// </summary>
        [Description("是否显示撤销结账")]
        IsCancelCheckOut=65,
        /// <summary>
        /// 是否显示当班操作员
        /// </summary>
        [Description("是否显示当班操作员")]
        OperatorsOnDuty=66,
        /// <summary>
        /// 是否显示销售单位
        /// </summary>
        [Description("是否显示销售单位")]
        IsReceiver = 67,
        /// <summary>
        /// 是否显示销售折扣
        /// </summary>
        [Description("是否显示销售折扣")]
        IsSaleDiscount = 68,
        /// <summary>
        /// 位置类型
        /// </summary>
        [Description("位置类型")]
        SectionType = 69,
        /// <summary>
        /// 结算类型
        /// </summary>
        [Description("结算类型")]
        BalanceType=70,
        /// <summary>
        /// 支付类型
        /// </summary>
        [Description("支付类型")]
        PayType=71,
        /// <summary>
        /// 显示图片
        /// </summary>
        [Description("显示图片")]
        IsDisplayImage = 72,
        /// <summary>
        /// 充值类型
        /// </summary>
        [Description("充值类型")]
        RechargeType=73,
        /// <summary>
        /// 消费类型
        /// </summary>
        [Description("消费类型")]
        ConsumeType=74,
        /// <summary>
        /// 单据状态
        /// </summary>
        [Description("单据状态")]
        ReceiptStatus=75,
        /// <summary>
        /// 卡级别是否自动递增
        /// </summary>
        [Description("卡级别是否自动递增")]
        IsCardLevelAuto = 76,
        /// <summary>
        /// 是否显示代金券
        /// </summary>
        [Description("是否显示代金券")]
        VoucherVisibility = 77,
        /// <summary>
        /// 是否显示支付方式
        /// </summary>
        [Description("是否显示支付方式")]
        PayTypeVisibility = 78,
        /// <summary>
        /// 是否显示刷卡
        /// </summary>
        [Description("是否显示刷卡")]
        CardVisibility = 79,
        /// <summary>
        /// 是否显示存货单价动态设置
        /// </summary>
        [Description("是否显示存货单价动态设置")]
        IsInvDynamicPrice = 80,
    }
    /// <summary>
    /// 单据状态
    /// </summary>
    public enum ReceiptStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 0,
        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        Complete = 1,
    }
    /// <summary>
    /// 单据类型
    /// </summary>
    public enum ReceiptType
    {
        /// <summary>
        /// 订货单
        /// </summary>
        [Description("订货单")]
        Order = 0,
        /// <summary>
        /// 返修单
        /// </summary>
        [Description("返修单")]
        Rework = 1,
    }

    public class MemberType
    {
        //null为发卡会员
        public readonly static Guid Receipt = Guid.Parse("4277B463-0892-4131-B1A5-E270B5360278");
    }

    /// <summary>
    /// 供应商类型
    /// </summary>
    public enum VendorType
    {
        /// <summary>
        /// 供货单位
        /// </summary>
        [Description("供货单位")]
        Supplier = 0,
        /// <summary>
        /// 收货单位
        /// </summary>
        [Description("收货单位")]
        Receiver = 1,
    }
    /// <summary>
    /// 支付方式类型
    /// </summary>
    public enum PayType
    {
        /// <summary>
        /// 现金
        /// </summary>
        [Description("现金")]
        Cash = 0,
        /// <summary>
        /// 银行卡
        /// </summary>
        [Description("银行卡")]
        Bank = 1,
        /// <summary>
        /// 代金券
        /// </summary>
        [Description("代金券")]
        Voucher=2,
        /// <summary>
        /// 其它
        /// </summary>
        [Description("其它")]
        Other = 99,
    }
    /// <summary>
    /// tbCommCode类型
    /// </summary>
    public enum CommSign
    {
        /// <summary>
        /// 商品类型
        /// </summary>
        [Description("商品类型")]
        GT=0,
        /// <summary>
        /// 网站查看权限
        /// </summary>
        [Description("网站查看权限")]
        CLT=1,
        /// <summary>
        /// 门店
        /// </summary>
        [Description("门店")]
        MD=2,
        /// <summary>
        /// 操作员权限
        /// </summary>
        [Description("操作员权限")]
        LM=3,
        /// <summary>
        /// 会员状态
        /// </summary>
        [Description("会员状态")]
        AS=4,
        /// <summary>
        /// 会员类型
        /// </summary>
        [Description("会员类型")]
        AT=5,
        /// <summary>
        /// 付费类型
        /// </summary>
        [Description("付费类型")]
        PT=6,
        /// <summary>
        /// 操作类型
        /// </summary>
        [Description("操作类型")]
        OP=7,
    }
}
