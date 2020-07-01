using si2.common;
using System;

namespace si2.bll.Helpers.ResourceParameters
{
    public class InstitutionResourceParameters
    {
        public int PageNumber { get; set; } = Constants.DEFAULT_PAGE_NUMBER;
        private int _pageSize = Constants.DEFAULT_PAGE_SIZE;
        public Guid? ParentId { get; set; }
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

        public string SearchQuery { get; set; }

        public string Status { get; set; }
    }
}
