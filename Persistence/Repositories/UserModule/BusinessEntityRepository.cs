using Domain.Entities.Users;
using Domain.Repositories.UserModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.UserModule
{
    public class BusinessEntityRepository : RepositoryBase<BusinessEntity>,  IRepositoryBusinessEntity<BusinessEntity>
    {
        public BusinessEntityRepository(SmartDriveContext dbContext) : base(dbContext)
        {
        }

        public BusinessEntity CreateEntity()
        {
            var entity = new BusinessEntity()
            {
                EntityModifiedDate = DateTime.Now,
            };

            Create(entity);

            return entity;
        }
    }
}
