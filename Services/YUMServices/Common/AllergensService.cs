using DataAccess.Infrastructure.Authorization;
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
    public class AllergensService : BaseAuthorizedService
    {
        public AllergensService(string userName) : base(userName, UserRole.ANY)
        {
        }

        public IEnumerable<Allergen> Get()
        {
            return _unitOfWork.AllergenRepository.Get();
        }

        public Allergen Get(int id)
        {
            return _unitOfWork.AllergenRepository.Get(id);
        }
    }
}
