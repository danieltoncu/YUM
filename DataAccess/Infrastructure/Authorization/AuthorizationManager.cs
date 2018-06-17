using DataAccess.Infrastructure.Authorization.Exceptions;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Infrastructure.Authorization
{
    public class AuthorizationManager
    {
        private readonly DbContext _context;
        private readonly DbSet<User> _dbSet;

        public enum UserRole { CLIENT, RESTAURANT, RESTAURANT_MONITOR, ANY };

        public AuthorizationManager(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public Nullable<int> AuthorizeMe(string userName, UserRole userRole)
        {
            User user = _dbSet.Find(userName);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if ((userRole == UserRole.CLIENT) && (user.UserProfileId != null))
            {
                return user.UserProfileId;
            }

            if ((userRole == UserRole.RESTAURANT) && (user.RestaurantId != null))
            {
                return user.RestaurantId;
            }

            if ((userRole == UserRole.RESTAURANT_MONITOR) && (user.RestaurantMonitorId != null))
            {
                return user.RestaurantMonitorId;
            }

            if (userRole == UserRole.ANY)
            {
                if ((user.UserProfileId != null) || (user.RestaurantId != null) || (user.RestaurantMonitorId != null))
                {
                    return null;
                }
            }

            throw new UserNotAuthorizedException();
        }

        public void RegisterClientRole(string userName, UserProfile userProfile)
        {
            User user = _dbSet.Find(userName);

            if (user == null)
            {
                user = new User
                {
                    UserName = userName,
                    UserProfileId = userProfile.UserProfileId
                };

                _dbSet.Add(user);
            }
            else
            {
                if (user.UserProfileId != null)
                {
                    throw new UserRoleAlreadyExistsException();
                }

                user.UserProfileId = userProfile.UserProfileId;
                _context.Entry(user).State = EntityState.Modified;
            }
        }

        public void RegisterRestaurantRole(string userName, Restaurant restaurant, RestaurantMonitor restaurantMonitor)
        {
            User user = _dbSet.Find(userName);

            if (user == null)
            {
                user = new User
                {
                    UserName = userName,
                    RestaurantId = restaurant.RestaurantId,
                    RestaurantMonitorId = restaurantMonitor.RestaurantMonitorId
                };

                _dbSet.Add(user);
            }
            else
            {
                if ((user.RestaurantId != null) || (user.RestaurantMonitorId != null))
                {
                    throw new UserRoleAlreadyExistsException();
                }

                user.RestaurantId = restaurant.RestaurantId;
                user.RestaurantMonitorId = restaurantMonitor.RestaurantMonitorId;
                _context.Entry(user).State = EntityState.Modified;
            }
        }

        public List<UserRole> GetUserRoles(string userName)
        {
            List<UserRole> userRoles = new List<UserRole>();

            User user = _dbSet.Find(userName);

            if (user == null)
            {
                return userRoles;
            }

            if (user.UserProfileId != null)
            {
                userRoles.Add(UserRole.CLIENT);
            }
            if (user.RestaurantId != null)
            {
                userRoles.Add(UserRole.RESTAURANT);
            }
            if (user.RestaurantMonitorId != null)
            {
                userRoles.Add(UserRole.RESTAURANT_MONITOR);
            }

            return userRoles;
        }
    }
}
