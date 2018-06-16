using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Infrastructure.Authorization.Exceptions
{
    class UserNotAuthorizedException : Exception
    {
        public UserNotAuthorizedException()
        {
        }

        public UserNotAuthorizedException(string message) : base(message)
        {
        }
    }
}
