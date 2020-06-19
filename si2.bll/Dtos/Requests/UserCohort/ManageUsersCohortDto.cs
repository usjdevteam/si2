using System;
using System.Collections.Generic;
using System.Text;

namespace si2.bll.Dtos.Requests.UserCohortDto
{
    public class ManageUsersCohortDto
    {
        public List<string> AddUsersIds { get; set; }
        public List<string> DeleteUsersIds { get; set; }
    }
}
