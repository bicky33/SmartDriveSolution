using Contract.DTO.HR;
using Contract.DTO.HR.CompositeDto;
using Contract.DTO.HR.UpdateEmployee;
using Contract.DTO.UserModule;
using Domain.Entities.HR;
using Domain.Entities.Master;
using Domain.Entities.Users;
using Domain.Exceptions;
using Domain.Repositories.HR;
using Domain.Repositories.UserModule;
using Domain.RequestFeatured;
using Mapster;
using Service.Abstraction.Base;
using Service.Abstraction.HR;
using System.Transactions;

namespace Service.HR
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IRepositoryHRManager _repositoryManager;
        private readonly IRepositoryManagerUser _userRepositoryManager;


        public EmployeeService(IRepositoryHRManager repositoryManager, IRepositoryManagerUser userRepositoryManager)
        {
            _repositoryManager = repositoryManager;
            _userRepositoryManager = userRepositoryManager;
        }

        public async Task<EmployeeCreateDto> CreateEmployee(EmployeeCreateDto entity)
        {
            using var transaction = new TransactionScope(
                TransactionScopeOption.Required,
                TransactionScopeAsyncFlowOption.Enabled

                );
            try
            {
                var be = _userRepositoryManager.BusinessEntityRepository.CreateEntity();
                await _userRepositoryManager.UnitOfWork.SaveChangesAsync();

                var emp = entity.Adapt<Employee>();
               
                var phone = entity.EmpEntity.UserPhones.FirstOrDefault().Adapt<UserPhone>();
                var address = entity.EmpEntity.UserAddresses.FirstOrDefault().Adapt<UserAddress>();

                var employee = new Employee()
                {
                    EmpEntityid = be.Entityid,
                    EmpName = emp.EmpName,
                    EmpJoinDate = emp.EmpJoinDate,
                    EmpGraduate = emp.EmpGraduate,
                    EmpType = emp.EmpType,
                    EmpStatus = emp.EmpStatus,
                    EmpAccountNumber = emp.EmpAccountNumber,
                    EmpNetSalary = emp.EmpNetSalary,
                    EmpJobCode = emp.EmpJobCode,
                    SoftDelete = "ACTIVE",
                };

                


                var role = new UserRole();
                var useraddress = new UserAddress()
                {
                    UsdrEntityid = be.Entityid,
                    UsdrAddress1 = address.UsdrAddress1,
                    UsdrAddress2 = address.UsdrAddress2,
                    UsdrCity = address.UsdrCity,    

                };
                
                var userPhone = new UserPhone()
                {
                    UsphEntityid = be.Entityid,
                    UsphPhoneNumber = phone.UsphPhoneNumber,
                    UsphPhoneType = phone.UsphPhoneType,
                };

                emp.EmpEntityid = be.Entityid;

                var user = entity.EmpEntity.Adapt<User>();
                var userEmployee = new User()
                {
                    UserEntityid = be.Entityid,
                    UserFullName = emp.EmpName,
                  UserName = userPhone.UsphPhoneNumber,
                  UserPassword = BCrypt.Net.BCrypt.HashPassword(userPhone.UsphPhoneNumber),
                  UserEmail = user.UserEmail,
                  UserNpwp = user.UserNpwp,
                  UserNationalId = user.UserNationalId,

                 };
                //user.UserEntityid = be.Entityid;


               // address.UsdrEntityid = be.Entityid;
                role.UsroEntityid = be.Entityid;
               // phone.UsphEntityid = be.Entityid;
                user.UserFullName = emp.EmpName;

                //asign username dan password
               /* user.UserName = userPhone.UsphPhoneNumber;
                user.UserPassword = BCrypt.Net.BCrypt.HashPassword(userPhone.UsphPhoneNumber);*/

                _userRepositoryManager.UserRepository.CreateEntity(userEmployee);
                await _userRepositoryManager.UnitOfWork.SaveChangesAsync();

                if (entity.grantUser = true)
                {
                    role.UsroRoleName = "EM";
                    role.UsroModifiedDate = DateTime.Now;
                    role.UsroStatus = "ACTIVE";
                    _userRepositoryManager.UserRoleRepository.CreateEntity(role);
                    await _userRepositoryManager.UnitOfWork.SaveChangesAsync();
                }
                else
                {
                    role.UsroRoleName = "EM";
                    role.UsroModifiedDate = DateTime.Now;
                    role.UsroStatus = "INACTIVE";
                    _userRepositoryManager.UserRoleRepository.CreateEntity(role);
                    await _userRepositoryManager.UnitOfWork.SaveChangesAsync();
                }

                _userRepositoryManager.UserAddressRepository.CreateEntity(useraddress);
                await _userRepositoryManager.UnitOfWork.SaveChangesAsync();


                _userRepositoryManager.UserPhoneRepository.CreateEntity(userPhone);
                await _userRepositoryManager.UnitOfWork.SaveChangesAsync();

                _repositoryManager.EmployeeRepository.CreateEntity(employee);
                await _repositoryManager.UnitOfWorks.SaveChangesAsync();

                transaction.Complete();
                return entity;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<EmployeeDto> CreateAsync(EmployeeDto entity)
        {
            var be = _userRepositoryManager.BusinessEntityRepository.CreateEntity();
            return entity;
        }


        public async Task DeleteAsync(int id)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                var category = await _repositoryManager.EmployeeRepository.GetEntityById(id, true);
                if (category == null)
                {
                    throw new EntityNotFoundException(id, nameof(Employee));
                }
                category.SoftDelete = "INACTIVE";
                await _repositoryManager.UnitOfWorks.SaveChangesAsync();

                transaction.Complete();
            } 
            catch(Exception)
            {
                throw;
            }
           
        }

        public async Task<IEnumerable<EmployeeShowDto>> GetData(bool trackChanges)
        {
            var employee = await _repositoryManager.EmployeeRepository.GetAllEntity(trackChanges);
            var employeeDto = employee.Adapt<IEnumerable<EmployeeShowDto>>();
            return employeeDto;
        }

        public async Task<IEnumerable<EmployeeShowDto>> GetAllPagingAsync(EntityParameter entityParameter, bool trackChanges)
        {
            var categories = await _repositoryManager.EmployeeRepository.GetAllPaging(entityParameter, trackChanges);

            var categoryDto = categories.Adapt<IEnumerable<EmployeeShowDto>>();

            return categoryDto;
        }

        public async Task<EmployeeDto> GetByIdAsync(int id, bool trackChanges)
        {
            var category = await _repositoryManager.EmployeeRepository.GetEntityById(id, false);
            if (category == null)
            {
                throw new EntityNotFoundException(id, nameof(EmployeeDto));
            }

            var data = category.Adapt<EmployeeDto>();

            return data;
        }

        public async Task UpdateData(int id, EmployeeUpdateDto entity)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                //update employee
                var data = await _repositoryManager.EmployeeRepository.GetEntityById(id, true);
                if (data == null)
                {
                    throw new EntityNotFoundException(id, nameof(Employee));
                }
                data.EmpName = entity.EmpName;
                data.EmpGraduate = entity.EmpGraduate.ToString();
                data.EmpNetSalary = entity.EmpNetSalary;
                data.EmpJobCode = entity.EmpJobCode;
                data.EmpModifiedDate = DateTime.Now;
                data.EmpAccountNumber = entity.EmpAccountNumber;
                await _repositoryManager.UnitOfWorks.SaveChangesAsync();

                //Update user
                var user = await _userRepositoryManager.UserRepository.GetEntityById(id, true);

                if (user == null)
                {
                    throw new EntityNotFoundException(id, nameof(User));
                }

                user.UserModifiedDate = DateTime.Now;
                user.UserFullName = data.EmpName;
                await _userRepositoryManager.UnitOfWork.SaveChangesAsync();

                var userAddress = await _userRepositoryManager.UserAddressRepository.GetAllEntityById(id, true);
                if (userAddress == null)
                {
                    throw new EntityNotFoundException(id, nameof(UserAddress));
                }
                var address = userAddress.FirstOrDefault();
                address.UsdrAddress1 = entity.UserComposite.UserAddressCompositeDto.UsdrAddress1;
                address.UsdrAddress2 = entity.UserComposite.UserAddressCompositeDto.UsdrAddress2;
                address.UsdrCityId = entity.UserComposite.UserAddressCompositeDto.UsdrCityId;

                await _userRepositoryManager.UnitOfWork.SaveChangesAsync();

                transaction.Complete();
            } 
            catch (Exception)
            {
                throw;
            }


        }
            Task<IEnumerable<EmployeeDto>> IServiceEntityBase<EmployeeDto>.GetAllAsync(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, EmployeeDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeCreateDto> FindEmployeeById(int id)
        {
            var employee = await _repositoryManager.EmployeeRepository.FindEmployeeById(id);
            var employeeDto = employee.Adapt<EmployeeCreateDto>();
            return employeeDto;
        }
    }
}
