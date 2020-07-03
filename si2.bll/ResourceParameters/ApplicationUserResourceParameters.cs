﻿using si2.common;
using System;

namespace si2.bll.ResourceParameters
{
    public class ApplicationUserResourceParameters
    {
        public int PageNumber { get; set; } = Constants.DEFAULT_PAGE_NUMBER;
        private int _pageSize = Constants.DEFAULT_PAGE_SIZE;
        public Guid CohortId { get; set; }
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