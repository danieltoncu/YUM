using DataAccess.Infrastructure;
using DataAccess.Infrastructure.Authorization;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.YUMServices.Base
{
    public abstract class BaseService
    {
        private readonly DbContext _context;

        protected readonly UnitOfWork _unitOfWork;
        protected readonly AuthorizationManager _authorizationManager;

        protected BaseService()
        {
            _context = new YUMFoodEntities();

            _unitOfWork = new UnitOfWork(_context);
            _authorizationManager = new AuthorizationManager(_context);
        }
    }
}
