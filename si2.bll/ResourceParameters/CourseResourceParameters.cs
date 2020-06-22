﻿using si2.common;
using System;
using System.Collections.Generic;
using System.Text;
using static si2.common.Enums;


namespace si2.bll.ResourceParameters
{
    public class CourseResourceParameters
    {
        public int PageNumber { get; set; } = Constants.DEFAULT_PAGE_NUMBER;
        private int _pageSize = Constants.DEFAULT_PAGE_SIZE;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }

            set
            {
                _pageSize = (value > Constants.MAX_PAGE_SIZE) ? Constants.MAX_PAGE_SIZE : value;
            }
        }
    }
}
