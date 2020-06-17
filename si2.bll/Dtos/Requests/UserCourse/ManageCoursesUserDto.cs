using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.bll.Dtos.Requests.UserCourse
{
    public class ManageCoursesUserDto
    {
        public List<Guid> AddCoursesIds { get; set; }
        public List<Guid> DeleteCoursesIds { get; set; }
    }
}