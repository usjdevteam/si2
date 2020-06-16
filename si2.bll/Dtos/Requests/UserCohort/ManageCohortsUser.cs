using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;



namespace si2.bll.Dtos.Requests.Cohort
{
    public class ManageCohortsUserDto
    {
        public List<Guid> AddCohortsIds { get; set; }
        public List<Guid> DeleteCohortsIds { get; set; }
    }
}