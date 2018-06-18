using DataAccess.Infrastructure.Authorization;
using DataAccess.Model;
using Services.YUMServices.Base;
using Services.YUMServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Infrastructure.Authorization.AuthorizationManager;

namespace Services.YUMServices.Clients
{
    public class RestaurantsService : BaseAuthorizedService
    {
        private readonly UserProfile _userProfile;

        public RestaurantsService(string userName) : base(userName, UserRole.CLIENT)
        {
            _userProfile = _unitOfWork.UserProfileRepository.Get(_id);
        }

        public IEnumerable<Restaurant> Get()
        {
        }

        public Restaurant Get(int id)
        {
            return _unitOfWork.RestaurantRepository.Get(
                r => (r.RestaurantId == id) && (r.Country == _userProfile.Country) && (r.City == _userProfile.City)
                ).FirstOrDefault();
        }

        public void Rate(int restaurantId, int rating)
        {
            Restaurant restaurant = _unitOfWork.RestaurantRepository.Get(
                r => (r.RestaurantId == restaurantId) && (r.Country == _userProfile.Country) && (r.City == _userProfile.City)
                ).FirstOrDefault();

            if (restaurant == null)
            {
                throw new EntityNotFoundException("Restaurant with id " + restaurantId + " not found.");
            }

            RestaurantRating restaurantRating = _unitOfWork.RestaurantRatingRepository.Get(
                rr => (rr.UserProfileId == _userProfile.UserProfileId) && (rr.RestaurantId == restaurant.RestaurantId)
                ).FirstOrDefault();

            if (restaurantRating == null)
            {
                restaurantRating = new RestaurantRating
                {
                    CreatedAt = DateTime.Now,
                    Rating = rating,
                    UserProfileId = _userProfile.UserProfileId,
                    RestaurantId = restaurant.RestaurantId
                };

                _unitOfWork.RestaurantRatingRepository.Add(restaurantRating);
            }
            else
            {
                restaurantRating.CreatedAt = DateTime.Now;
                restaurantRating.Rating = rating;

                _unitOfWork.RestaurantRatingRepository.Update(restaurantRating);
            }

            _unitOfWork.Complete();
        }

        public RestaurantRating GetMyRating(int restaurantId)
        {
            Restaurant restaurant = _unitOfWork.RestaurantRepository.Get(
              r => (r.RestaurantId == restaurantId) && (r.Country == _userProfile.Country) && (r.City == _userProfile.City)
              ).FirstOrDefault();

            if (restaurant == null)
            {
                return null;
            }

            return _unitOfWork.RestaurantRatingRepository.Get(
                rr => (rr.UserProfileId == _userProfile.UserProfileId) && (rr.RestaurantId == restaurant.RestaurantId)
                ).FirstOrDefault();
        }

        public Nullable<double> GetAverageRating(int restaurantId)
        {
            Restaurant restaurant = _unitOfWork.RestaurantRepository.Get(
              r => (r.RestaurantId == restaurantId) && (r.Country == _userProfile.Country) && (r.City == _userProfile.City)
              ).FirstOrDefault();

            if (restaurant == null)
            {
                return null;
            }

            return _unitOfWork.RestaurantRatingRepository.Get(
                rr => rr.RestaurantId == restaurant.RestaurantId
                ).Average(rr => rr.Rating);
        }

        public void AddReview(int restaurantId, string review)
        {
            Restaurant restaurant = _unitOfWork.RestaurantRepository.Get(
              r => (r.RestaurantId == restaurantId) && (r.Country == _userProfile.Country) && (r.City == _userProfile.City)
              ).FirstOrDefault();

            if (restaurant == null)
            {
                throw new EntityNotFoundException("Restaurant with id " + restaurantId + " not found.");
            }

            RestaurantReview restaurantReview = new RestaurantReview
            {
                CreatedAt = DateTime.Now,
                Review = review,
                UserProfileId = _userProfile.UserProfileId,
                RestaurantId = restaurant.RestaurantId
            };

            _unitOfWork.RestaurantReviewRepository.Add(restaurantReview);

            _unitOfWork.Complete();
        }

        public IEnumerable<RestaurantReview> GetReviews(int restaurantId)
        {
            Restaurant restaurant = _unitOfWork.RestaurantRepository.Get(
            r => (r.RestaurantId == restaurantId) && (r.Country == _userProfile.Country) && (r.City == _userProfile.City)
            ).FirstOrDefault();

            if (restaurant == null)
            {
                return null;
            }

            return _unitOfWork.RestaurantReviewRepository.Get(
                rr => rr.RestaurantId == restaurant.RestaurantId,
                q => q.OrderByDescending(rr => rr.CreatedAt));
        }
    }
}
