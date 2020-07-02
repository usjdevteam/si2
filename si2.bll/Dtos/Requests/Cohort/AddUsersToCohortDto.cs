using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace si2.bll.Dtos.Requests.Cohort
{
    public class AddUsersToCohortDto
    {
        [Required]
        public List<string> UsersIds { get; set; }
    }
}
