using Core.Common.Mapper;
using System.ComponentModel;


namespace BaseProject.Admin.ViewModels.Code
{
    public class Detail : IMapFrom<BaseProject.Model.Code>
    {
        public int SN { get; set; }

        [DisplayName("代碼主檔名稱")]
        public string MainID { get; set; }

        [DisplayName("代碼值")]
        public string Value { get; set; }

        [DisplayName("代碼顯示值")]
        public string Display { get; set; }

        [DisplayName("順序")]
        public int OrderBy { get; set; }

        public virtual BaseProject.Admin.ViewModels.CodeMain.Detail CodeMain { get; set; }
    }
}