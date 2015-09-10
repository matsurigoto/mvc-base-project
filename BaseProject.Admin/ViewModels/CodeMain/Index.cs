using Core.Base;
using Core.Common.Mapper;
using System.ComponentModel;


namespace BaseProject.Admin.ViewModels.CodeMain
{
    public class Index : SearchViewModelBase<SearchFormViewModel, SearchResult>
    {
    }

    public class SearchResult : IMapFrom<Model.CodeMain>
    {
        [DisplayName("代碼主檔Id")]
        public string ID { get; set; }

        [DisplayName("代碼主檔名稱")]
        public string Name { get; set; }
    }

    public class SearchFormViewModel : SearchFormViewModelBase<Model.CodeMain>
    {
        [DisplayName("代碼主檔Id")]
        public string ID { get; set; }

        [DisplayName("代碼主檔名稱")]
        public string Name { get; set; }
    }
}