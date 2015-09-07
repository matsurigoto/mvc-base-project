using Core.Common.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BaseProject.Admin.ViewModels.CodeMain
{
    /// <summary>
    /// CodeMain Edit ViewModel
    /// </summary>
    public class Edit : Core.Base.CoreViewModelBase,  IHaveCustomMapping
    {
        public string ID { get; set; }

        [DisplayName("代碼主檔名稱")]
        public string Name { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Edit, Admin.CodeMain>()
                .ForMember(m => m.LastModifyDate, opt => opt.UseValue(DateTime.Now))
                .ForMember(m => m.LastModifyBy, opt => opt.UseValue("Admin")); // TODO: change to real user once authorisation is done.

            configuration.CreateMap<Admin.CodeMain, Edit>();
        }
    }
}