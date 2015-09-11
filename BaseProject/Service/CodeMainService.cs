using BaseProject.Model;
using Core.Common.Business;
using Core.Common.Repository;
using Core.Service;


namespace BaseProject.Service
{
    public class CodeMainService : GenericService<CodeMain>, ICodeMainService
    {
        public CodeMainService(IUnitOfWork inDb,
            //IHttpFileProcessBusiness inHttpFileProcess,
            IDeleteManyToManyProcess inDeleteProcess)
            : base(inDb,
                //inHttpFileProcess, 
                inDeleteProcess)
        {
        }
    }
}
