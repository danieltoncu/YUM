using DataAccess.Infrastructure.Authorization;
using DataAccess.Model;
using Services.YUMServices.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Infrastructure.Authorization.AuthorizationManager;

namespace Services.YUMServices.Clients
{
    public class ClientProfilesService : BaseAuthorizedService
    {
        public ClientProfilesService(string userName) : base(userName, UserRole.CLIENT)
        {
        }

        public UserProfile Get()
        {
            return _unitOfWork.UserProfileRepository.Get(_id);
        }

        public void Update(UserProfile userProfile)
        {
            _unitOfWork.UserProfileRepository.Update(userProfile);
            _unitOfWork.Complete();
        }
    }
}
