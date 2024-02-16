using Domain.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Base
{
    public interface IRepositoryEntityBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAllEntity(bool trackChanges);

        Task<T> GetEntityById(int id, bool trackChanges);

        void CreateEntity(T entity);

        void DeleteEntity(T entity);
    }

    public interface IRepositoryEntityPaymentTransaction
    {
        Task<IEnumerable<PaymentTransaction>> GetAllEntity(bool trackChanges);

        Task<PaymentTransaction> GetEntityById(int? id, bool trackChanges);

        Task<PaymentTransaction> CreateEntity(PaymentTransaction entity);

        void DeleteEntity(PaymentTransaction entity);
    }
}
