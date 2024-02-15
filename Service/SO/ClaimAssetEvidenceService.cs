using Contract.DTO.SO;
using Domain.Exceptions.SO;
using Domain.Repositories.SO;
using Mapster;
using Service.Abstraction.SO;
using System.Net.Http.Headers;

namespace Service.SO
{
    public class ClaimAssetEvidenceService : IServiceSOEntityBase<ClaimAssetEvidenceDto,ClaimAssetEvidenceDtoCreate, int>
    {
        private readonly IRepositorySOManager _repositoryManager;

        public ClaimAssetEvidenceService(IRepositorySOManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

       public async Task<ClaimAssetEvidenceDtoCreate> CreateAsync(ClaimAssetEvidenceDtoCreate entityDto)
        {
            ClaimAssetEvidenceDtoCreate claimAssetDto = new();
            // save photo
            try
            {
                var file = entityDto.Photo;
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    // manually convert dtocreate to dto 
                    claimAssetDto = new ClaimAssetEvidenceDtoCreate()
                    {
                        CaevCreatedDate = entityDto.CaevCreatedDate,
                        CaevFilename = entityDto.CaevFilename,
                        CaevFiletype = file.ContentType,
                        // original value using bytes but converted to kb
                        CaevFilesize = (int)file.Length / 2048,
                        CaevUrl = fileName,
                        CaevNote = entityDto.CaevNote,
                        CaevSeroId = entityDto.CaevSeroId,
                        CaevPartEntityid = entityDto.CaevPartEntityid,
                        CaevServiceFee = entityDto.CaevServiceFee,
                    };
                }
            }
            catch (Exception)
            {
                throw;
            }
            var claimAssetEvidence = claimAssetDto.Adapt<Domain.Entities.SO.ClaimAssetEvidence>();
            // create to table 
            _repositoryManager.ClaimAssetEvidenceRepository.CreateEntity(claimAssetEvidence);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            
            return claimAssetDto;
        }

        public async Task DeleteAsync(int id)
        {
            var claimAssetEvidence = await _repositoryManager.ClaimAssetEvidenceRepository.GetEntityById(id,false);
            if (claimAssetEvidence == null)
                throw new EntityNotFoundException(id,"Claim Asset Evidence");
            _repositoryManager.ClaimAssetEvidenceRepository.DeleteEntity(claimAssetEvidence);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClaimAssetEvidenceDto>> GetAllAsync(bool trackChanges)
        {
            var claimAssetEvidence = await _repositoryManager.ClaimAssetEvidenceRepository.GetAllEntity(trackChanges);
            var claimAssetEvidenceDtos = claimAssetEvidence.Adapt<IEnumerable<ClaimAssetEvidenceDto>>();
            return claimAssetEvidenceDtos;
        }

        public async Task<ClaimAssetEvidenceDto> GetByIdAsync(int id, bool trackChanges)
        {
            var claimAssetEvidence = await _repositoryManager.ClaimAssetEvidenceRepository.GetEntityById(id, trackChanges); 
            if (claimAssetEvidence == null)
                throw new EntityNotFoundException(id,"Claim Asset Evidence");

            var ClaimAssetEvidenceDtos = claimAssetEvidence.Adapt<ClaimAssetEvidenceDto>();
            return ClaimAssetEvidenceDtos;
        }

        public async Task<ClaimAssetEvidenceDtoCreate> UpdateAsync(int id, ClaimAssetEvidenceDtoCreate entity)
        {
            var claimAssetEvidence = await _repositoryManager.ClaimAssetEvidenceRepository.GetEntityById(id, true);
            if (claimAssetEvidence == null)
                throw new EntityNotFoundException(id,"Claim Asset Evidence");

            claimAssetEvidence.CaevId = id;
            claimAssetEvidence.CaevFilename=entity.CaevFilename;
            claimAssetEvidence.CaevFilesize=entity.CaevFilesize;
            claimAssetEvidence.CaevUrl=entity.CaevUrl;
            claimAssetEvidence.CaevNote=entity.CaevNote;
            claimAssetEvidence.CaevPartEntityid=entity.CaevPartEntityid;
            claimAssetEvidence.CaevSeroId=entity.CaevSeroId;
            claimAssetEvidence.CaevServiceFee = entity.CaevServiceFee;
            claimAssetEvidence.CaevCreatedDate = entity.CaevCreatedDate;

            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            return claimAssetEvidence.Adapt<ClaimAssetEvidenceDtoCreate>();
        }
    }
}
