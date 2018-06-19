using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Infrastructure
{
    public class UnitOfWork : IDisposable
    {
        private readonly DbContext _context;

        private IRepository<UserProfile> userProfileRepository;
        private IRepository<Restaurant> restaurantRepository;
        private IRepository<RestaurantMonitor> restaurantMonitorRepository;

        private IRepository<Allergen> allergenRepository;
        private IRepository<DishType> dishTypeRepository;
        private IRepository<OrderType> orderTypeRepository;
        private IRepository<OrderState> orderStateRepository;

        private IRepository<Dish> dishRepository;

        private IRepository<RestaurantRating> restaurantRatingRepository;
        private IRepository<RestaurantReview> restaurantReviewRepository;

        private IRepository<DishRating> dishRatingRepository;
        private IRepository<DishReview> dishReviewRepository;

        public IRepository<UserProfile> UserProfileRepository
        {
            get
            {
                if (this.userProfileRepository == null)
                {
                    this.userProfileRepository = new Repository<UserProfile>(_context);
                }

                return this.userProfileRepository;
            }
        }

        public IRepository<Restaurant> RestaurantRepository
        {
            get
            {
                if (this.restaurantRepository == null)
                {
                    this.restaurantRepository = new Repository<Restaurant>(_context);
                }

                return this.restaurantRepository;
            }
        }

        public IRepository<RestaurantMonitor> RestaurantMonitorRepository
        {
            get
            {
                if (this.restaurantMonitorRepository == null)
                {
                    this.restaurantMonitorRepository = new Repository<RestaurantMonitor>(_context);
                }

                return this.restaurantMonitorRepository;
            }
        }

        public IRepository<Allergen> AllergenRepository
        {
            get
            {
                if (this.allergenRepository == null)
                {
                    this.allergenRepository = new Repository<Allergen>(_context);
                }

                return this.allergenRepository;
            }
        }

        public IRepository<DishType> DishTypeRepository
        {
            get
            {
                if (this.dishTypeRepository == null)
                {
                    this.dishTypeRepository = new Repository<DishType>(_context);
                }

                return this.dishTypeRepository;
            }
        }

        public IRepository<OrderType> OrderTypeRepository
        {
            get
            {
                if (this.orderTypeRepository == null)
                {
                    this.orderTypeRepository = new Repository<OrderType>(_context);
                }

                return this.orderTypeRepository;
            }
        }

        public IRepository<OrderState> OrderStateRepository
        {
            get
            {
                if (this.orderStateRepository == null)
                {
                    this.orderStateRepository = new Repository<OrderState>(_context);
                }

                return this.orderStateRepository;
            }
        }

        public IRepository<Dish> DishRepository
        {
            get
            {
                if (this.dishRepository == null)
                {
                    this.dishRepository = new Repository<Dish>(_context);
                }

                return this.dishRepository;
            }
        }

        public IRepository<RestaurantRating> RestaurantRatingRepository
        {
            get
            {
                if (this.restaurantRatingRepository == null)
                {
                    this.restaurantRatingRepository = new Repository<RestaurantRating>(_context);
                }

                return this.restaurantRatingRepository;
            }
        }

        public IRepository<RestaurantReview> RestaurantReviewRepository
        {
            get
            {
                if (this.restaurantReviewRepository == null)
                {
                    this.restaurantReviewRepository = new Repository<RestaurantReview>(_context);
                }

                return this.restaurantReviewRepository;
            }
        }

        public IRepository<DishRating> DishRatingRepository
        {
            get
            {
                if (this.dishRatingRepository == null)
                {
                    this.dishRatingRepository = new Repository<DishRating>(_context);
                }

                return this.dishRatingRepository;
            }
        }

        public IRepository<DishReview> DishReviewRepository
        {
            get
            {
                if (this.dishReviewRepository == null)
                {
                    this.dishReviewRepository = new Repository<DishReview>(_context);
                }

                return this.dishReviewRepository;
            }
        }


        public UnitOfWork(DbContext context)
        {
            _context = context;
        }


        public void Complete()
        {
            _context.SaveChanges();
        }


        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
