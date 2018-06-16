using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Infrastructure.Authorization.Exceptions
{
    class UserRoleAlreadyExistsException : Exception
    {
        public UserRoleAlreadyExistsException()
        {
        }

        public UserRoleAlreadyExistsException(string message) : base(message)
        {
        }
    }
}
