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
    
    
    public class tbFormulaConfiguration : EntityTypeConfiguration<tbFormula>
    {
        
        public tbFormulaConfiguration()
        {
            this.HasKey(k => k.cnvcProductCode);
            this.Property(o => o.cnvcProductCode).IsRequired();
            this.Property(o => o.cnvcProductCode).HasMaxLength(20);
            this.Property(o => o.cnvcProductName).HasMaxLength(20);
            this.Property(o => o.cnvcProductType).HasMaxLength(20);
            this.Property(o => o.cnvcProductClass).HasMaxLength(20);
            this.Property(o => o.cnnMaterialCostSum).HasPrecision(10,4);
            this.Property(o => o.cnnPackingCostSum).HasPrecision(10,4);
            this.Property(o => o.cnnCostSum).HasPrecision(10,4);
            this.Property(o => o.cnvcUnit).HasMaxLength(20);
            this.Property(o => o.cnnPortionCount).HasPrecision(10,2);
            this.Property(o => o.cnvcPortionUnit).HasMaxLength(20);
        }
    }
}