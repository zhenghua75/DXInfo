using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using DXInfo.Data.Contracts;
using FairiesCoolerCash.Business;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Threading;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using DXInfo.Models;
using System.Net;
using System.ComponentModel;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using AutoMapper;

namespace FairiesCoolerCash.ViewModel
{
    public class BusinessViewModelBase : MyViewModelBase
    {
        private readonly IMapper mapper;
        public BusinessViewModelBase(IFairiesMemberManageUow uow, IMapper mapper, List<string> lValidationPropertyNames)
            : base(uow,mapper, lValidationPropertyNames)
        {
            this.mapper = mapper;
        }
    }
}
