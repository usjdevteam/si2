using System;
using System.Collections.Generic;

namespace si2.bll.Dtos.Requests.UserCourse
{
    public class ManageCoursesUserDto
    {
        public List<Guid> AddCoursesIds { get; set; }
        public List<Guid> DeleteCoursesIds { get; set; }
        public List<String> AddUsersIds { get; set; }
        public List<String> DeleteUsersIds { get; set; }
    }
}