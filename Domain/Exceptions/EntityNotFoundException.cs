using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EntityNotFoundException : NotFoundExeption
    {
        public EntityNotFoundException(int id) : base($"Entity with identifier {id} not found")
        {
        }
    }
}
