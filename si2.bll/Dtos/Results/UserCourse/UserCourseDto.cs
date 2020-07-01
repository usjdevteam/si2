using si2.dal.Entities;
using System.Collections.Generic;

namespace si2.bll.Dtos.Results.UserCourse
{
    public class UserCourseDto
    {
        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<si2.dal.Entities.Course> Courses { get; set; }
    }
}
