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
    public class DishTypesService : BaseAuthorizedService
    {
        public DishTypesService(string userName) : base(userName, UserRole.ANY)
        {
        }

        public IEnumerable<DishType> Get()
        {
            return _unitOfWork.DishTypeRepository.Get();
        }

        public DishType Get(int id)
        {
            return _unitOfWork.DishTypeRepository.Get(id);
        }
    }
}
