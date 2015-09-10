using Core.Common.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BaseProject.Admin.ViewModels.CodeMain
{
    public class Detail : IMapFrom<Model.CodeMain>
    {
        [DisplayName("代碼主檔Id")]
        public string ID { get; set; }

        [DisplayName("代碼主檔名稱")]
        public string Name { get; set; }

        [DisplayName("建立人員")]
        public string CreateBy { get; set; }

        [DisplayName("建立時間")]
        public System.DateTime CreateDate { get; set; }

        [DisplayName("最後修改人")]
        public string LastModifyBy { get; set; }

        [DisplayName("最後修改時間")]
        public Nullable<System.DateTime> LastModifyDate { get; set; }
    }
}