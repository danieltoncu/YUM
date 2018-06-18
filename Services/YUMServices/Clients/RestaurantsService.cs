using DataAccess.Infrastructure.Authorization;
using DataAccess.Model;
using Services.YUMServices.Base;
using Services.YUMServices.Exceptions;
using Services.YUMServices.Infrastructure;
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

        private FilterOptions<Restaurant> filterOptions;
        private SortOptions<Restaurant> sortOptions;

        private FilterOptions<Restaurant> FilterOptions
        {
            get
            {
                if (filterOptions == null)
                {
                    filterOptions = new FilterOptions<Restaurant>(null);
                }

                filterOptions.AddOption("open_now", "Deschise Acum",
                    (r => (r.OpenAt == null) || (r.CloseAt == null) || ((r.OpenAt < DateTime.Now.TimeOfDay) && (r.CloseAt > DateTime.Now.TimeOfDay))));

                filterOptions.AddOption("free_delivery", "Livrări Gratis", (r => r.MinOrderFreeDelivery != null));

                return filterOptions;
            }
        }

        private SortOptions<Restaurant> SortOptions
        {
            get
            {
                if (sortOptions == null)
                {
                    sortOptions = new SortOptions<Restaurant>(q => q.OrderBy(r => r.Name));
                }

                sortOptions.AddOption("name_asc", "Nume", (q => q.OrderBy(r => r.Name)));

                sortOptions.AddOption("newest", "Cele mai noi", (q => q.OrderByDescending(r => r.CreatedAt)));

                sortOptions.AddOption("popularity", "Cele mai populare", (q => q.OrderByDescending(r => r.Orders.Count)));

                sortOptions.AddOption("most_rated", "Cele mai apreciate",
                    (q => q.OrderByDescending(r => r.RestaurantRatings.Count).ThenByDescending(r => r.RestaurantRatings.Average(rr => rr.Rating))));

                sortOptions.AddOption("most_reviewed", "Cele mai discutate", (q => q.OrderByDescending(r => r.RestaurantReviews.Count)));

                sortOptions.AddOption("minimum_order", "Comandă minimă", (q => q.OrderBy(r => r.MinOrderFreeDelivery == null).ThenBy(r => r.MinOrderFreeDelivery)));

                sortOptions.AddOption("delivery_time", "Timp de livrare", (q => q.OrderBy(r => r.DeliveryTime == null).ThenBy(r => r.DeliveryTime)));

                return sortOptions;
            }
        }

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

        public void Rate(RestaurantRating restaurantRating)
        {
            Restaurant restaurant = _unitOfWork.RestaurantRepository.Get(
                r => (r.RestaurantId == restaurantRating.RestaurantId) && (r.Country == _userProfile.Country) && (r.City == _userProfile.City)
                ).FirstOrDefault();

            if (restaurant == null)
            {
                throw new EntityNotFoundException("Restaurant with id " + restaurantRating.RestaurantId + " not found.");
            }

            RestaurantRating existingRestaurantRating = _unitOfWork.RestaurantRatingRepository.Get(
                rr => (rr.UserProfileId == _userProfile.UserProfileId) && (rr.RestaurantId == restaurant.RestaurantId)
                ).FirstOrDefault();

            if (existingRestaurantRating == null)
            {
                restaurantRating.CreatedAt = DateTime.Now;
                restaurantRating.UserProfileId = _userProfile.UserProfileId;

                _unitOfWork.RestaurantRatingRepository.Add(restaurantRating);
            }
            else
            {
                existingRestaurantRating.CreatedAt = DateTime.Now;
                existingRestaurantRating.Rating = restaurantRating.Rating;

                _unitOfWork.RestaurantRatingRepository.Update(existingRestaurantRating);
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

        public void AddReview(RestaurantReview restaurantReview)
        {
            Restaurant restaurant = _unitOfWork.RestaurantRepository.Get(
              r => (r.RestaurantId == restaurantReview.RestaurantId) && (r.Country == _userProfile.Country) && (r.City == _userProfile.City)
              ).FirstOrDefault();

            if (restaurant == null)
            {
                throw new EntityNotFoundException("Restaurant with id " + restaurantReview.RestaurantId + " not found.");
            }

            restaurantReview.CreatedAt = DateTime.Now;
            restaurantReview.UserProfileId = _userProfile.UserProfileId;

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
                q => q.OrderByDescending(rr => rr.CreatedAt),
                "UserProfile.FirstName,UserProfile.LastName");
        }
    }
}
