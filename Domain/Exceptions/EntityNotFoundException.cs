﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class EntityNotFoundException : NotFoundException
    {
        public EntityNotFoundException(int? id, string message) : base($"Entity {message} with identifier {id} not found")
        {
        }

        public EntityNotFoundException(string? id, string message) : base($"Entity {message} with identifier {id} not found")
        {
        }
    }
}
