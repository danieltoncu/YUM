using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Infrastructure.Authorization.AuthorizationManager;

namespace Services.YUMServices.Base
{
    public abstract class BaseAuthorizedService : BaseService
    {
        protected readonly int _id;

        protected BaseAuthorizedService(string userName, UserRole userRole) : base()
        {
            var id = _authorizationManager.AuthorizeMe(userName, userRole);

            _id = id ?? 0;
        }
    }
}
