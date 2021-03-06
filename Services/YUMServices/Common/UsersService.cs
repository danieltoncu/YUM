﻿using DataAccess.Model;
using Services.YUMServices.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Infrastructure.Authorization.AuthorizationManager;

namespace Services.YUMServices.Common
{
    public class UsersService : BaseService
    {
        public UsersService() : base()
        {
        }

        public void RegisterClient(string userName, UserProfile userProfile)
        {
            userProfile.CreatedAt = DateTime.Now;

            _unitOfWork.UserProfileRepository.Add(userProfile);

            _authorizationManager.RegisterClientRole(userName, userProfile);

            _unitOfWork.Complete();
        }

        public void RegisterRestaurant(string userName, Restaurant restaurant)
        {
            restaurant.CreatedAt = DateTime.Now;

            _unitOfWork.RestaurantRepository.Add(restaurant);

            RestaurantMonitor restaurantMonitor = new RestaurantMonitor
            {
                CreatedAt = DateTime.Now,
                RestaurantId = restaurant.RestaurantId
            };

            _unitOfWork.RestaurantMonitorRepository.Add(restaurantMonitor);

            _authorizationManager.RegisterRestaurantRole(userName, restaurant, restaurantMonitor);

            _unitOfWork.Complete();
        }

        public IEnumerable<UserRole> GetUserRoles(string userName)
        {
            return _authorizationManager.GetUserRoles(userName);
        }

        public Nullable<int> AuthorizeUser(string userName, UserRole userRole)
        {
            return _authorizationManager.AuthorizeMe(userName, userRole);
        }
    }
}
