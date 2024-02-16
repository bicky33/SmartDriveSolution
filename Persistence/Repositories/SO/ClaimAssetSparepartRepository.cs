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
    public class ClaimAssetSparepartRepository : RepositoryBase<ClaimAssetSparepart>, IRepositorySOEntityBase<ClaimAssetSparepart,int>
    {
        public ClaimAssetSparepartRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public void CreateEntity(ClaimAssetSparepart entity)
        {
            Create(entity);
        }

        public void DeleteEntity(ClaimAssetSparepart entity)
        {
            Delete(entity);

        }

        public async Task<IEnumerable<ClaimAssetSparepart>> GetAllEntity(bool trackChanges)
        {
            return await GetAll(trackChanges).OrderBy(x=>x.CaspId).ToListAsync();
        }

        public async Task<ClaimAssetSparepart> GetEntityById(int id, bool trackChanges)
        {
            return await GetByCondition(c => c.CaspId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
        
    }
}
