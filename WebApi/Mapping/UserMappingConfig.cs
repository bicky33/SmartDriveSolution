using Contract.DTO.UserModule;
using Domain.Entities.Users;
using Mapster;

namespace WebApi.Mapping
{
    public class UserMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            //config.NewConfig<UserDto, User>()
            //     .Map(dest => dest.UserRoles, src => src.UserRoles);
        }
    }
}
