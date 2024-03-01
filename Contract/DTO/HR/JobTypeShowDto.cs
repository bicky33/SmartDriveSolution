using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.Entities.HR;

namespace Contract.DTO.HR
{
    public class JobTypeShowDto
    {
   

        public string? JobDesc { get; set; }



        //[InverseProperty("EmpJobCodeNavigation")]
        //public virtual ICollection<EmployeeUserDto> EmployeeUserDto { get; set; } = new List<EmployeeUserDto>();
    }
}
