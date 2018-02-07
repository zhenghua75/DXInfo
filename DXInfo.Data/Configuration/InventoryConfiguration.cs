//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18052
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DXInfo.Data.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data.Entity.ModelConfiguration;
    using DXInfo.Models;
    using System.ComponentModel.DataAnnotations.Schema;
    
    
    public class InventoryConfiguration : EntityTypeConfiguration<Inventory>
    {
        
        public InventoryConfiguration()
        {
            this.HasKey(k => k.Id);
            this.Property(o => o.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(o => o.Id).IsRequired();
            this.Property(o => o.Category).IsRequired();
            this.Property(o => o.Code).IsRequired();
            this.Property(o => o.Code).IsUnicode();
            this.Property(o => o.Code).HasMaxLength(50);
            this.Property(o => o.Name).IsRequired();
            this.Property(o => o.Name).IsUnicode();
            this.Property(o => o.Name).HasMaxLength(50);
            this.Property(o => o.Specs).IsUnicode();
            this.Property(o => o.Specs).HasMaxLength(50);
            this.Property(o => o.Comment).IsUnicode();
            this.Property(o => o.Comment).HasMaxLength(50);
            this.Property(o => o.ImageFileName).IsUnicode();
            this.Property(o => o.ImageFileName).HasMaxLength(200);
            this.Property(o => o.SalePrice).HasPrecision(24,2);
            this.Property(o => o.SalePrice).IsRequired();
            this.Property(o => o.SalePrice0).HasPrecision(24,2);
            this.Property(o => o.SalePrice0).IsRequired();
            this.Property(o => o.SalePrice1).HasPrecision(24,2);
            this.Property(o => o.SalePrice1).IsRequired();
            this.Property(o => o.SalePrice2).HasPrecision(24,2);
            this.Property(o => o.SalePrice2).IsRequired();
            this.Property(o => o.SalePoint).HasPrecision(24,2);
            this.Property(o => o.SalePoint).IsRequired();
            this.Property(o => o.SalePoint0).HasPrecision(24,2);
            this.Property(o => o.SalePoint0).IsRequired();
            this.Property(o => o.SalePoint1).HasPrecision(24,2);
            this.Property(o => o.SalePoint1).IsRequired();
            this.Property(o => o.SalePoint2).HasPrecision(24,2);
            this.Property(o => o.SalePoint2).IsRequired();
            this.Property(o => o.IsDonate).IsRequired();
            this.Property(o => o.Stars).IsRequired();
            this.Property(o => o.Feature).IsUnicode();
            this.Property(o => o.Feature).HasMaxLength(2000);
            this.Property(o => o.Dosage).IsUnicode();
            this.Property(o => o.Dosage).HasMaxLength(2000);
            this.Property(o => o.Palette).IsUnicode();
            this.Property(o => o.Palette).HasMaxLength(2000);
            this.Property(o => o.EnglishName).IsUnicode();
            this.Property(o => o.EnglishName).HasMaxLength(200);
            this.Property(o => o.IsRecommend).IsRequired();
            this.Property(o => o.Printer).IsUnicode();
            this.Property(o => o.Printer).HasMaxLength(200);
            this.Property(o => o.EnglishIntroduce).IsUnicode();
            this.Property(o => o.EnglishIntroduce).HasMaxLength(2000);
            this.Property(o => o.EnglishDosage).IsUnicode();
            this.Property(o => o.EnglishDosage).HasMaxLength(2000);
            this.Property(o => o.InvType).IsRequired();
            this.Property(o => o.IsPackage).IsRequired();
            this.Property(o => o.IsInvalid).IsRequired();
            this.Property(o => o.Sort).IsRequired();
            this.Property(o => o.MeasurementUnitGroup).IsRequired();
            this.Property(o => o.MainUnit).IsRequired();
            this.Property(o => o.UnitCategory).IsRequired();
            this.Property(o => o.ValueType).IsRequired();
            this.Property(o => o.HighStock).HasPrecision(24,9);
            this.Property(o => o.HighStock).IsRequired();
            this.Property(o => o.LowStock).HasPrecision(24,9);
            this.Property(o => o.LowStock).IsRequired();
            this.Property(o => o.SecurityStock).HasPrecision(24,9);
            this.Property(o => o.SecurityStock).IsRequired();
            this.Property(o => o.Locator).IsRequired();
            this.Property(o => o.CheckCycle).IsRequired();
            this.Property(o => o.SomeDay).IsRequired();
            this.Property(o => o.IsShelfLife).IsRequired();
            this.Property(o => o.ShelfLife).IsRequired();
            this.Property(o => o.EarlyWarningDay).IsRequired();
            this.Property(o => o.IsBatch).IsRequired();
            this.Property(o => o.ShelfLifeType).IsRequired();
            this.Property(o => o.IsSale).IsRequired();

            this.Property(o => o.Karat).HasPrecision(24, 2);
            this.Property(o => o.Karat).IsRequired();
            this.Property(o => o.MetalWeight).HasPrecision(24, 2);
            this.Property(o => o.MetalWeight).IsRequired();
            this.Property(o => o.JewelWeight).HasPrecision(24, 2);
            this.Property(o => o.JewelWeight).IsRequired();
            this.Property(o => o.TotalWeight).HasPrecision(24, 2);
            this.Property(o => o.TotalWeight).IsRequired();
            this.Property(o => o.QTY).HasPrecision(24, 2);
            this.Property(o => o.QTY).IsRequired();

            this.Property(o => o.OrderNo).HasMaxLength(200);
            this.Property(o => o.VendorSpec).HasMaxLength(200);
            this.Property(o => o.Length).HasMaxLength(200);
        }
    }
}
