﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.DTO.CR.Request
{
    public class CustomerInscDocRequestDto
    {
        public int CadocCreqEntityid { get; set; }
        public string? CadocFilename { get; set; }
        public string? CadocFiletype { get; set; }
        public int? CadocFilesize { get; set; }
        public string? CadocCategory { get; set; }
        public DateTime? CadocModifiedDate { get; set; }
    }
}
