using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EntityBadRequestException : BadRequestException
    {
        public EntityBadRequestException(string? message) : base($"Entity with {message} bad request")
        {
        }
    }
}
