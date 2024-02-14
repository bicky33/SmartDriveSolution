using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.SO
{
    public class EntityNotFoundException : NotFoundException
    {
        public EntityNotFoundException(object id, string message) : base($"Entity {message} with identifier {id} not found")
        {
        }
    }
}
