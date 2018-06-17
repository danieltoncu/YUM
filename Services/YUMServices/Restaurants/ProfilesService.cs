using DataAccess.Infrastructure.Authorization;
using DataAccess.Model;
using Services.YUMServices.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Infrastructure.Authorization.AuthorizationManager;

namespace Services.YUMServices.Restaurants
{
    public class ProfilesService : BaseAuthorizedService
    {
        public ProfilesService(string userName) : base(userName, UserRole.RESTAURANT)
        {
        }

        public Restaurant Get()
        {
            return _unitOfWork.RestaurantRepository.Get(_id);
        }

        public void Update(Restaurant restaurant)
        {
            restaurant.RestaurantId = _id;

            _unitOfWork.RestaurantRepository.Update(restaurant);

            _unitOfWork.Complete();
        }
    }
}
