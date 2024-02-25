using Contract.DTO.Partners;
using Contract.DTO.SO;
using Domain.Entities.SO;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Contract.Extensions
{
    public static class ClaimAssets
    {
        public static ClaimAssetEvidenceDtoCreate AsEvidenceSO(this PartnerClaimAssetEvidenceRequest request)
        {
            string filename = $"{Path.GetRandomFileName()}{Path.GetExtension(request.Photo.FileName)}";
            long length = request.Photo.Length / 1024;

            return new ClaimAssetEvidenceDtoCreate(
                    CaevId: null,
                    CaevUrl: null,
                    CaevFiletype: request.Photo.ContentType,
                    CaevFilename: filename,
                    CaevFilesize: (int)length,
                    CaevNote: request.CaevNote,
                    CaevPartEntityid: request.CaevPartEntityid,
                    CaevSeroId: request.CaevSeroId,
                    CaevServiceFee: request.CaevFee,
                    CaevCreatedDate: DateTime.Now,
                    Photo: request.Photo
                );
        }

        public static (List<ClaimAssetEvidence>, List<IFormFile>) AsEvidenceEntity(this PartnerClaimAssetEvidenceBatchRequest request)
        {
            List<ClaimAssetEvidence> data = new();
            List<IFormFile> files = new();
            for (int i = 0; i < request.Photo.Count; i++)
            {
                string filename = $"{Path.GetRandomFileName()}{Path.GetExtension(request.Photo[i].FileName)}";
                long length = request.Photo[i].Length / 1024;

                ClaimAssetEvidence temp = new()
                {
                    CaevUrl = null,
                    CaevFiletype = request.Photo[i].ContentType,
                    CaevFilename = filename,
                    CaevFilesize = (int)length,
                    CaevNote = request.CaevNote[i],
                    CaevPartEntityid = request.CaevPartEntityid[i],
                    CaevSeroId = request.CaevSeroId[i],
                    CaevServiceFee = request.CaevFee[i],
                    CaevCreatedDate = DateTime.Now,
                };

                data.Add(temp);
                files.Add(request.Photo[i]);
            }

            return (data.ToList(), files);
        }
    }
}
