﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.bll.Dtos.Requests.Document
{
    public class UpdateDocumentDto
    {
        [Required]
        [Display(Name = "NameFr")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string NameFr { get; set; }

        [Required]
        [Display(Name = "NameAr")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string NameAr { get; set; }

        [Required]
        [Display(Name = "NameEn")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string NameEn { get; set; }

        [Required]
        [Display(Name = "DescriptionFr")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string DescriptionFr { get; set; }

        [Required]
        [Display(Name = "DescriptionAr")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string DescriptionAr { get; set; }

        [Required]
        [Display(Name = "DescriptionEn")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string DescriptionEn { get; set; }


        [Required]
        [Display(Name = "isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
