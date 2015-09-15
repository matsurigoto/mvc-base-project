using BaseProject.Model;
using Core.Common.Business;
using Core.Common.Repository;
using Core.Service;

namespace BaseProject.Service
{
    public class CodeService : GenericService<Code>, ICodeService
    {
        public CodeService(IUnitOfWork inDb,
            /*IHttpFileProcessBusiness inHttpFileProcess,*/
            IDeleteManyToManyProcess inDeleteProcess)
            : base(inDb,
            /*inHttpFileProcess, */
            inDeleteProcess)
        {
        }
    }
}
