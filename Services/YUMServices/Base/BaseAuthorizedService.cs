using DataAccess.Infrastructure.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Infrastructure.Authorization.AuthorizationManager;

namespace Services.YUMServices.Base
{
    abstract class BaseAuthorizedService : BaseService
    {
        protected readonly AuthorizationManager _authorizationManager;
        protected readonly int _id;

        protected BaseAuthorizedService(string userName, UserRole userRole) : base()
        {
            _authorizationManager = new AuthorizationManager(_context);

            var id = _authorizationManager.AuthorizeMe(userName, userRole);

            _id = id ?? 0;
        }
    }
}
