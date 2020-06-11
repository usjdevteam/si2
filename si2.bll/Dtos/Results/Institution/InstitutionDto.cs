﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.bll.Dtos.Results.Institution
{
    public class InstitutionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [StringLength(3, ErrorMessage = "Code can't be longer than 3 characters.")]
        public string Code { get; set; }
    }
}
