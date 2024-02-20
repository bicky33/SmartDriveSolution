using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Contract.DTO.Master
{
    public class CategoryResponse

    {
        public int CateId { get; set; }

        [StringLength(55)]
        [Unicode(false)]
        public string? CateName { get; set; }
    }
}