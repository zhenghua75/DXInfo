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
    
    
    public class aspnet_MembershipConfiguration : EntityTypeConfiguration<aspnet_Membership>
    {
        
        public aspnet_MembershipConfiguration()
        {
            this.HasKey(k => k.UserId);
            this.Property(o => o.ApplicationId).IsRequired();
            this.Property(o => o.UserId).IsRequired();
            this.Property(o => o.Password).IsRequired();
            this.Property(o => o.Password).IsUnicode();
            this.Property(o => o.Password).HasMaxLength(128);
            this.Property(o => o.PasswordFormat).IsRequired();
            this.Property(o => o.PasswordSalt).IsRequired();
            this.Property(o => o.PasswordSalt).IsUnicode();
            this.Property(o => o.PasswordSalt).HasMaxLength(128);
            this.Property(o => o.MobilePIN).IsUnicode();
            this.Property(o => o.MobilePIN).HasMaxLength(16);
            this.Property(o => o.Email).IsUnicode();
            this.Property(o => o.Email).HasMaxLength(256);
            this.Property(o => o.LoweredEmail).IsUnicode();
            this.Property(o => o.LoweredEmail).HasMaxLength(256);
            this.Property(o => o.PasswordQuestion).IsUnicode();
            this.Property(o => o.PasswordQuestion).HasMaxLength(256);
            this.Property(o => o.PasswordAnswer).IsUnicode();
            this.Property(o => o.PasswordAnswer).HasMaxLength(128);
            this.Property(o => o.IsApproved).IsRequired();
            this.Property(o => o.IsLockedOut).IsRequired();
            this.Property(o => o.CreateDate).IsRequired();
            this.Property(o => o.LastLoginDate).IsRequired();
            this.Property(o => o.LastPasswordChangedDate).IsRequired();
            this.Property(o => o.LastLockoutDate).IsRequired();
            this.Property(o => o.FailedPasswordAttemptCount).IsRequired();
            this.Property(o => o.FailedPasswordAttemptWindowStart).IsRequired();
            this.Property(o => o.FailedPasswordAnswerAttemptCount).IsRequired();
            this.Property(o => o.FailedPasswordAnswerAttemptWindowStart).IsRequired();
            this.Property(o => o.Comment).HasMaxLength(1073741823);
        }
    }
}
