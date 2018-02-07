using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ynhnTransportManage.Models
{
    public class CustomeProfileModels
    {
        [DisplayName("姓名")]
        [DataType(DataType.Text)]
        public string FullName { get; set; }

        [DisplayName("部门ID")]
        [DataType(DataType.Text)]
        public Guid DeptId { get; set; }

        [DisplayName("部门编码")]
        [DataType(DataType.Text)]
        public Guid DeptCode { get; set; }

        [DisplayName("部门名称")]
        [DataType(DataType.Text)]
        public Guid DeptName { get; set; }
    }
}