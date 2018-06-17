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
    public class OrderStatesService : BaseAuthorizedService
    {
        public OrderStatesService(string userName) : base(userName, UserRole.ANY)
        {
        }

        public IEnumerable<OrderState> Get()
        {
            return _unitOfWork.OrderStateRepository.Get();
        }

        public OrderState Get(int id)
        {
            return _unitOfWork.OrderStateRepository.Get(id);
        }
    }
}
