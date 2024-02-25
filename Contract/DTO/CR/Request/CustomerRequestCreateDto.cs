﻿using Contract.DTO.CR.Response;
using Domain.Entities.CR;
using Domain.Entities.HR;
using Domain.Entities.SO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.CR.Request
{
    public class CustomerRequestCreateDto
    {
        public int CreqEntityid { get; set; }
        public DateTime? CreqCreateDate { get; set; }
        public string? CreqStatus { get; set; }
        public string? CreqType { get; set; }
        public DateTime? CreqModifiedDate { get; set; }
        public int? CreqCustEntityid { get; set; }
        public int? CreqAgenEntityid { get; set; }
        public CustomerInscAssetCreateDto? CustomerInscAsset { get; set; }
    }
}