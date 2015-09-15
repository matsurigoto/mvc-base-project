using System.Collections.Generic;
using Core.Common.Mapper;
using Core.SelectListFill;
using BaseProject.SelectListFill;
using System.ComponentModel;

namespace BaseProject.Admin.ViewModels.Code
{
    public class Edit : Core.Base.CoreViewModelBase, IHaveCustomMapping
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

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Edit, BaseProject.Model.Code>();
            configuration.CreateMap<BaseProject.Model.Code, Edit>();
        }

        public override Core.SelectListFill.SelectListViewModel[] NeedFillSelectList
        {
            get
            {
                List<SelectListViewModel> temp = new List<SelectListViewModel>();

                temp.Add(SelectListViewModelHelper.GetSelectListViewModelForCodeMain
                        <Edit>(x => x.MainID, () => MainID));

                return temp.ToArray();
            }
        }
    }
}