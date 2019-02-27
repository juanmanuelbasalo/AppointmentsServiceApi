using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentsAPI.Helpers
{
    public class AppointmentQueryParameters
    {
        private readonly int maxPageCount = 100;

        public int Page { get; set; } = 1;

        private int pageCount = 100;
        public int PageCount
        {
            get => pageCount;
            set => pageCount = (value > maxPageCount) ? maxPageCount : value; 
        }

        public bool HasQueryFilter => !string.IsNullOrEmpty(Filter);
        public string Filter { get; set; }
    }
}
