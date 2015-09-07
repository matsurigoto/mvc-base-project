using Core.Common.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BaseProject.Admin.ViewModels.CodeMain
{
    /// <summary>
    /// CodeMain Create ViewModel
    /// </summary>
    public class Create : Core.Base.CoreViewModelBase, IHaveCustomMapping
    {
        [DisplayName("代碼主檔Id")]
        public string ID { get; set; }
        [DisplayName("代碼主檔名稱")]
        public string Name { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Create, Admin.CodeMain>()
                .ForMember(m => m.CreateDate, opt => opt.UseValue(DateTime.Now))
                .ForMember(m => m.CreateBy, opt => opt.UseValue("Admin")); 
            // TODO: change to real user once authorisation is done.
        }
    }
}