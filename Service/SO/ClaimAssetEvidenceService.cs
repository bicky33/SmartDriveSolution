using Contract.DTO.Partners;
using Contract.DTO.SO;
using Contract.Extensions;
using Domain.Entities.SO;
using Domain.Repositories.SO;
using Mapster;
using Service.Abstraction.Helpers;
using Service.Abstraction.Partners;
using System.Security.Claims;

namespace Service.SO
{
    public class ClaimAssetEvidenceService : IServicePartnerClaimAssetEvidence
    {
        private readonly IRepositorySOManager _repositoryManager;
        private readonly IFileServer _fileServer;
        private readonly string _basePath = @".\Resources\ClaimEvidences";
        private readonly string _baseFolder = @"Resources/ClaimEvidences";

        public ClaimAssetEvidenceService(IRepositorySOManager repositoryManager, IFileServer fileServer)
        {
            _repositoryManager = repositoryManager;
            _fileServer = fileServer;
        }

        public async Task<ClaimAssetEvidenceDtoCreate> CreateAsync(ClaimAssetEvidenceDtoCreate entityDto)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsyncWithLink(ClaimAssetEvidenceDtoCreate entityDto, string baseUrl)
        {
            try
            {
                ClaimAssetEvidence claim = entityDto.Adapt<ClaimAssetEvidence>();
                claim.CaevUrl = $"{baseUrl}/{_baseFolder}/{entityDto.CaevFilename}";
                _repositoryManager.ClaimAssetEvidenceRepository.CreateEntity(claim);
                await _repositoryManager.UnitOfWork.SaveChangesAsync();
                string path = Path.Combine(_basePath, entityDto.CaevFilename);
                await _fileServer.Save(entityDto.Photo, path);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task CreateBatch(PartnerClaimAssetEvidenceBatchRequest request, string baseUrl)
        {
            try
            {
                var (claims, files) = request.AsEvidenceEntity();
                int length = files.Count;
                for (var i = 0; i< length; i++)
                {
                    claims[i].CaevUrl = $"{baseUrl}/{_baseFolder}/{claims[i].CaevFilename}";
                    string path = Path.Combine(_basePath, claims[i].CaevFilename);
                    await _fileServer.Save(files[i], path);
                }
                await _repositoryManager.RepositoryPartnerClaimAssetEvidenceBatch.CreateBatch(claims);
                await _repositoryManager.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            ClaimAssetEvidence claim = await _repositoryManager.ClaimAssetEvidenceRepository.GetEntityById(id, true);
            _repositoryManager.ClaimAssetEvidenceRepository.DeleteEntity(claim);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
            string path = Path.Combine(_baseFolder, claim.CaevFilename);
            await _fileServer.Delete(path);
        }

        public async Task DeleteBatch(int caspPartEntityid, string caspSeroId)
        {
            IEnumerable<ClaimAssetEvidence> claims = await _repositoryManager.RepositoryPartnerClaimAssetEvidenceBatch
                .GetData(caspPartEntityid, caspSeroId);
            await _repositoryManager.RepositoryPartnerClaimAssetEvidenceBatch.DeleteBatch(caspPartEntityid, caspSeroId);
            foreach (var item in claims)
            {
                string path = Path.Combine(_baseFolder, item.CaevFilename);
                await _fileServer.Delete(path);
            }
        }

        public async Task<IEnumerable<ClaimAssetEvidenceDto>> GetAllAsync(bool trackChanges)
        {
            IEnumerable<ClaimAssetEvidence> data = await _repositoryManager.ClaimAssetEvidenceRepository.GetAllEntity(trackChanges);
            return data.Adapt<IEnumerable<ClaimAssetEvidenceDto>>();
        }

        public async Task<ClaimAssetEvidenceDto> GetByIdAsync(int id, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public async Task<ClaimAssetEvidenceDtoCreate> UpdateAsync(int id, ClaimAssetEvidenceDtoCreate entityDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClaimAssetEvidenceDto>> GetByParameter(int caspPartEntityid, string caspSeroId)
        {
            IEnumerable<ClaimAssetEvidence> claims = await _repositoryManager.RepositoryPartnerClaimAssetEvidenceBatch.GetData(caspPartEntityid, caspSeroId);
            return claims.Adapt<IEnumerable<ClaimAssetEvidenceDto>>();
        }
    }
}
