using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.Collections.ObjectModel;
using FairiesCoolerCash.Business;
using System.Windows;
using DXInfo.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Specialized;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Data;
using AutoMapper;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Net;

namespace FairiesCoolerCash.ViewModel
{
    public class StockConsumeViewModel : CardConsumeViewModel
    {        
        public StockConsumeViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }

        public override void SetCurrentType()
        {
            this.CurrentCategoryType = CategoryType.StockManage;
            this.CurrentInvType = InvType.StockManage;
            this.DeptType = DeptType.Sale;
            this.IsStock = true;
        }
        
        //public override void AfterCheckOut()
        //{
        //    base.AfterCheckOut();
        //    AddRetailOutStock();
        //}
    }
    public class CardConsumeViewModel : ConsumeViewModelBase
    {
        public Visibility ImageColumnVisibility { get; set; }
        
        #region 构造
        public CardConsumeViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
            this.IsCupType = BusinessCommon.IsCupType();
            bool imageColumnVisibility = BusinessCommon.ImageColumnVisibility();
            if (imageColumnVisibility)
            {
                this.ImageColumnVisibility = Visibility.Visible;
            }
            else
            {
                this.ImageColumnVisibility = Visibility.Collapsed;
            }            
        }
        public override void SetCurrentType()
        {
            this.CurrentCategoryType = CategoryType.ColdDrinkShop;
            this.CurrentInvType = InvType.ColdDrinkShop;
            this.DeptType = DeptType.Sale;
        }
        #endregion

        #region 存货与分类 
        protected override void CreateInventoryEx(DXInfo.Models.Inventory inv)
        {
            //if (this.SelectedInventory != null)
            //{
            InventoryEx inventoryEx = Mapper.Map<DXInfo.Models.InventoryEx>(inv);
            inventoryEx.IsCupType = this.IsCupType;
            inventoryEx.IsInvPrice = this.IsInvPrice;
            inventoryEx.lTasteEx = this.lTasteEx.Clone() as DXInfo.Models.TasteExList;
            inventoryEx.Quantity = 1;
            inventoryEx.IsDiscount = JudgeIsDiscount(inv.Id);

            inventoryEx.IsInvDynamicPrice = this.IsInvDynamicPrice;
            inventoryEx.Discount = 100;
            inventoryEx.AgreementPrice = inventoryEx.SalePrice;
            this.SetCupType(inventoryEx);
            this.SetInvPrice(inventoryEx);
            if (inventoryEx.IsDiscount)
            {
                this.SetInvDynamicPrice(inventoryEx);
            }
            //this.SetCurrentStock(inventoryEx);
            this.SetOCInventoryEx();
            this.OCInventoryEx.Add(inventoryEx);
            //}
        }
        protected override void AfterSelectInventory()
        {
            base.AfterSelectInventory();
            if (this.IsCancelCheckOut &&
                        this.CancelConsume != null &&
                        this.lCancelConsumeList != null &&
                        this.lCancelConsumeList.Count > 0&&
                this.OCInventoryEx != null &&
                        this.OCInventoryEx.Count > 0)
            {
                Helper.ShowSuccMsg("结账撤销不能选择");
                return;
            }
            if (this.IsCancelCheckOut)
            {
                ResetCancelCheckOut();
            }
            if (this.SelectedInventory != null)
            {
                CreateInventoryEx(this.SelectedInventory);
            }
        }
        protected override void AfterSelectInventoryEx()
        {
            if (this.IsCupType)
            {
                if (SelectedInventoryEx != null)
                {
                    if (this.MyDataGrid.CurrentColumn != null && this.MyDataGrid.CurrentColumn.Header != null)
                    {
                        if (!(this.MyDataGrid.CurrentColumn.Header.ToString() == "数量" || this.MyDataGrid.CurrentColumn.Header.ToString() == "撤销"))
                        {
                            CardConsumeSetWindow csw = new CardConsumeSetWindow(Uow, SelectedInventoryEx);
                            csw.ShowDialog();
                        }
                    }
                }
            }
        }
        #endregion
        
        #region 删除记录
        private void DeleteSelectedInventoryExExecute()
        {
            if (this.SelectedInventoryEx != null && this.OCInventoryEx != null)
            {
                this.OCInventoryEx.Remove(this.SelectedInventoryEx);
            }
        }
        public ICommand DeleteSelectedInventoryEx
        {
            get
            {
                return new RelayCommand(DeleteSelectedInventoryExExecute);
            }
        }
        #endregion       
    }
}
