using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.HR;

[PrimaryKey("BesaEmpEntityId", "BesaCreatedDate")]
[Table("batch_employee_salary", Schema = "hr")]
public partial class BatchEmployeeSalary
{
    [Key]
    [Column("besa_emp_entity_id")]
    public int BesaEmpEntityId { get; set; }

    [Key]
    [Column("besa_created_date", TypeName = "date")]
    public DateTime BesaCreatedDate { get; set; }

    [Column("ems_trasfer_Date", TypeName = "datetime")]
    public DateTime? EmsTrasferDate { get; set; }

    [Column("besa_total_salary", TypeName = "money")]
    public decimal? BesaTotalSalary { get; set; }

    [Column("besa_account_number")]
    [StringLength(35)]
    [Unicode(false)]
    public string? BesaAccountNumber { get; set; }

    [Column("besa_status")]
    [StringLength(15)]
    [Unicode(false)]
    public string? BesaStatus { get; set; }

    [Column("besa_patr_trxno")]
    [StringLength(55)]
    [Unicode(false)]
    public string? BesaPatrTrxno { get; set; }

    [Column("besa_paid_date", TypeName = "datetime")]
    public DateTime? BesaPaidDate { get; set; }

    [Column("besa_modified_date", TypeName = "datetime")]
    public DateTime? BesaModifiedDate { get; set; }
}
