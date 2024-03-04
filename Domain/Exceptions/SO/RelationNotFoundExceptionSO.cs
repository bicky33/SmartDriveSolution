using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.SO
{
    public class RelationNotFoundExceptionSO : NotFoundException
    {
        public RelationNotFoundExceptionSO(string relation, string table) : base($"Relation {relation} at {table} not found")
        {
        }
    }
}
