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
    
    
    public class VehiclesConfiguration : EntityTypeConfiguration<Vehicles>
    {
        
        public VehiclesConfiguration()
        {
            this.HasKey(k => k.Id);
            this.Property(o => o.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(o => o.Id).IsRequired();
            this.Property(o => o.PlateNo).IsRequired();
            this.Property(o => o.PlateNo).IsUnicode();
            this.Property(o => o.PlateNo).HasMaxLength(50);
            this.Property(o => o.BrandModel).IsUnicode();
            this.Property(o => o.BrandModel).HasMaxLength(50);
            this.Property(o => o.MotorNo).IsUnicode();
            this.Property(o => o.MotorNo).HasMaxLength(50);
            this.Property(o => o.Comment).IsUnicode();
            this.Property(o => o.Comment).HasMaxLength(50);
            this.Property(o => o.OwnerName).IsUnicode();
            this.Property(o => o.OwnerName).HasMaxLength(200);
        }
    }
}
