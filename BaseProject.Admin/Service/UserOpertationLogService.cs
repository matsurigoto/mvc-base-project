using BaseProject.Model;
using Core.Common.Business;
using Core.Common.Repository;
using Core.Service;

namespace BaseProject.Admin.Service
{
    public class UserOpertationLogService : GenericService<UserOperationLog>, IUserOpertationLogService
    {
        public UserOpertationLogService(IUnitOfWork inDb,
            /*IHttpFileProcessBusiness inHttpFileProcess,*/
            IDeleteManyToManyProcess inDeleteProcess)
            : base(inDb,
                   /*inHttpFileProcess,*/
                   inDeleteProcess)
        {
        }
    }
}
