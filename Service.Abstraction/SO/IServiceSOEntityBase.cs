namespace Service.Abstraction.SO
{
    public interface IServiceSOEntityBase<TEntityDto, TEntityDtoCreate, TID>
    {
        Task<IEnumerable<TEntityDto>> GetAllAsync(bool trackChanges);
        Task<TEntityDto> GetByIdAsync(TID id, bool trackChanges);
        Task<TEntityDtoCreate> CreateAsync(TEntityDtoCreate entity);
        Task<TEntityDtoCreate> UpdateAsync(TID id, TEntityDtoCreate entity);
        Task DeleteAsync(TID id);
    }
}
