using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.YUMServices.Base
{
    abstract class BaseService
    {
        protected readonly DbContext _context;

        protected BaseService()
        {
            _context = new YUMFoodEntities();
        }
    }
}
