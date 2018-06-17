using DataAccess.Model;
using Services.YUMServices.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Infrastructure.Authorization.AuthorizationManager;

namespace Services.YUMServices.Common
{
    public class OrderTypesService : BaseAuthorizedService
    {
        public OrderTypesService(string userName) : base(userName, UserRole.ANY)
        {
        }

        public IEnumerable<OrderType> Get()
        {
            return _unitOfWork.OrderTypeRepository.Get();
        }

        public OrderType Get(int id)
        {
            return _unitOfWork.OrderTypeRepository.Get(id);
        }
    }
}
