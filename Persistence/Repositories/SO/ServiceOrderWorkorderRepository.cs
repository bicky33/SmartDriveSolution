﻿using Contract.DTO.SO;
using Domain.Entities.SO;
using Domain.Repositories.SO;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.SO
{
    public class ServiceOrderWorkorderRepository : RepositoryBase<ServiceOrderWorkorder>, IRepositoryEntityBase<ServiceOrderWorkorder,int>
    {
        public ServiceOrderWorkorderRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(ServiceOrderWorkorder entity)
        {
            Create(entity);
        }

        public void DeleteEntity(ServiceOrderWorkorder entity)
        {
            Delete(entity);

        }

        public async Task<IEnumerable<ServiceOrderWorkorder>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x=>x.SowoId).ToListAsync();
        }

        public async Task<ServiceOrderWorkorder> GetEntityById(int id, bool trackChanges)
        {
            var newId = (int)id;
            return await GetByCondition(c => c.SowoId.Equals(newId), trackChanges).SingleOrDefaultAsync();
        }
        
    }
}
