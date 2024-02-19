using Contract.DTO.HR;
using Contract.DTO.HR.CompositeDto;
using Contract.DTO.UserModule;
using Domain.Entities.HR;
using Domain.Entities.Users;
using Domain.Repositories.HR;
using Domain.RequestFeatured;
using Mapster;
using Service.Abstraction.HR;

namespace Service.HR
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IRepositoryHRManager _repositoryManager;


        public EmployeeService(IRepositoryHRManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<BusinessEntityCompositeDto> CreateEmployee(BusinessEntityCompositeDto entity)
        {
            /*            var emp = entity.Adapt<Employee>();

                        var userDto = entity.Adapt<User>();
                        var userAddress = entity.Adapt<UserAddress>();
                        var userPhone = entity.Adapt<UserPhone>();
                        var userRole = entity.Adapt<UserRole>();

                        userDto.UserEntityid = emp.EmpEntityid;
                        userAddress.UsdrEntityid = emp.EmpEntityid;
                        userRole.UsroEntityid = emp.EmpEntityid;
                        userPhone.UsphEntityid = emp.EmpEntityid;*/
            var be = entity.Adapt<BusinessEntity>();
            var user = entity.UserComposite.Adapt<User>();
            var userAddress = entity.UserComposite.UserAddressCompositeDto.Adapt<IEnumerable<UserAddress>>();
            var userPhone = entity.UserComposite.UserPhoneCompositeDto.Adapt<IEnumerable<UserPhone>>();
            var userRole = entity.UserComposite.UserRoleCompositeDto.Adapt<IEnumerable<UserRole>>();
            var employee = entity.UserComposite.EmployeeDto.Adapt<Employee>();


          /*  var user = userDto.Adapt<User>();
            var userAddress = userAddressDto.Adapt<UserAddress>();
            var userPhone = userPhoneDto.Adapt<UserPhone>();
            var userRole = userRoleDto.Adapt<UserRole>();
            var employee = employeeDto.Adapt<Employee>();
*/
            user.UserEntityid = be.Entityid;
           
            foreach (var address in userAddress) 
            {
                address.UsdrEntityid = be.Entityid;
            }

            foreach (var phone in userPhone)
            {
                phone.UsphEntityid = be.Entityid;
                user.UserPassword = phone.UsphPhoneNumber;

            }

            foreach (var role in userRole)
            {
                role.UsroEntityid = be.Entityid;
            } 
            employee.EmpEntityid = be.Entityid;


            user.UserFullName = employee.EmpName;
            //user.UserPassword = userPhone.UsphPhoneNumber;
            user.UserEmail = user.UserEmail;
           /* var dataUserPhone = entity.Adapt<UserPhone>();
            var dataUser = entity.Adapt<User>();
            var dataUserAddress = entity.Adapt<UserAddress>();
            var dataUserRole = entity.Adapt<UserRole>();*/


            /*emp.User.UserEntityid = entity.Entityid;
            emp.User.UserRoles = new List<UserRole>
            {
                new UserRole {UsroEntityid=entity.Entityid},
            };
            emp.User.UserPhones = new List<UserPhone>
            {
                new UserPhone {UsphEntityid=entity.Entityid},
            };
            emp.User.UserAddresses = new List<UserAddress>
            {
                new UserAddress {UsdrEntityid = entity.Entityid},
            };

            emp.User.UserName = entity.UserComposite.UserEmail;
            
            var data = entity.Adapt<UserPhone>();

            emp.User.UserPassword = data.UsphPhoneNumber;*/


            //_repositoryManager.EmployeeRepository.CreateEmployee(be,user,userAddress,userPhone,userRole,employee);
            await _repositoryManager.UnitOfWorks.SaveChangesAsync();

            return entity;
        }

        public Task<EmployeeDto> CreateAsync(EmployeeDto entity)
        {
            throw new NotImplementedException();
        }


        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync(bool trackChanges)
        {
            var employee = await _repositoryManager.EmployeeRepository.GetAllEntity(false);
            var employeeDto = employee.Adapt<IEnumerable<EmployeeDto>>();
            return employeeDto;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllPagingAsync(EntityParameter entityParameter, bool trackChanges)
        {
            var categories = await _repositoryManager.EmployeeRepository.GetAllPaging(entityParameter, trackChanges);

            var categoryDto = categories.Adapt<IEnumerable<EmployeeDto>>();

            return categoryDto;
        }

        public async Task<EmployeeDto> GetByIdAsync(int id, bool trackChanges)
        {
            var category = await _repositoryManager.EmployeeRepository.GetEntityById(id, false);
            /* if (category == null)
             {
                 throw new EntityBadRequestException(id, "Category");
             }*/

            var data = category.Adapt<EmployeeDto>();

            return data;
        }

        public Task UpdateAsync(int id, EmployeeDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
