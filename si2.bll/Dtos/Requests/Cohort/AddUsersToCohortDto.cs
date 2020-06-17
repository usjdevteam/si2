using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.bll.Dtos.Requests.Cohort
{
    public class AddUsersToCohortDto
    {
        [Required]
        public List<string> UsersIds { get; set; }
    }
}
