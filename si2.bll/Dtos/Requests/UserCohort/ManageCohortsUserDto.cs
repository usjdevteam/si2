﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace si2.bll.Dtos.Requests.UserCohort
{
    public class ManageCohortsUserDto
    {
        public List<Guid> AddCohortsIds { get; set; }
        public List<Guid> DeleteCohortsIds { get; set; }

        public List<string> AddUsersIds { get; set; }
        public List<string> DeleteUsersIds { get; set; }
    }
}