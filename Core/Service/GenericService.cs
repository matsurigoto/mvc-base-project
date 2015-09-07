using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Core.Common.Business;
using Core.Common.Repository;
using Core.Common.Service;
using Core.Mapper;
using Core.Utility.Paging;
using Core.Utility.Paging.Extension;
using Core.Utility.ValidationError;
using Core.Utility.ValidationError.Exception;

namespace Core.Service
{
    /// <summary>
    /// 通用行的Service layer實作
    /// </summary>
    /// <typeparam name="T">主要的Entity形態</typeparam>
    public class GenericService<T> : IService<T>
        where T : class
    {
        /// <summary>
        /// 代表DB的UoW
        /// </summary>
        protected IUnitOfWork db;

        /// <summary>
        /// 取得驗證資訊的字典
        /// </summary>
        /// <value>
        /// 驗證資訊的字典
        /// </value>
        public Common.ValidationError.IValidationDictionary ValidationDictionary { get; private set; }

        /// <summary>
        /// 處理上傳的檔案邏輯
        /// </summary>
        //protected IHttpFileProcessBusiness httpFileProcess;

        /// <summary>
        /// 處理刪除多對多關係的邏輯
        /// </summary>
        protected IDeleteManyToManyProcess deleteProcess;

        /// <summary>
        /// 初始化 <see cref="GenericService{T}"/> 的執行個體.
        /// </summary>
        /// <param name="inDb">UoW代表DB的實例</param>
        /// <param name="inHttpFileProcess">處理上傳檔案的實例</param>
        /// <param name="inDeleteProcess">處理刪除的實例</param>
        public GenericService(IUnitOfWork inDb,
            //IHttpFileProcessBusiness inHttpFileProcess,
            IDeleteManyToManyProcess inDeleteProcess)
        {
            db = inDb;
            //httpFileProcess = inHttpFileProcess;
            deleteProcess = inDeleteProcess;
        }

        /// <summary>
        /// 初始化IValidationDictionary
        /// </summary>
        /// <param name="inValidationDictionary">要用來儲存錯誤訊息的object</param>
        public void InitialiseIValidationDictionary(Common.ValidationError.IValidationDictionary inValidationDictionary)
        {
            ValidationDictionary = inValidationDictionary;
        }

        /// <summary>
        /// 處理在Index ViewModel所需要做的事情
        /// </summary>
        /// <typeparam name="TSearchForm">搜索form的形態</typeparam>
        /// <typeparam name="TPageResult">搜索結果的形態</typeparam>
        /// <param name="searchViewModel">搜索相關的viewmodel</param>
        /// <param name="wherePredicate">當不用預設的where處理邏輯的時候，傳入的自訂where條件</param>
        /// <param name="includes">需要Include進來的其他Entity</param>
        public virtual void ProcessIndexViewModel<TSearchForm, TPageResult>(Common.Base.ISearchViewModelBase<TSearchForm, TPageResult> searchViewModel,
            System.Linq.Expressions.Expression<Func<T, bool>> wherePredicate = null,
            params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
            where TSearchForm : Common.Base.ISearchFormViewModelBase, new()
        {
            var data = db.Repository<T>().Reads();

            foreach (var item in includes)
            {
                data.Include(item);
            }

            // 如果沒有where條件，表示依照預設邏輯
            if (wherePredicate == null)
            {
                // 當沒有給filter條件並且搜索條件屬於前台看的
                if (searchViewModel.SearchForm is IFrontViewModel)
                {
                    SearchViewModelProcess.ApplySearchFormForFront<T, TSearchForm, TPageResult>(data,
                        searchViewModel);
                }
                else
                {
                    SearchViewModelProcess.ApplySearchForm<T, TSearchForm, TPageResult>(data,
                        searchViewModel);
                }
            }
            else
            {
                SearchViewModelProcess.ApplySearchForm<T, TSearchForm, TPageResult>(data,
                    searchViewModel, wherePredicate);
            }
        }

        /// <summary>
        /// 用IndexViewModel產生出要匯出的內容
        /// </summary>
        /// <typeparam name="TSearchForm">搜索form的形態</typeparam>
        /// <typeparam name="TPageResult">搜索結果的形態</typeparam>
        /// <param name="searchViewModel">搜索相關的viewmodel</param>
        /// <param name="wherePredicate">傳入要匯出的資料條件</param>
        /// <param name="includes">需要Include進來的其他Entity</param>
        public virtual void ProcessExportViewModel<TSearchForm, TPageResult>(Common.Base.ISearchViewModelBase<TSearchForm, TPageResult> searchViewModel,
            System.Linq.Expressions.Expression<Func<T, bool>> wherePredicate = null,
            params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
            where TSearchForm : Common.Base.ISearchFormViewModelBase, new()
        {

            var data = db.Repository<T>().Reads();

            foreach (var item in includes)
            {
                data.Include(item);
            }

            SearchViewModelProcess.ApplySearchForm<T, TSearchForm, TPageResult>(data,
                    searchViewModel, wherePredicate);
        }

        /// <summary>
        /// 取得某一個條件下面的某一筆Entity
        /// </summary>
        /// <param name="wherePredicate">過濾邏輯</param>
        /// <param name="includes">需要Include的Entity</param>
        /// <returns>取得Entity或者是null</returns>
        public virtual T GetSpecificDetail(System.Linq.Expressions.Expression<Func<T, bool>> wherePredicate,
            params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            var data = ApplyIncludeAndGetIQueryable(includes);

            return data.Where(wherePredicate).FirstOrDefault();
        }

        /// <summary>
        /// 取得IQueryable同時加上Include的Entity
        /// </summary>
        /// <param name="includes">要Include進來的Entity</param>
        /// <returns>加過Include的IQueryable</returns>
        private IQueryable<T> ApplyIncludeAndGetIQueryable(System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            var data = db.Repository<T>().Reads();

            foreach (var item in includes)
            {
                data.Include(item);
            }

            return data;
        }

        /// <summary>
        /// 取得某一個條件下面的某一筆Entity並且轉成對應的ViewModel
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="wherePredicate">過濾邏輯</param>
        /// <param name="includes">需要Include的Entity</param>
        /// <returns>取得轉換過的ViewModel或者是null</returns>
        public virtual TViewModel GetSpecificDetailToViewModel<TViewModel>(System.Linq.Expressions.Expression<Func<T, bool>> wherePredicate,
            params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            return GetSpecificDetail(wherePredicate, includes).ToModel<TViewModel>();
        }

        /// <summary>
        /// 取得某一個條件下面的某一筆Entity並且確認符合前台顯示的條件（IsEnable，上下架時間）。
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="wherePredicate">過濾邏輯</param>
        /// <param name="includes">需要Include的Entity</param>
        /// <returns>取得轉換過的ViewModel或者是null</returns>
        public virtual TViewModel GetSpecificDetailToViewModelForFront<TViewModel>(System.Linq.Expressions.Expression<Func<T, bool>> wherePredicate,
            params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            var data = ApplyIncludeAndGetIQueryable(includes);

            data = data.WhereForFrontDisplay();

            data = data.Where(wherePredicate);

            return data.FirstOrDefault().ToModel<TViewModel>();
        }

        /// <summary>
        /// 依照某一個ViewModel的值，更新對應的Entity
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="viewModel">ViewModel的值</param>
        /// <param name="wherePredicate">過濾條件 - 要被更新的那一筆過濾調價</param>
        /// <returns>是否刪除成功</returns>
        public virtual bool UpdateViewModelToDatabase<TViewModel>(TViewModel viewModel, System.Linq.Expressions.Expression<Func<T, bool>> wherePredicate)
        {
            if (ValidationDictionary.IsValid)
            {
                //httpFileProcess.SaveHttpPostFile(viewModel, Core.Utility.Config.DoPath);

                var entity = db.Repository<T>().Read(wherePredicate);

                AutoMapper.Mapper.Map(viewModel, entity);

                db.Repository<T>().Update(entity);

                SaveChange();
            }

            return ValidationDictionary.IsValid;
        }

        /// <summary>
        /// 依照某一個ViewModel的值，更新對應的Entity:客製路徑
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="viewModel">ViewModel的值</param>
        /// <param name="wherePredicate">過濾條件 - 要被更新的那一筆過濾調價</param>
        /// <returns>是否刪除成功</returns>
        public virtual bool UpdateViewModelToDatabase<TViewModel>(TViewModel viewModel, System.Linq.Expressions.Expression<Func<T, bool>> wherePredicate, string path)
        {
            if (ValidationDictionary.IsValid)
            {
                //httpFileProcess.SaveHttpPostFile(viewModel, Core.Utility.Config.DoPath);

                var entity = db.Repository<T>().Read(wherePredicate);

                AutoMapper.Mapper.Map(viewModel, entity);

                db.Repository<T>().Update(entity);

                SaveChange();
            }

            return ValidationDictionary.IsValid;
        }

        /// <summary>
        /// 刪除某一筆Entity，包含ManyToMany的relation的實際另一端Entity
        /// </summary>
        /// <typeparam name="TManyToMany">Many to many另外一端的形態</typeparam>
        /// <param name="wherePredicate">取得某一筆要被刪除的過濾條件</param>
        /// <param name="includes">Include進來的其他Entity</param>
        /// <returns>是否刪除成功</returns>
        public virtual bool DeleteWithManyToMany<TManyToMany>(System.Linq.Expressions.Expression<Func<T, bool>> wherePredicate,
            params Expression<Func<T, object>>[] includes)
            where TManyToMany : class
        {
            return DeleteForManyToMany(wherePredicate, includes, (x => deleteProcess.DeleteIncludeManyToManyRelationship<T, TManyToMany>(db, x)));
        }

        /// <summary>
        /// 刪除 - 包含刪除多對多關係
        /// </summary>
        /// <typeparam name="TManyToMany">要被刪多對多的第一個形態</typeparam>
        /// <typeparam name="TManyToMany2">要被刪多對多的第二個形態</typeparam>
        /// <param name="wherePredicate">要被刪除的那一筆的where條件</param>
        /// <param name="includes">包含進去多對多的欄位</param>
        /// <returns></returns>
        public virtual bool DeleteWithManyToMany<TManyToMany, TManyToMany2>(System.Linq.Expressions.Expression<Func<T, bool>> wherePredicate,
            params Expression<Func<T, object>>[] includes)
            where TManyToMany : class
            where TManyToMany2 : class
        {
            return DeleteForManyToMany(wherePredicate, includes,
                (x =>
                {
                    deleteProcess.DeleteIncludeManyToManyRelationship<T, TManyToMany>(db, x);
                    deleteProcess.DeleteIncludeManyToManyRelationship<T, TManyToMany2>(db, x);
                }));
        }

        private bool DeleteForManyToMany(System.Linq.Expressions.Expression<Func<T, bool>> wherePredicate,
            Expression<Func<T, object>>[] includes, Action<T> deleteManyToMany)
        {
            var data = db.Repository<T>().Reads();

            foreach (var item in includes)
            {
                data.Include(item);
            }

            var entity = data.Where(wherePredicate).FirstOrDefault();

            deleteManyToMany.Invoke(entity);

            db.Repository<T>().Delete(entity);

            SaveChange();

            return ValidationDictionary.IsValid;
        }

        /// <summary>
        /// 刪除某一筆Entity
        /// </summary>
        /// <param name="wherePredicate">過濾出要被刪除的Entity條件</param>
        /// <returns>是否刪除成功</returns>
        public virtual bool Delete(Expression<Func<T, bool>> wherePredicate)
        {
            var data = db.Repository<T>().Read(wherePredicate);
            db.Repository<T>().Delete(data);

            SaveChange();

            return ValidationDictionary.IsValid;
        }

        /// <summary>
        /// 實際儲呼叫DB儲存。如果有發生驗證錯誤，把它記錄到ValidationDictionary
        /// </summary>
        protected void SaveChange()
        {
            try
            {
                db.Save();
            }
            catch (ValidationErrors propertyErrors)
            {
                ValidationDictionary.AddValidationErrors(propertyErrors);
            }
        }

        /// <summary>
        /// 依照某一個ViewModel的值，產生對應的Entity並且新增到資料庫
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="viewModel">ViewModel的Reference</param>
        /// <returns>是否儲存成功</returns>
        public bool CreateViewModelToDatabase<TViewModel>(TViewModel viewModel)
        {
            if (ValidationDictionary.IsValid)
            {
                CreateSingleViewModel<TViewModel>(viewModel);

                SaveChange();
            }

            return ValidationDictionary.IsValid;
        }


        /// <summary>
        /// 依照某一個ViewModel的值，產生對應的Entity並且新增到資料庫:客製路徑
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="viewModel">ViewModel的Reference</param>
        /// <returns>是否儲存成功</returns>
        public bool CreateViewModelToDatabase<TViewModel>(TViewModel viewModel, string path)
        {
            if (ValidationDictionary.IsValid)
            {
                CreateSingleViewModel<TViewModel>(viewModel, path);

                SaveChange();
            }

            return ValidationDictionary.IsValid;
        }

        /// <summary>
        /// 處理某一筆viewmodel並且用Repository做Create。但是還沒有save
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="viewModel">ViewModel的Reference</param>
        protected virtual void CreateSingleViewModel<TViewModel>(TViewModel viewModel)
        {
            //httpFileProcess.SaveHttpPostFile(viewModel, Core.Utility.Config.DoPath);

            var entity = viewModel.ToModel<T>();

            db.Repository<T>().Create(entity);
        }

        /// <summary>
        /// 處理某一筆viewmodel並且用Repository做Create。但是還沒有save : 客製儲存路徑
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="viewModel">ViewModel的Reference</param>
        protected virtual void CreateSingleViewModel<TViewModel>(TViewModel viewModel, string savePath)
        {
            //httpFileProcess.SaveHttpPostFile(viewModel, savePath);

            var entity = viewModel.ToModel<T>();

            db.Repository<T>().Create(entity);
        }

        /// <summary>
        /// 依照IList ViewModel的值，產生對應的Entity並且新增到資料庫
        /// </summary>
        /// <typeparam name="TViewModel">ViewModel的形態</typeparam>
        /// <param name="viewModelList">要被儲存到DB裡面的ViewModel清單</param>
        /// <returns>是否儲存成功</returns>
        public bool CreateListViewModelToDatabase<TViewModel>(IList<TViewModel> viewModelList)
        {
            if (ValidationDictionary.IsValid)
            {
                foreach (var viewModel in viewModelList)
                {
                    CreateSingleViewModel<TViewModel>(viewModel);
                }

                SaveChange();
            }

            return ValidationDictionary.IsValid;
        }

    }
}
